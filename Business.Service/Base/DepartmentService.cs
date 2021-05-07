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
    public class DepartmentService : BaseService<UserEntity>, IDepartmentService
    {
        public DepartmentService(IRepositoryFactory repositoryFactory, DbContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public List<DepartmentEntity> GetList(Expression<Func<DepartmentEntity, bool>> Exception)
        {
            return new List<DepartmentEntity>();
        }
    }
}
