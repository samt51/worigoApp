using System.Collections.Generic;
using Worigo.Core.Dtos.Comment.Request;
using Worigo.Core.Dtos.Comment.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Reports.HotelGeneralPuan;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface ICommentService
    {
        ResponseDto<CommentResponse> GetById(int id, TokenKeys keys);
        ResponseDto<List<CommentResponse>> commentListJoins(int hotelid, TokenKeys keys);
        ResponseDto<CommentResponse> Create(CommentAddOrUpdateRequest request, TokenKeys keys);
        ResponseDto<CommentResponse> Update(CommentAddOrUpdateRequest request, TokenKeys keys);
        ResponseDto<HotelGeneralPointResponse> HotelGeneralPointByHotelId(int hotelid, TokenKeys keys);
        ResponseDto<List<CommentResponse>> GetEmployeesOfCommentByHotelidAndEmployeesid(int hotelid, int employeeid, TokenKeys keys);
        ResponseDto<List<GetOrderCommentResponse>> GetOrderCommentByVerificationId(int vertificationId, TokenKeys keys);
    }
}
