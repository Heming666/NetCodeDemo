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
        Task<List<UserEntity>> GetList(Expression<Func<UserEntity, bool>> expression);
        Task<int> Insert(UserEntity entity);
        Task<int> Register(UserEntity user);
        /// <summary>
        ///  验证账户是否存在
        ///  <code>
        /// 参数 <paramref name="account"/>是账户
        ///  <para><see cref="IUserService"/>   </para>
        /// <![CDATA[ 
        /// 存在则返回true
        /// 不存在则返回false
        /// ]]></code>
        /// </summary>
        /// <param name="account">账户Account</param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> ExistsAccount(string account);
        Task<UserEntity> GetEntity(Expression<Func<UserEntity, bool>> expression);
    }
}
