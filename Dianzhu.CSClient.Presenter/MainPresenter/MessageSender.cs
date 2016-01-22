using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using PHSuit;
using Dianzhu.CSClient.IInstantMessage;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 消息发送. 如果用户在线,则发送xmpp消息, 否则,发送推送消息.
    /// </summary>
    public partial class MainPresenter
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter");
        public void SendMessageWithPush(IInstantMessage.InstantMessage im,ReceptionChat chat)
        {
            //string deviceToken = "8de76c196a605120db39ab58373edf159c1301b43659bd129fcf72b696e2a26c";
            im.SendMessage(chat);
            // get resource of customer
            // if iso
            // get parameters
            // IPush pushIos=new PushIOS( para1,para,2pama4);
            //pushIos.push(message);
            IMUserStatus user = bllIMUserStatus.GetIMUSByUserId(chat.To.Id);            
            bool isUserNull = user == null;
            Debug.Assert(!isUserNull, "错误！用户状态表中不存在该用户！");
            if (isUserNull)
            {
                ilog.Error("错误！用户状态表中不存在该用户！");
                return;
            }
            bool isOnline = user.Status == Model.Enums.enum_UserStatus.available;
            if (!isOnline)
            {
                string target = user.ClientName.Split('_')[1];
                switch (target.ToLower())
                {
                    case "ios":
                        string url = Dianzhu.Config.Config.GetAppSetting("PushServerUrl");
                        DeviceBind deviceBindObj = bllDeviceBind.getDevBindByUserID(chat.To);
                        Debug.Assert(deviceBindObj != null, "错误！设备绑定表中不存在该用户！");
                        bool isValidToken = deviceBindObj.AppToken != null;
                        if (!isValidToken)
                        {
                            ilog.Error("错误！设备绑定表中不存在该用户的apptoken！");
                            return;
                        }
                        string deviceToken = deviceBindObj.AppToken;

                        var respData = new NameValueCollection();
                        respData.Add("deviceToken", deviceToken);
                        respData.Add("pushNum", "1");
                        respData.Add("notificaitonSound", "default");
                        respData.Add("message", chat.MessageBody);

                        HttpHelper.CreateHttpRequest(url.ToString(), "post", respData);
                        break;
                    case "android":

                        return;

                    case "democlient":

                        return;
                    default:
                        return;
                }
            }            
        }
         
    }
}
