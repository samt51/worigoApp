using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order GetById(int id);
        Order Create(Order entity);
        Order Update(Order entity);
    }
}
