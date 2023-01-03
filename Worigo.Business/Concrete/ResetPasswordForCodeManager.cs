using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class ResetPasswordForCodeManager : IResetPasswordForCodeService
    {
        private readonly IResetPasswordForCodeDal _resetPasswordForCodeDal;
        public ResetPasswordForCodeManager(IResetPasswordForCodeDal resetPasswordForCodeDal)
        {
            _resetPasswordForCodeDal = resetPasswordForCodeDal;
        }

        public ResetPasswordForCode Create(ResetPasswordForCode entity)
        {
           return _resetPasswordForCodeDal.Create(entity);
        }

        public ResetPasswordForCode Delete(ResetPasswordForCode entity)
        {
           return _resetPasswordForCodeDal.Delete(entity);
        }

        public ResetPasswordForCode GetById(int id)
        {
            return _resetPasswordForCodeDal.GetById(id);
        }

        public ResetPasswordForCode GetCodeByCode(int code)
        {
            return _resetPasswordForCodeDal.GetCodeByCode(code);
        }

        public ResetPasswordForCode Update(ResetPasswordForCode entity)
        {
          return  _resetPasswordForCodeDal.Update(entity);
        }
    }
}
