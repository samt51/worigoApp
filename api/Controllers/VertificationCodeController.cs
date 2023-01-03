using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.API.Model;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.VertificationCodeDto.Request;
using Worigo.Core.Dtos.VertificationCodeDto.Response;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class VertificationCodeController : CustomBaseController
    {
        public readonly IVertificationCodeService _vertificationCodeService;
        public VertificationCodeController(IMapper mapper, IVertificationCodeService vertificationCodeService, IRoomService roomService)
        {
            _vertificationCodeService = vertificationCodeService;
        }
        [HttpPost]
        public ResponseDto<VertificationCodeResponse> CodeCreate([FromHeader] string Authorization, VertificationCodeRequest entity)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _vertificationCodeService.Create(entity, keys);

        }
      
    }
}
