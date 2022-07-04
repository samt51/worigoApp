using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class GeneralServiceAndServiceManager : IGeneralServiceAndServiceService
    {
        private readonly IGeneralServiceAndServiceDal _generalServiceAndServiceDal;
        public GeneralServiceAndServiceManager(IGeneralServiceAndServiceDal generalServiceAndServiceDal)
        {
            _generalServiceAndServiceDal = generalServiceAndServiceDal;
        }
        public void Create(GeneralServiceAndService entity)
        {
            _generalServiceAndServiceDal.Create(entity);
        }

        public List<GeneralServiceAndService> GetAll()
        {
            return _generalServiceAndServiceDal.GetAll();
        }

        public GeneralServiceAndService GetById(int id)
        {
            return _generalServiceAndServiceDal.GetById(id);
        }

        public void Update(GeneralServiceAndService entity)
        {
            _generalServiceAndServiceDal.Update(entity);
        }
    }
}
