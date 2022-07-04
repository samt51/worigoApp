using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Worigo.DataAccess.Abstrack.Repository
{
    public interface IRepositoryDesignPattern<T>
    {
        T GetById(int id);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        void Create(T entity);
        void Update(T entity);
    }
}
