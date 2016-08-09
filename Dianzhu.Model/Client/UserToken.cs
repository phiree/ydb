using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    public class UserToken : DDDCommon.Domain.Entity<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual string UserID { get; set; }

        /// <summary>
        /// 用户Token
        /// </summary>
        public virtual string Token { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get; set; }
        
        /// <summary>
        /// 有效状态
        /// </summary>
        public virtual int Flag { get; set; }
    }
}
