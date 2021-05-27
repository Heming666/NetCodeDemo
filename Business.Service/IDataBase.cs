using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Service
{
    public abstract class IDataBase<TEntity> where TEntity : class
    {
        public readonly DbContext db;
        public IDataBase(DbContext dbContext)
            {
            db = dbContext;
            //db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;//不需要更新从数据库中检索到的实体
        }
        public virtual IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> expression)
        {
            return db.Set<TEntity>().Where(expression);
        }
        public virtual IQueryable<TEntity> IQueryable()
        {
            return db.Set<TEntity>().AsQueryable();
        }
        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> expression)
        {
            return db.Set<TEntity>().Where(expression).ToList();
        }
        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await db.Set<TEntity>().Where(expression).ToListAsync();
        }
        public virtual async Task<List<TEntity>> GetListAsync(IOrderedQueryable<TEntity> OrderQuery)
        {
            return await OrderQuery.ToListAsync();
        }
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await db.AddAsync(entity);
            return db.SaveChanges();
        }

    }
}
