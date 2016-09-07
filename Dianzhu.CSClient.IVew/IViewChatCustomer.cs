using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 显示聊天内容
    /// </summary>
    public interface IViewChatCustomer
    {
        DZMembership CurrentCS { set; }
        ReceptionChat Chat { set; }
        string CustomerAvatar { set; }
    }



}
