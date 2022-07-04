using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class HotelManager : IHotelService
    {
        private readonly IHotelDal _hotelsDal;
        public HotelManager(IHotelDal hotelsDal)
        {
            _hotelsDal = hotelsDal;
        }
        public void Create(Hotel entity)
        {
            _hotelsDal.Create(entity);
        }

        public List<Hotel> GetAll()
        {
            return _hotelsDal.GetAll(x=>x.isDeleted==false);
        }

        public Hotel GetById(int id)
        {
          return _hotelsDal.GetById(id);
        }

        public void Update(Hotel entity)
        {
            _hotelsDal.Update(entity);
        }
    }
}
