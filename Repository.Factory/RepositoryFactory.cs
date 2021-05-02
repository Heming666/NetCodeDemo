using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Factory
{
    public class RepositoryFactory : IRepositoryFactory 
    {
        public IRepository<TEntity> CreateRepository<TEntity>(DbContext mydbcontext) where TEntity : class,new()
        {
            return new Repository<TEntity>(mydbcontext);
        }
    }
}
