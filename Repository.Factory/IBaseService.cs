using Repository.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Factory
{
  public   interface IBaseService 
    {
        IRepository<TEntity> CreateService<TEntity>() where TEntity : class, new();
    }
}
