using Business.IService.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Entity.Models.Base;
using Repository.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Service.Base
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        public UserService(IRepositoryFactory repositoryFactory, DbContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }
     
        public List<UserEntity> GetList(Expression<Func<UserEntity, bool>> expression)
        {
            return this.Repository.Where(expression).ToList();
        }

        public void Insert(UserEntity entity)
        {
            this.Repository.Add(entity);
        }

        public Task<bool> Register(UserEntity user)
        {
            this.Repository.Add(user);
        }
    }
}
