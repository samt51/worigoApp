using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.UserRole.Request;
using Worigo.Core.Dtos.UserRole.Response;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserRoleController : CustomBaseController
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;
        public UserRoleController(IUserRoleService userRoleService, IMapper mapper)
        {
            _mapper = mapper;
            _userRoleService = userRoleService;
        }
        [HttpGet]
        public ResponseDto<List<UserRoleResponse>> GetAll([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _userRoleService.GetAll(keys);
        }
        [HttpGet("{id}")]
        public ResponseDto<UserRoleResponse> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _userRoleService.GetById(id, keys);
        }
        [HttpPost]
        public ResponseDto<UserRoleResponse> Update([FromHeader] string Authorization, UserRoleRequest entity)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _userRoleService.Update(entity, keys);
        }
        [HttpPost]
        public ResponseDto<UserRoleResponse> Add([FromHeader] string Authorization, UserRoleRequest entity)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _userRoleService.Create(entity, keys);
        }
    }
}
