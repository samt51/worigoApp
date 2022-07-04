using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IGeneralServiceAndServiceService
    {
        List<GeneralServiceAndService> GetAll();
        GeneralServiceAndService GetById(int id);
        void Create(GeneralServiceAndService entity);
        void Update(GeneralServiceAndService entity);
    }
}
