using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Factory
{
    public interface IDataBase
    {
        IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> expression);
         Task<IQueryable<T>> IQueryableAsync<T>(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetList<T>(Expression<Func<T, bool>> expression);
         Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> expression);
    }
}
