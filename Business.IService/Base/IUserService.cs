using Repository.Entity.Models;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.IService.Base
{
    public interface IUserService
    {
        List<UserEntity> GetList(Expression<Func<UserEntity, bool>> expression);
        void Insert(UserEntity entity);
    }
}
