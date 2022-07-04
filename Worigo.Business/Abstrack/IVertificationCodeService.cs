using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IVertificationCodeService
    {
        List<VertificationCodes> GetAll();
        VertificationCodes GetById(int id);
        void Create(VertificationCodes entity);
        void Update(VertificationCodes entity);
    }
}
