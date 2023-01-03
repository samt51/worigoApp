using System;
using Worigo.Core.Dtos.Reports.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IVertificationCodeDal: IRepositoryDesignPattern<VertificationCodes>
    {
        VertificationCodes GetCodesByRoomid(int roomnumber);
        GetRoomOccupancyRateByDateResponse GetRoomOccupancyRateByDate(int hotelid,DateTime startDate);
        RoomCountResponse GetRoomCountByDate(int hotelid, DateTime date);
        RoomCountResponse GetTotalRoomCountOfUsedApp(int hotelid);
        RoomCountResponse GetTotalRoomCountOfUsedAppDateSearch(int hotelid, DateTime starDate, DateTime endDate);
    }
}
