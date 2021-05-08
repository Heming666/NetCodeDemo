using Business.IService.Customer;
using Microsoft.EntityFrameworkCore;
using Repository.Entity.Models.Consume;
using Repository.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Customer
{
    public class ComsumeService : BaseService<ConsumeEntity>, IComsumeService
    {
        public ComsumeService(IRepositoryFactory repositoryFactory, DbContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public void Add(ConsumeEntity entity)
        {
            this.Repository.Add(entity);
        }

        public List<ConsumeEntity> GetList(Expression<Func<ConsumeEntity, bool>> expression)
        {
            return this.Repository.Where(expression).ToList();
        }
    }
}
