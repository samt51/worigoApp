using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface ICompaniesService
    {
        List<Companies> GetAll();
        Companies GetById(int id);
        Companies Create(Companies entity);
        Companies Update(Companies entity);
    }
}
