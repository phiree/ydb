using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 用户的IM 状态
    /// </summary>
    public class IMUserStatusArchieve
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 口传递进来的原始ID(可能包含服务器域名, 资源名等)
        /// </summary>
        public virtual string UserIdRaw { get; set; }
        /// <summary>
        /// 从原始ID中提取出来的用户ID
        /// </summary>
        public virtual Guid UserID { get; set; }
        /// <summary>
        /// 用户的状态
        /// </summary>
        public virtual enum_UserStatus Status { get; set; }
        /// <summary>
        /// 存档的时间
        /// </summary>
        public virtual DateTime ArchieveTime { get; set; }
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public virtual string IpAddress { get; set; }
        /// <summary>
        /// 登录openfire的地址
        /// </summary>
        public virtual string OFIpAddress { get; set; }
        /// <summary>
        /// 登录客户端名称
        /// </summary>
        public virtual string ClientName { get; set; }
    }
}
