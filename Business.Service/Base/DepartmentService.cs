using Business.IService.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Entity.Models;
using Repository.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Base
{
    public class DepartmentService : IDataBase<UserEntity>, IDepartmentService
    {
        public DepartmentService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
