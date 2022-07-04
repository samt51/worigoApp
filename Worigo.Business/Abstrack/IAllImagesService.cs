using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IAllImagesService
    {
        List<AllImages> GetAll();
        AllImages GetById(int id);
        void Create(AllImages entity);
        void Update(AllImages entity);
    }
}
