using System.Collections.Generic;
using Worigo.Core.Dtos.Comment.Response;
using Worigo.Core.Dtos.Reports.HotelGeneralPuan;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface ICommentDal:IRepositoryDesignPattern<Comment>
    {
        List<CommentResponse> GetCommentByHotelid(int hotelid);
        List<CommentResponse> GetEmployeesOfCommentByHotelidAndEmployeesid(int hotelid, int employeeid);
        CommentResponse GetByIdJoin(int id);
        HotelGeneralPointResponse HotelGeneralPointByHotelId(int hotelid);  
    }
}
