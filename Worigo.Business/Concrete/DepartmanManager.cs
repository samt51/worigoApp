using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class DepartmanManager : IDepartmanService
    {
        private readonly IDepartmanDal _departmanDal;
        public DepartmanManager(IDepartmanDal departmanDal)
        {
            _departmanDal = departmanDal;
        }
        public void Create(Departman entity)
        {
            _departmanDal.Create(entity);
        }

        public List<Departman> GetAll()
        {
            return _departmanDal.GetAll(x => x.isDeleted == false);
        }

        public List<DepartmanAndHotelJoin> GetAllJoin(int hotelid)
        {
            return _departmanDal.GetAllJoin(hotelid);
        }

        public Departman GetById(int id)
        {
            return _departmanDal.GetById(id);
        }

        public void Update(Departman entity)
        {
            _departmanDal.Update(entity);
        }
    }
}
