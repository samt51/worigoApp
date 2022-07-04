using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IRoomTypeService
    {
        List<RoomType> GetAll();
        RoomType GetById(int id);
        void Create(RoomType entity);
        void Update(RoomType entity);
    }
}
