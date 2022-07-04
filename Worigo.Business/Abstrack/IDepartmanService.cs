using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IDepartmanService
    {
        List<DepartmanAndHotelJoin> GetAllJoin(int hotelid);
        List<Departman> GetAll();
        Departman GetById(int id);
        void Create(Departman entity);
        void Update(Departman entity);
    }
}
