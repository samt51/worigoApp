using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class GeneralServiceManager : IGeneralServiceService
    {
        private readonly IGeneralServiceDal _generalServiceDal;
        public GeneralServiceManager(IGeneralServiceDal generalServiceDal)
        {
            _generalServiceDal = generalServiceDal;
        }
        public void Create(GeneralService entity)
        {
            _generalServiceDal.Create(entity);
        }

        public List<GeneralService> GetAll()
        {
            return _generalServiceDal.GetAll(x => x.isDeleted == false);
        }

        public GeneralService GetById(int id)
        {
            return _generalServiceDal.GetById(id);
        }

        public void Update(GeneralService entity)
        {
            _generalServiceDal.Update(entity);
        }
    }
}
