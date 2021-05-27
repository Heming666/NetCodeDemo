using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Factory
{
    public class RepositoryTest<T> : IRepository<T> where T : class, new()
    {
        private DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private readonly string _connStr;
        public RepositoryTest(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();
            this._connStr = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public IQueryable<T> Entities => throw new NotImplementedException();

        public IQueryable<T> TrackEntities => throw new NotImplementedException();

        public T Add(T entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> entitys, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public void Delete(bool isSave = false, params T[] entitys)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> where, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Distinct(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefault<TOrder>(Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll<TOrder>(Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public T GetById<Ttype>(Ttype id)
        {
            throw new NotImplementedException();
        }

        public Ttype Max<Ttype>(Expression<Func<T, Ttype>> column)
        {
            throw new NotImplementedException();
        }

        public Ttype Max<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Ttype Min<Ttype>(Expression<Func<T, Ttype>> column)
        {
            throw new NotImplementedException();
        }

        public Ttype Min<Ttype>(Expression<Func<T, Ttype>> column, Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public TType Sum<TType>(Expression<Func<T, TType>> selector, Expression<Func<T, bool>> where) where TType : new()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public void Update(bool isSave = true, params T[] entitys)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Where<TOrder>(Func<T, bool> where, Func<T, TOrder> order, int pageIndex, int pageSize, out int count, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order, int pageIndex, int pageSize, out int count, bool isDesc = false)
        {
            throw new NotImplementedException();
        }
    }
}
