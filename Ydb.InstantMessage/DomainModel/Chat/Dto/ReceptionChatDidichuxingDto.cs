using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat.Enums;
using Ydb.InstantMessage.DomainModel.Enums;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    /// <summary>
    /// IM,信息,多媒体
    /// </summary>
    public class ReceptionChatDidichuxingDto : ReceptionChatDto
    {
        /// <summary>
        /// 出发地纬度
        /// </summary>
        public virtual string Fromlat { get; protected internal set; }
        /// <summary>
        /// 出发地经度
        /// </summary>
        public virtual string Fromlng { get; protected internal set; }
        /// <summary>
        /// 出发地地址
        /// </summary>
        public virtual string Fromaddr { get; protected internal set; }
        /// <summary>
        /// 出发地名称
        /// </summary>
        public virtual string Fromname { get; protected internal set; }
        /// <summary>
        /// 目的地纬度
        /// </summary>
        public virtual string Tolat { get; protected internal set; }
        /// <summary>
        /// 目的地经度
        /// </summary>
        public virtual string Tolng { get; protected internal set; }
        /// <summary>
        /// 目的地地址
        /// </summary>
        public virtual string Toaddr { get; protected internal set; }
        /// <summary>
        /// 目的地名称
        /// </summary>
        public virtual string Toname { get; protected internal set; }
        /// <summary>
        /// 乘客手机号
        /// </summary>
        public virtual string Phone { get; protected internal set; }
    }
}
