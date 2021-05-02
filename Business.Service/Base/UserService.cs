using Business.IService.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Entity.Models;
using Repository.Factory;
using System;

namespace Business.Service.Base
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        public UserService(IRepositoryFactory repositoryFactory, DbContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }
        public void aaa()
        {
           var int1= this.Repository.Count();
            var int2 =this.CreateService<DepartmentEntity>().Count();
            var int3 = this.Repository.Where(a => !string.IsNullOrWhiteSpace(a.UserName)).ToListAsync();
        }
    }
}
