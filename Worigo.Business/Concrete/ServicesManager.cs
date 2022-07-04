using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class ServicesManager : IServicesService
    {
        private readonly IServicesDal _servicesDal;
        public ServicesManager(IServicesDal servicesDal)
        {
            _servicesDal = servicesDal;
        }
        public void Create(Services entity)
        {
            _servicesDal.Create(entity);
        }

        public List<Services> GetAll()
        {
            return _servicesDal.GetAll(x => x.isDeleted == false);
        }

        public Services GetById(int id)
        {
            return _servicesDal.GetById(id);
        }

        public List<ServiceAndHotelJoin> serviceAndHotelJoins(int hoteid)
        {
            return _servicesDal.serviceAndHotelJoins(hoteid);
        }

        public void Update(Services entity)
        {
            _servicesDal.Update(entity);
        }
    }
}
