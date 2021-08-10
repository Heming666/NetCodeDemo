using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.AuthModel
{
   public class JWTConfig
    {
        /// <summary>
        /// 发起者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string IssuerSigningKey { get; set; }
        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public int AccessTokenExpiresMinutes { get; set; }

    }
}
