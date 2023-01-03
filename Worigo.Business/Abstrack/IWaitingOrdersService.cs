using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IWaitingOrdersService
    {
        List<WaitingOrders> GetAll();
        WaitingOrders GetById(int id);
        WaitingOrders Create(WaitingOrders entity);
        WaitingOrders Update(WaitingOrders entity);
    }
}
