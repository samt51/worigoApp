using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Departman.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Reports.HotelGeneralPuan;
using Worigo.Core.Dtos.Reports.Response;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ReportsController : CustomBaseController
    {
        private readonly IHotelService _hotelService;
        private readonly ICommentService _commentService;
        private readonly IRoomService _roomService;
        private readonly IVertificationCodeService _vertificationCodeService;
        private readonly IManagementOfHotelService _managementOfHotelService;
        private readonly IDepartmanService _departmanService;
        public ReportsController(IHotelService hotelService, IVertificationCodeService vertificationCodeService,
            IRoomService roomService, IManagementOfHotelService managementOfHotelService,
            IDepartmanService departmanService, ICommentService commentService)
        {
            _hotelService = hotelService;
            _vertificationCodeService = vertificationCodeService;
            _roomService = roomService;
            _commentService = commentService;
            _managementOfHotelService = managementOfHotelService;
            _departmanService = departmanService;
        }
        [HttpGet("{hotelid}/{starDate}")]
        public IActionResult GetRoomOccupancyRateByDate([FromHeader] string Authorization, int hotelid, DateTime starDate)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            var value = _vertificationCodeService.GetRoomOccupancyRateByDate(hotelid, starDate);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                return CreateActionResult(ResponseDto<GetRoomOccupancyRateByDateResponse>.Success(value, 200));
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                return CreateActionResult(ResponseDto<GetRoomOccupancyRateByDateResponse>.Success(value, 200));
            }
            return CreateActionResult(ResponseDto<GetRoomOccupancyRateByDateResponse>.Authorization());
        }
        [HttpGet("{hotelid}/{departmanid}")]
        public IActionResult DepartmanCommentRateByHotelIdAndDepartmanId([FromHeader] string Authorization, int hotelid, int departmanid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            var data = _departmanService.DepartmanCommentRateResponse(hotelid, departmanid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                return CreateActionResult(ResponseDto<DepartmentCommentRateResponse>.Success(data, 200));
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                return CreateActionResult(ResponseDto<DepartmentCommentRateResponse>.Success(data, 200));
            }
            return CreateActionResult(ResponseDto<DepartmentCommentRateResponse>.Authorization());
        }
        [HttpGet("{hotelid}")]
        public IActionResult GetGeneralHotelPointByHotelId([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var GeneralHotelPoint = _commentService.HotelGeneralPointByHotelId(hotelid);
                return CreateActionResult(ResponseDto<HotelGeneralPointResponse>.Success(GeneralHotelPoint, 200));
            }
            if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var GeneralHotelPoint = _commentService.HotelGeneralPointByHotelId(hotelid);
                return CreateActionResult(ResponseDto<HotelGeneralPointResponse>.Success(GeneralHotelPoint, 200));
            }
            return CreateActionResult(ResponseDto<HotelGeneralPointResponse>.Authorization());

        }
        [HttpGet("{hotelid}/{date}")]
        public IActionResult GetRoomCountByHotelIdAndDate([FromHeader] string Authorization, int hotelid, DateTime date)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var GeneralHotelPoint = _vertificationCodeService.GetRoomCountByDate(hotelid, date);
                return CreateActionResult(ResponseDto<RoomCountResponse>.Success(GeneralHotelPoint, 200));
            }
            if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var GeneralHotelPoint = _vertificationCodeService.GetRoomCountByDate(hotelid, date);    
                return CreateActionResult(ResponseDto<RoomCountResponse>.Success(GeneralHotelPoint, 200));
            }
            return CreateActionResult(ResponseDto<HotelGeneralPointResponse>.Authorization());
        }
        [HttpGet("{hotelid}")]
        public IActionResult GetTotalRoomCountOfUsedApp([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var GetTotalRoomCountOfUsedAppCount = _vertificationCodeService.GetTotalRoomCountOfUsedApp(hotelid);
                return CreateActionResult(ResponseDto<RoomCountResponse>.Success(GetTotalRoomCountOfUsedAppCount, 200));
            }
            if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var GetTotalRoomCountOfUsedAppCount = _vertificationCodeService.GetTotalRoomCountOfUsedApp(hotelid);
                return CreateActionResult(ResponseDto<RoomCountResponse>.Success(GetTotalRoomCountOfUsedAppCount, 200));
            }
            return CreateActionResult(ResponseDto<HotelGeneralPointResponse>.Authorization());
        }
        [HttpGet("{hotelid}")]
        public IActionResult GetTotalRoomCountOfUsedAppDateSearch([FromHeader] string Authorization, int hotelid,DateTime startDate,DateTime finishDate)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var GetTotalRoomCountOfUsedAppCount = _vertificationCodeService.GetTotalRoomCountOfUsedAppDateSearch(hotelid,startDate,finishDate);
                return CreateActionResult(ResponseDto<RoomCountResponse>.Success(GetTotalRoomCountOfUsedAppCount, 200));
            }
            if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var GetTotalRoomCountOfUsedAppCount = _vertificationCodeService.GetTotalRoomCountOfUsedAppDateSearch(hotelid, startDate, finishDate);
                return CreateActionResult(ResponseDto<RoomCountResponse>.Success(GetTotalRoomCountOfUsedAppCount, 200));
            }
            return CreateActionResult(ResponseDto<HotelGeneralPointResponse>.Authorization());
        }

    }
}
