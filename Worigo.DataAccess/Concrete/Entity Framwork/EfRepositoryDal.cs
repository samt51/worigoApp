using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack.Repository;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfRepositoryDal<T, TEntity> : IRepositoryDesignPattern<T> where T : class where TEntity : DbContext, new()
    {
        public T Create(T entity)
        {
            using (var db = new TEntity())
            {
                var data = db.Set<T>().Add(entity);
                db.SaveChanges();
                return data.Entity;
            }
        }

        public T Delete(T entity)
        {
            using (var db = new TEntity())
            {
                var data = db.Set<T>().Remove(entity);
                db.SaveChanges();
                return data.Entity;
            }
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using (var db = new TEntity())
            {
                return filter == null
                    ? db.Set<T>().ToList()
                    : db.Set<T>().Where(filter).ToList();
            }
        }
        public T GetById(int id)
        {
            using (var db = new TEntity())
            {
                var find = db.Set<T>().Find(id);
                if (find == null)
                {
                    throw new ClientSideException($"{typeof(T).Name}  Not Found");
                }
                return find;
            }
        }
        public T Update(T entity)
        {
            using (var db = new TEntity())
            {
                var data = db.Set<T>().Update(entity);
                db.SaveChanges();
                return data.Entity;

            }
        }
    }
}