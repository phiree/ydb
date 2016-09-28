using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.CSClient.ViewModel;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 显示聊天内容
    /// </summary>
    public interface IViewChatCustomer
    {
        Guid CurrentCSId { set; }
        VMChat VMChat { set; }
    }



}
