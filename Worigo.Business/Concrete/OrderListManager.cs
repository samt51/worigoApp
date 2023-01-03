using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class OrderListManager : IOrderListService
    {
        private readonly IOrderListDal _orderListDal;
        public OrderListManager(IOrderListDal orderListDal)
        {
            _orderListDal = orderListDal;
        }
        
        public OrderList Create(OrderList entity)
        {
           return _orderListDal.Create(entity);
        }

        public List<OrderList> GetAll()
        {
            return _orderListDal.GetAll();
        }

        public OrderList GetById(int id)
        {
            return _orderListDal.GetById(id);
        }

        public OrderList Update(OrderList entity)
        {
          return  _orderListDal.Update(entity);
        }
    }
}
