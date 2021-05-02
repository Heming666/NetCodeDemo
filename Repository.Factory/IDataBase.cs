using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Factory
{
    public abstract class IDataBase<TEntity> where TEntity : class
    {
        private readonly DbContext db;
        public IDataBase()
        {
        }
        public IDataBase(DbContext dbContext)
            {
            db = dbContext;
            //db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;//不需要更新从数据库中检索到的实体
        }
        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> expression)
        {
            return db.Set<TEntity>().Where(expression);
        }
        public IQueryable<TEntity> IQueryable()
        {
            return db.Set<TEntity>().AsQueryable();
        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> expression)
        {
            return db.Set<TEntity>().Where(expression).ToList();
        }
        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return db.Set<TEntity>().Where(expression).ToListAsync();
        }
    }
}
