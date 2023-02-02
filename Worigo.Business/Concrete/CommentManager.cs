using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Comment.Request;
using Worigo.Core.Dtos.Comment.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Reports.HotelGeneralPuan;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;
        private readonly IHotelService _hotelService;
        private readonly IManagementOfHotelService _managementOfHotelService;
        private readonly IMapper _mapper;
        public CommentManager(ICommentDal commentDal, IMapper mapper, IHotelService hotelService, IManagementOfHotelService managementOfHotelService)
        {
            _commentDal = commentDal;
            _hotelService = hotelService;
            _mapper = mapper;
            _managementOfHotelService = managementOfHotelService;
        }
        public ResponseDto<List<CommentResponse>> commentListJoins(int hotelid, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            if ((keys.companyid == hotel.data.Companyid) && keys.role == 2 || keys.role == 1)
            {
                var listcomment = _commentDal.GetCommentByHotelid(hotelid);
                return new ResponseDto<List<CommentResponse>>().Success(listcomment, 200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var listcomment = _commentDal.GetCommentByHotelid(hotelid);
                return new ResponseDto<List<CommentResponse>>().Success(listcomment, 200);
            }
            return new ResponseDto<List<CommentResponse>>().Authorization();
        }

        public ResponseDto<CommentResponse> Create(CommentAddOrUpdateRequest request, TokenKeys keys)
        {
            var map = _mapper.Map<Comment>(request);
            var response = _commentDal.Create(map);
            return new ResponseDto<CommentResponse>().Success(_mapper.Map<CommentResponse>(response), 200);
        }
        public ResponseDto<CommentResponse> GetById(int id, TokenKeys keys)
        {
            var comment = _commentDal.GetByIdJoin(id);
            var hotel = _hotelService.GetById(keys, comment.hotelid);
            if (keys.role == 3)
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, comment.hotelid);
            if (keys.role == 2 && keys.companyid == hotel.data.Companyid || keys.role == 3 || keys.role == 1)
            {
                return new ResponseDto<CommentResponse>().Success(comment, 200);
            }
            return new ResponseDto<CommentResponse>().Authorization();
        }
        public ResponseDto<CommentResponse> Update(CommentAddOrUpdateRequest request, TokenKeys keys)
        {
            var data = _commentDal.GetById(request.Id);
            data.employeesid = request.employeesid;
            data.Commentary = request.Commentary;
            data.Point = request.Point;
            data.speedPoint = request.speedPoint;
            data.contentsPoint = request.contentsPoint;
            data.ModifyDate = System.DateTime.Now;
            var response = _commentDal.Update(data);
            return new ResponseDto<CommentResponse>().Success(_mapper.Map<CommentResponse>(data), 200);
        }
        ResponseDto<HotelGeneralPointResponse> ICommentService.HotelGeneralPointByHotelId(int hotelid, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.data.Companyid) || keys.role == 1)
            {
                var GeneralHotelPoint = _commentDal.HotelGeneralPointByHotelId(hotelid);
                return new ResponseDto<HotelGeneralPointResponse>().Success(GeneralHotelPoint, 200);
            }
            if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var GeneralHotelPoint = _commentDal.HotelGeneralPointByHotelId(hotelid);
                return new ResponseDto<HotelGeneralPointResponse>().Success(GeneralHotelPoint, 200);
            }
            return new ResponseDto<HotelGeneralPointResponse>().Authorization();

        }
    }
}
