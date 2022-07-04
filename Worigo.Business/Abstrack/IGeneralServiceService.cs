using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IGeneralServiceService
    {
        List<GeneralService> GetAll();
        GeneralService GetById(int id);
        void Create(GeneralService entity);
        void Update(GeneralService entity);
    }
}
