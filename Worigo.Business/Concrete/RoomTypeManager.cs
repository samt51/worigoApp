using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class RoomTypeManager : IRoomTypeService
    {
        private readonly IRoomTypeDal _roomTypeDal;
        public RoomTypeManager(IRoomTypeDal roomTypeDal)
        {
            _roomTypeDal = roomTypeDal;
        }
        public void Create(RoomType entity)
        {
            _roomTypeDal.Create(entity);
        }

        public List<RoomType> GetAll()
        {
            return _roomTypeDal.GetAll();
        }

        public RoomType GetById(int id)
        {
            return _roomTypeDal.GetById(id);
        }

        public void Update(RoomType entity)
        {
            _roomTypeDal.Update(entity);
        }
    }
}
