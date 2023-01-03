using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Worigo.DataAccess.Abstrack.Repository
{
    public interface IRepositoryDesignPattern<T>
    {
        T GetById(int id);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);  
    }
}
