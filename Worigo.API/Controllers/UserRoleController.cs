using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserRoleController : CustomBaseController
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;
        public UserRoleController(IUserRoleService userRoleService, IMapper mapper)
        {
            _mapper = mapper;
            _userRoleService = userRoleService;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var entity = _userRoleService.GetById(id);
            var commentSingularDto = _mapper.Map<UserRoleDto>(entity);
            return CreateActionResult(ResponseDto<UserRoleDto>.Success(commentSingularDto, 200));
        }
        [HttpPost]
        public IActionResult Update(UserRoleDto userRoleDto)
        {
            var entity = _userRoleService.GetById(userRoleDto.id);
            entity.ModifyDate = System.DateTime.Now;
            entity.isActive = userRoleDto.isActive;
            entity.RoleName = userRoleDto.RoleName;
            _userRoleService.Update(entity);
            return CreateActionResult(ResponseDto<UserRole>.Success(200));
        }
        [HttpPost]
        public IActionResult Add(UserRoleDto userRoleDto)
        {
            var entity = new UserRole
            {
                CreatedDate = System.DateTime.Now,
                isDeleted = false,
                isActive = userRoleDto.isActive,
                RoleName = userRoleDto.RoleName,
                ModifyDate = System.DateTime.Now
            };
            _userRoleService.Create(entity);
            return CreateActionResult(ResponseDto<UserRole>.Success(200));
        }
    }
}
