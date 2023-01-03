using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
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
        private readonly IHotelDal _hotelDal;
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        public CommentManager(ICommentDal commentDal, IHotelDal hotelDal, IManagementOfHotelsDal managementOfHotelsDal)
        {
            _commentDal = commentDal;
            _hotelDal = hotelDal;
            _managementOfHotelsDal = managementOfHotelsDal;
        }

        public List<CommentListJoin> commentListJoins(int hotelid)
        {
            return _commentDal.GetCommentByHotelid(hotelid);
        }

        public Comment Create(Comment entity)
        {
            return _commentDal.Create(entity);
        }

        public List<Comment> GetAll()
        {
            return _commentDal.GetAll(x => x.isDeleted == false);
        }

        public Comment GetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public CommentListJoin GetByIdJoin(int id)
        {
            return _commentDal.GetByIdJoin(id);
        }



        public Comment Update(Comment entity)
        {
            return _commentDal.Update(entity);
        }

        ResponseDto<HotelGeneralPointResponse> ICommentService.HotelGeneralPointByHotelId(int hotelid, TokenKeys keys)
        {
            var hotel = _hotelDal.GetById(hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var GeneralHotelPoint = _commentDal.HotelGeneralPointByHotelId(hotelid);
                return new ResponseDto<HotelGeneralPointResponse>().Success(GeneralHotelPoint, 200);
            }
            if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var GeneralHotelPoint = _commentDal.HotelGeneralPointByHotelId(hotelid);
                return new ResponseDto<HotelGeneralPointResponse>().Success(GeneralHotelPoint, 200);
            }
            return new ResponseDto<HotelGeneralPointResponse>().Authorization();

        }
    }
}
