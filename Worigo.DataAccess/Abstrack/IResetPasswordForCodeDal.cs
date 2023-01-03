using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IResetPasswordForCodeDal: IRepositoryDesignPattern<ResetPasswordForCode>
    {
        ResetPasswordForCode GetCodeByCode(int code);
    }
}
