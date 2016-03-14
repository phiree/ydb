using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 聊天消息发送
    /// </summary>
    public interface IViewChatSend
    {
        
    
      
        event SendTextClick SendTextClick;
        event SendImageClick SendImageClick;
        string MessageText { get; set; }
        
    }
    public delegate void SendTextClick();
    public delegate void SendImageClick(byte[] fileData, string domainType, string mediaType);



}
