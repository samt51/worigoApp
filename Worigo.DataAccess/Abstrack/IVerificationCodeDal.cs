using System;
using System.Collections.Generic;
using Worigo.Core.Dtos.Reports.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.VerificationCodeDto.Dto;
using Worigo.Core.Dtos.VerificationCodeDto.Request;
using Worigo.Core.Dtos.VerificationCodeDto.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IVerificationCodeDal : IRepositoryDesignPattern<VerificationCodes>
    {
        VerificationCodes GetCodesByRoomid(int roomnumber);
        GetRoomOccupancyRateByDateResponse GetRoomOccupancyRateByDate(int hotelid, DateTime startDate);
        RoomCountResponse GetRoomCountByDate(int hotelid, DateTime date);
        RoomCountResponse GetTotalRoomCountOfUsedApp(int hotelid);
        RoomCountResponse GetTotalRoomCountOfUsedAppDateSearch(int hotelid, DateTime starDate, DateTime endDate);
        List<CheckinTimeResponse> GetCheckinTimeByDate(int hotelid, DateTime startDate, DateTime endDate);

        ResponseDto<VerificationCodeResponse> GetVertificationProduce(VerificationCodeRequest request);
        ResponseDto<VerificationCodeForAccessDto> CodeForAccess(ForAccessRequestModel requestModel);
         ResponseDto<VerificationCodeResponse> CodeForAccess(string code);
    }
}
