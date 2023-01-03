using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class CompaniesManager : ICompaniesService
    {
        private readonly ICompaniesDal _companiesDal;
        public CompaniesManager(ICompaniesDal companiesDal)
        {
            _companiesDal= companiesDal;    
        }
        public Companies Create(Companies entity)
        {
           return _companiesDal.Create(entity);
        }

        public List<Companies> GetAll()
        {
            return _companiesDal.GetAll();
        }

        public Companies GetById(int id)
        {
            return _companiesDal.GetById(id);
        }

        public Companies Update(Companies entity)
        {
          return  _companiesDal.Update(entity);
        }
    }
}
