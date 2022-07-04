using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IHotelService
    {
        List<Hotel> GetAll();
        Hotel GetById(int id);
        void Create(Hotel entity);
        void Update(Hotel entity);
    }
}
