using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class WaitingOrdersManager : IWaitingOrdersService
    {
        private readonly IWaitingOrdersDal _waitingOrdersDal;
        public WaitingOrdersManager(IWaitingOrdersDal waitingOrdersDal)
        {
            _waitingOrdersDal = waitingOrdersDal;
        }
    
        public WaitingOrders Create(WaitingOrders entity)
        {
         return   _waitingOrdersDal.Create(entity);
        }

        public List<WaitingOrders> GetAll()
        {
            return _waitingOrdersDal.GetAll();
        }

        public WaitingOrders GetById(int id)
        {
            return _waitingOrdersDal.GetById(id);
        }

        public WaitingOrders Update(WaitingOrders entity)
        {
          return  _waitingOrdersDal.Update(entity);
        }
    }
}
