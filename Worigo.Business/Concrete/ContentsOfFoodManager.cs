using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class ContentsOfFoodManager : IContentsOfFoodService
    {
        private readonly IContentsOfFoodDal _contentsOfFoodDal;
        public ContentsOfFoodManager(IContentsOfFoodDal contentsOfFoodDal)
        {
            _contentsOfFoodDal = contentsOfFoodDal;
        }

        public ContentsOfFood Create(ContentsOfFood entity)
        {
         return   _contentsOfFoodDal.Create(entity);
        }

        public List<ContentsOfFood> GetAll()
        {
            return _contentsOfFoodDal.GetAll();
        }

        public ContentsOfFood GetById(int id)
        {
            return _contentsOfFoodDal.GetById(id);
        }

        public ContentsOfFood Update(ContentsOfFood entity)
        {
           return  _contentsOfFoodDal.Update(entity);
        }
    }
}
