using System.Collections.Generic;
using Worigo.Core.Dtos.ServicesValue.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IServicesValuesDal : IRepositoryDesignPattern<ServicesValues>
    {
        List<ServicesValueResponse> GetValueByServiceId(int serviceid, int hotelId);

    }
}
