using System;
using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Reports.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.VertificationCodeDto.Request;
using Worigo.Core.Dtos.VertificationCodeDto.Response;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IVertificationCodeService
    {
        ResponseDto<GetRoomOccupancyRateByDateResponse> GetRoomOccupancyRateByDate(int hotelid, DateTime startDate,TokenKeys keys);
        ResponseDto<RoomCountResponse> GetRoomCountByDate(int hotelid, DateTime date,TokenKeys keys);
        List<VertificationCodes> GetAll();
        VertificationCodes GetById(int id);
        ResponseDto<VertificationCodeResponse> Create(VertificationCodeRequest entity, TokenKeys keys);
        ResponseDto<VertificationCodeResponse> Update(VertificationCodeRequest entity, TokenKeys keys);
        ResponseDto<RoomCountResponse> GetTotalRoomCountOfUsedApp(int hotelid,TokenKeys keys);
        ResponseDto<RoomCountResponse> GetTotalRoomCountOfUsedAppDateSearch(int hotelid, DateTime starDate, DateTime endDate,TokenKeys keys);
    }
}
