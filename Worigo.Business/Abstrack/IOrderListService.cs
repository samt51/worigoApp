using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
  public interface IOrderListService
    {
        List<OrderList> GetAll();
        OrderList GetById(int id);
        OrderList Create(OrderList entity);
        OrderList Update(OrderList entity);
    }
}
