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
    public class UserService : Repository<UserEntity>, IUserService
    {
        public UserService( DbContext mydbcontext) : base( mydbcontext)
        {
        }
     
        public async Task<List<UserEntity>> GetList(Expression<Func<UserEntity, bool>> expression)
        {
            return await Where(expression).ToListAsync();
        }

        public async Task<int> Insert(UserEntity entity)
        {
          return await  InsertAsync(entity);
        }

        public async Task<int> Register(UserEntity user)
        {
            return await InsertAsync(user);
        }
    }
}
