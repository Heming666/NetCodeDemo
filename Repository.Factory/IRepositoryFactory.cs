using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Factory
{
   public interface IRepositoryFactory
    {
        IRepository<TEntity> CreateRepository<TEntity>(DbContext mydbcontext) where TEntity : class, new();
    }
}
