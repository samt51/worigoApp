using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IRoomTypeService
    {
        List<RoomType> GetAll();
        RoomType GetById(int id);
        RoomType Create(RoomType entity);
        RoomType Update(RoomType entity);
    }
}
