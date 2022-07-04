using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IServicesService
    {

        List<ServiceAndHotelJoin> serviceAndHotelJoins(int hoteid);
        List<Services> GetAll();
        Services GetById(int id);
        void Create(Services entity);
        void Update(Services entity);
    }
}
