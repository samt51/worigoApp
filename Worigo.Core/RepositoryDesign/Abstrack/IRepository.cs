using System.Collections.Generic;

namespace Worigo.Core.RepositoryDesign.Abstrack
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
    }
}
