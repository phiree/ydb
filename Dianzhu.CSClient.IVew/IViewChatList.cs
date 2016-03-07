using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 聊天信息列表
    /// </summary>
    public interface IViewChatList
    {
        
        IList<ReceptionChat> ChatList { set; }
        void AddOneChat(ReceptionChat chat);
    }
    
}
