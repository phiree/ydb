using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.DomainModel.Chat.Enums
{
    public enum ChatTarget
    {
        /// <summary>
        /// 与平台客服聊天类型
        /// </summary>
        cer = 1,
        /// <summary>
        /// 与商家客服聊天类型
        /// </summary>
        store = 2,
        /// <summary>
        /// 查询所有的类型
        /// </summary>
        all = 4,
        /// <summary>
        /// 用户
        /// </summary>
        user = 8,
        /// <summary>
        /// 系统
        /// </summary>
        system = 16,
    }
}
