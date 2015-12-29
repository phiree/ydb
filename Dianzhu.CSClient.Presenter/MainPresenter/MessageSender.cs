using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using PHSuit;
using Dianzhu.CSClient.IInstantMessage;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 消息发送. 如果用户在线,则发送xmpp消息, 否则,发送推送消息.
    /// </summary>
    public partial class MainPresenter
    {
        
        public void SendMessageWithPush(IInstantMessage.InstantMessage im,ReceptionChat chat)
        {
            string deviceToken = "8de76c196a605120db39ab58373edf159c1301b43659bd129fcf72b696e2a26c";
            bool isOnline = ClientState.customerOnlineList.Contains(chat.To);
            if (isOnline)
            {
                im.SendMessage(chat);
            }
            else {
                Push.pushNotifications(deviceToken, chat.MessageBody, @"files\aps_development_Mark.p12", 1);
            }
        }
         
    }
}
