﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Common;

namespace Ydb.InstantMessage.DomainModel.Reception
{
    public class IMUserStatus : Entity<Guid>
    {
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
        /// 最后一次修改的时间
        /// </summary>
        public virtual DateTime LastModifyTime { get; set; }
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

    /// <summary>
    /// 用户的IM 状态
    /// </summary>
    public class IMUserStatusArchieve : Entity<Guid>
    {
        /// <summary>
        /// 构造函数
        /// 初始化存档时间
        /// </summary>
        public IMUserStatusArchieve()
        {
            ArchieveTime = DateTime.Now;
        }

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
