using System.Linq;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfResetPasswordForCodeDal: EfRepositoryDal<ResetPasswordForCode, DataContext>, IResetPasswordForCodeDal
    {
      
        ResetPasswordForCode IResetPasswordForCodeDal.GetCodeByCode(int code)
        {
            using (var db = new DataContext())
            {
                var cod = db.ResetPasswordForCodes.Where(x => x.code == code).FirstOrDefault();
                if (cod != null)
                    return cod;
                throw new NotFoundException("This is Code Not Found");

            }
        }
    }
}
