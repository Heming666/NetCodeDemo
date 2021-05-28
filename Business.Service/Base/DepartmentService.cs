using Business.IService.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Entity.Models.Base;
using Repository.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Base
{
    public class DepartmentService : Repository<DepartmentEntity>, IDepartmentService
    {
        public DepartmentService(DbContext mydbcontext) : base(mydbcontext)
        {
        }

        public async Task<List<DepartmentEntity>> GetList(Expression<Func<DepartmentEntity, bool>> expression)
        {
            return await Where(expression).ToListAsync();
        }

        public async Task<int> Insert(DepartmentEntity entity)
        {
          return  await InsertAsync(entity);
        }
    }
}
