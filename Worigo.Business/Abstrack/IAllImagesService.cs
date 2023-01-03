using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IAllImagesService
    {
        List<AllImages> GetAll();
        AllImages GetById(int id);
        AllImages Create(AllImages entity);
        AllImages Update(AllImages entity);
    }
}
