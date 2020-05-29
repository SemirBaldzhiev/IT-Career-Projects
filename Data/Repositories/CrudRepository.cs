using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public abstract class CrudRepository<T>
       where T : class
    {
        private readonly GameDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public CrudRepository(GameDbContext dbContext, DbSet<T> dbSet)
        {
            this._dbContext = dbContext;
            this._dbSet = dbSet;
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet
                .Where(predicate)
                .AsQueryable();
        }

        public T GetById(int id)
        {
            return this._dbSet.Find(id);
        }

        public T GetOne(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.FirstOrDefault(predicate);
        }

        public void Add(T entity)
        {
            this._dbSet.Add(entity);
            this._dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;
            this._dbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            this._dbSet.Remove(entity);
            this._dbContext.SaveChanges();
        }

        public int Count()
        {
            return this._dbSet.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Count(predicate);
        }
    }
}
