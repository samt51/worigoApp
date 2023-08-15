using System;
using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Reports.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.VerificationCodeDto.Dto;
using Worigo.Core.Dtos.VerificationCodeDto.Request;
using Worigo.Core.Dtos.VerificationCodeDto.Response;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IVerificationCodeService
    {
        ResponseDto<GetRoomOccupancyRateByDateResponse> GetRoomOccupancyRateByDate(int hotelid, DateTime startDate, TokenKeys keys);
        ResponseDto<RoomCountResponse> GetRoomCountByDate(int hotelid, DateTime date, TokenKeys keys);
        List<VerificationCodes> GetAll();
        VerificationCodes GetById(int id);
        ResponseDto<VerificationCodeResponse> Create(VerificationCodeRequest entity, TokenKeys keys);
        ResponseDto<VerificationCodeResponse> Update(VerificationCodeRequest entity, TokenKeys keys);
        ResponseDto<RoomCountResponse> GetTotalRoomCountOfUsedApp(int hotelid, TokenKeys keys);
        ResponseDto<RoomCountResponse> GetTotalRoomCountOfUsedAppDateSearch(int hotelid, DateTime starDate, DateTime endDate, TokenKeys keys);
        ResponseDto<List<CheckinTimeResponse>> GetCheckinTimeByDate(int hotelid, DateTime startDate, DateTime endDate, TokenKeys keys);
        ResponseDto<VerificationCodeResponse> GetVertificationProduce(VerificationCodeRequest request, TokenKeys keys);
        ResponseDto<VerificationCodeResponse> CodeForAccess(string code);
        ResponseDto<VerificationCodeForAccessDto> CodeForAccess(ForAccessRequestModel requestModel);
    }
}
