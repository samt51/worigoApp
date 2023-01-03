using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IContentsOfFoodService
    {
        List<ContentsOfFood> GetAll();
        ContentsOfFood GetById(int id);
        ContentsOfFood Create(ContentsOfFood entity);
        ContentsOfFood Update(ContentsOfFood entity);
    }
}
