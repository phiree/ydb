using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.DomainModel.Chat.Enums
{
    public enum ChatType
    {

        Chat,//聊天信息,需要持久化
        Notice,//推送信息,不需要持久化
    }
}
