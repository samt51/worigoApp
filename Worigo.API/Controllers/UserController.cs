using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Business.Encryption;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHotelService _hotelService;
        TokenViewModel tkn = new TokenViewModel();
        public UserController(IMapper mapper, IUserService userService, IHotelService hotelService)
        {
            _mapper = mapper;
            _userService = userService;
            _hotelService = hotelService;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var user = _userService.GetUserByEmailAndPassword(loginViewModel.email, loginViewModel.password);
            if (user != null)
            {
                var token = _userService.ProduceToken(user.id.ToString(), user.email, user.roleid.ToString(), user.hotelid.ToString());
                tkn.Token = token;
                return CreateActionResult(ResponseDto<TokenViewModel>.Success(tkn, 200));
            }
            return CreateActionResult(ResponseDto<TokenViewModel>.Fail(400, "Your Email And Password Is Wrong"));
        }
        [HttpPost]
        public IActionResult Update([FromHeader] string Authorization, UserDto userDto)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var entity = _userService.GetById(userDto.id);
            entity.isActive = userDto.isActive;
            entity.ModifyDate = System.DateTime.Now;
            entity.email = userDto.email;
            if (keys.userId == entity.id || keys.role <= 3)
            {
                _userService.Update(entity);
                return CreateActionResult(ResponseDto<UserDto>.Success(200));
            }
            return CreateActionResult(ResponseDto<UserDto>.Authorization());
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var userdata = _userService.GetById(id);
            keys.role = 2;
            if (keys.userId == userdata.id || keys.role <= 3)
            {
                var join = new UserAndUserRoleJoin
                {
                    id = userdata.id,
                    email = userdata.email,
                    hotel = _hotelService.GetById(userdata.hotelid).HotelName,
                };
                return CreateActionResult(ResponseDto<UserAndUserRoleJoin>.Success(join, 200));
            }
            return CreateActionResult(ResponseDto<UserDto>.Authorization());

        }
        [HttpPost]
        public IActionResult Add([FromHeader] string Authorization, UserDto userDto)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var entity = new User
            {
                CreatedDate = System.DateTime.Now,
                email = userDto.email,
                isActive = userDto.isActive,
                isDeleted = false,
                ModifyDate = System.DateTime.Now,
                password = userDto.password,
                roleid = userDto.roleid,
                hotelid = keys.hotelid
            };
            if (keys.role == 2 || keys.role == 3)
                entity.hotelid = keys.hotelid;
            else if (keys.role == 1) { }
            else
                return CreateActionResult(ResponseDto<UserDto>.Authorization());
            entity.hotelid = userDto.hotelid;
            _userService.Create(entity);
            return CreateActionResult(ResponseDto<UserDto>.Success(200));
        }
        [HttpGet]
        public IActionResult List([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys.role == 1)
            {
                var usergetall = _userService.GetAllJoin();
                return CreateActionResult(ResponseDto<List<UserAndUserRoleJoin>>.Success(usergetall, 200));
            }
            return CreateActionResult(ResponseDto<List<UserAndUserRoleJoin>>.Authorization());
        }
    }
}
