using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    public class RefreshToken
    {
        /// <summary>
        /// RefreshToken值，且为主键
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 客户端的使用用户
        /// </summary>
        public virtual string Subject { get; set; }
        /// <summary>
        /// 客户端ID
        /// </summary>
        public virtual string ClientId { get; set; }
        /// <summary>
        /// RefreshToken发布时间
        /// </summary>
        public virtual DateTime IssuedUtc { get; set; }
        /// <summary>
        /// RefreshToken到期时间
        /// </summary>
        public virtual DateTime ExpiresUtc { get; set; }
        /// <summary>
        /// RefreshToken保护票据
        /// </summary>
        public virtual string ProtectedTicket { get; set; }
    }
}
