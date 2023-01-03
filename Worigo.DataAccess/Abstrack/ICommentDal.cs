using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.Reports.HotelGeneralPuan;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface ICommentDal:IRepositoryDesignPattern<Comment>
    {
        List<CommentListJoin> GetCommentByHotelid(int hotelid);
        List<CommentListJoin> GetEmployeesOfCommentByHotelidAndEmployeesid(int hotelid, int employeeid);
        CommentListJoin GetByIdJoin(int id);
        HotelGeneralPointResponse HotelGeneralPointByHotelId(int hotelid);  
    }
}
