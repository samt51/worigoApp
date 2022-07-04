using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class VertificationCodeManager : IVertificationCodeService
    {
        private readonly IVertificationCodeDal _vertificationCodeDal;
        public VertificationCodeManager(IVertificationCodeDal vertificationCodeDal)
        {
            _vertificationCodeDal = vertificationCodeDal;
        }
        public void Create(VertificationCodes entity)
        {
            _vertificationCodeDal.Create(entity);
        }

        public List<VertificationCodes> GetAll()
        {
            return _vertificationCodeDal.GetAll();
        }

        public VertificationCodes GetById(int id)
        {
            return _vertificationCodeDal.GetById(id);
        }

        public void Update(VertificationCodes entity)
        {
            _vertificationCodeDal.Update(entity);
        }
    }
}
