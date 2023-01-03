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

        private readonly IVertificationCodeService _vertificationCodeService;
        private readonly IHotelService _hotelService;
        private readonly IDepartmanService _departmanService;
        private readonly ICommentService _commentService;
        private readonly IManagementOfHotelService _managementOfHotelService;

        public ReportsController(ICommentService commentService, IVertificationCodeService vertificationCodeService, IManagementOfHotelService managementOfHotelService, IDepartmanService departmanService, IHotelService hotelService)
        {

            _vertificationCodeService = vertificationCodeService;
            _departmanService = departmanService;
            _commentService = commentService;
            _hotelService = hotelService;
            _managementOfHotelService = managementOfHotelService;

        }
        [HttpGet("{hotelid}/{starDate}")]
        public ResponseDto<GetRoomOccupancyRateByDateResponse> GetRoomOccupancyRateByDate([FromHeader] string Authorization, int hotelid, DateTime starDate)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _vertificationCodeService.GetRoomOccupancyRateByDate(hotelid, starDate, keys);
        }
        [HttpGet("{hotelid}/{departmanid}")]
        public ResponseDto<DepartmentCommentRateResponse> DepartmanCommentRateByHotelIdAndDepartmanId([FromHeader] string Authorization, int hotelid, int departmanid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            var data = _departmanService.DepartmanCommentRateResponse(hotelid, departmanid, keys);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                return data;
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                return data;
            }
            return new ResponseDto<DepartmentCommentRateResponse>().Authorization();
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<HotelGeneralPointResponse> GetGeneralHotelPointByHotelId([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _commentService.HotelGeneralPointByHotelId(hotelid, keys);

        }
        [HttpGet("{hotelid}/{date}")]
        public ResponseDto<RoomCountResponse> GetRoomCountByHotelIdAndDate([FromHeader] string Authorization, int hotelid, DateTime date)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _vertificationCodeService.GetRoomCountByDate(hotelid, date, keys);
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<RoomCountResponse> GetTotalRoomCountOfUsedApp([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _vertificationCodeService.GetTotalRoomCountOfUsedApp(hotelid, keys);
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<RoomCountResponse> GetTotalRoomCountOfUsedAppDateSearch([FromHeader] string Authorization, int hotelid, DateTime startDate, DateTime finishDate)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _vertificationCodeService.GetTotalRoomCountOfUsedAppDateSearch(hotelid, startDate, finishDate, keys);

        }

    }
}
