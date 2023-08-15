using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.VerificationCodeDto.Dto;
using Worigo.Core.Dtos.VerificationCodeDto.Request;
using Worigo.Core.Dtos.VerificationCodeDto.Response;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class VerificationCodeController : CustomBaseController
    {
        public readonly IVerificationCodeService _vertificationCodeService;
        private readonly ICustomerService _customerService;
        public VerificationCodeController(IMapper mapper, IVerificationCodeService vertificationCodeService, IRoomService roomService, ICustomerService customerService)
        {
            _vertificationCodeService = vertificationCodeService;
            _customerService = customerService;
        }
        [HttpPost]
        public ResponseDto<VerificationCodeResponse> VerificationCodeProduce([FromHeader] string Authorization, VerificationCodeRequest entity)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _vertificationCodeService.GetVertificationProduce(entity, keys);
        }
        [HttpPost]
        public ResponseDto<List<CheckinTimeResponse>> GetCheckinTimeByDate([FromHeader] string Authorization, CheckinTimeRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _vertificationCodeService.GetCheckinTimeByDate(request.hotelid, request.startDate, request.endDate, keys);
        }
        [HttpPost]
        [AllowAnonymous]
        public ResponseDto<VerificationCodeForAccessDto> CodeForAccess(ForAccessRequestModel requestModel)
        {
            return _vertificationCodeService.CodeForAccess(requestModel);
        }

    }
}
