using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IResetPasswordForCodeService
    {
        ResetPasswordForCode GetById(int id);
        ResetPasswordForCode Create(ResetPasswordForCode entity);
        ResetPasswordForCode Delete(ResetPasswordForCode entity);
        ResetPasswordForCode GetCodeByCode(int code);
        ResetPasswordForCode Update(ResetPasswordForCode entity);
    }
}
