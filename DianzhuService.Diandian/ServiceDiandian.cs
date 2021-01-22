using System.ServiceProcess;
using agsc = agsXMPP.protocol.client;
using agsXMPP.protocol.client;
using agsXMPP;
using System;
using System.Text.RegularExpressions;
using Ydb.Common;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;
 
using System.Collections.Generic;
using System.Linq;
using Dianzhu.RequestRestful;

namespace DianzhuService.Diandian
{
    public partial class ServiceDiandian : ServiceBase
    {
        string csId;
        string customerId;
        string orderID;

        static log4net.ILog log = GlobalViables.log;

        Dictionary<string, int> csOnlin = new Dictionary<string, int>();
        

        public ServiceDiandian()
        {
            InitializeComponent();
            log.Debug("StartService");
            GlobalViables.XMPPConnection.OnLogin += XMPPConnection_OnLogin;
            GlobalViables.XMPPConnection.OnMessage += XMPPConnection_OnMessage;
            GlobalViables.XMPPConnection.OnError += XMPPConnection_OnError;
            GlobalViables.XMPPConnection.OnAuthError += XMPPConnection_OnAuthError;
            GlobalViables.XMPPConnection.OnSocketError += XMPPConnection_OnSocketError;
            GlobalViables.XMPPConnection.OnIq += XMPPConnection_OnIq;
            GlobalViables.XMPPConnection.OnStreamError += XMPPConnection_OnStreamError;
            GlobalViables.XMPPConnection.OnClose += XMPPConnection_OnClose;
        }
        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
        private void XMPPConnection_OnClose(object sender)
        {
            log.Debug("XMPPConnection_OnClose");
            //MessageBox.Show("Connection has been Closed");
        }

        private void XMPPConnection_OnStreamError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            log.Debug("XMPPConnection_OnStreamError"+e.ToString());
            this.Stop();
            //MessageBox.Show(e.ToString());
        }

        private void XMPPConnection_OnIq(object sender, IQ iq)
        {
            log.Debug("XMPPConnection_OnIq:"+iq.ToString());
            //MessageBox.Show(iq.ToString());
        }
        string errMsg = string.Empty;
        private void XMPPConnection_OnSocketError(object sender, Exception ex)
        {
            log.Error("XMPPConnection_OnSocketError" + ex.Source + Environment.NewLine + "CallStack" + ex.StackTrace);
            this.Stop();
        }

        private void XMPPConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            log.Error("用户名/密码有误");
            this.Stop();
            
        }

        private void XMPPConnection_OnError(object sender, Exception ex)
        {
            log.Error("XMPPConnection_OnError" +ex.Message);
            this.Stop();
        }
       

        private void XMPPConnection_OnMessage(object sender, agsc.Message msg)
        {
            try
            {
                log.Debug("XMPPConnection_OnMessage:" + msg.ToString());
                //if (InvokeRequired)
                //{
                //    BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                //    return;
                //}

                customerId = msg.From.User;
                string body = msg.Body ?? string.Empty;
                string msgObj_url = String.Empty;
                string msgObj_type = String.Empty;
                var extNode = msg.SelectSingleElement("ext");
                if (extNode == null)
                {
                    log.Error("没有ext节点,跳过");
                    return;
                }
                string msgType = extNode.Namespace;



                string reply = string.Empty;// "当前没有客服在线，请留言..";

                switch (msgType.ToLower())
                {
                    case "ihelper:chat:text":
                        //判断是否是特定格式的消息，返回 true 或 false
                        
                        reply = CheckMessage(body);
                        break;
                    case "ihelper:chat:media":
                        msgObj_url = msg.SelectSingleElement("ext").SelectSingleElement("msgObj").GetAttribute("url");
                        msgObj_type = msg.SelectSingleElement("ext").SelectSingleElement("msgObj").GetAttribute("type");
                        break;
                    case "ihelper:notice:cer:online":
                        string onlineCsAreaCode=msg.SelectSingleElement("ext").SelectSingleElement("areaCode").Value;
                        CSOnline(onlineCsAreaCode);
                       
                        log.Debug("收到客服上线通知,当前在线客服人数:"+ FormatCSOnlineStatus());                        
                        return;
                    case "ihelper:notice:cer:offline":
                        string offlineCsAreaCode = msg.SelectSingleElement("ext").SelectSingleElement("areaCode").Value;
                        CSOffline(offlineCsAreaCode);

                        log.Debug("收到客服离线通知,当前在线客服:"+ FormatCSOnlineStatus());                        
                        return;
                    default:
                        log.Warn("请求的类型" + msgType.ToLower() + "无法处理，直接返回");
                        return;
                }
                string userAreaCode = GetUserAreaCode(customerId);
                if (string.IsNullOrEmpty(userAreaCode))
                {
                    log.Error("获取用户AreaCode失败");
                    return;
                }
                if (GetAreaCsCount(userAreaCode)>0)
                {
                    //发送客服离线消息给用户
                    string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
                    string noticeDraftNew = string.Format(@"<message xmlns = ""jabber:client"" type = ""headline"" id = ""{2}"" to = ""{0}"" from = ""{1}"">
                                                  <active xmlns = ""http://jabber.org/protocol/chatstates""></active><body>客服已上线</body><ext xmlns=""ihelper:notice:cer:online""></ext></message>",
                                                      msg.From.User + "@" + server + "/" + enum_XmppResource.YDBan_User, msg.To.User, Guid.NewGuid());
                    GlobalViables.XMPPConnection.Send(noticeDraftNew);
                    log.Debug("send message:" + noticeDraftNew);
                    return;
                }

                orderID = msg.SelectSingleElement("ext").SelectSingleElement("orderID").Value;
                
                //自动回复消息
                csId = msg.To.User;
                agsc.Message message = new MessageBuilder().Create(csId, customerId, reply, orderID).BuildText();
                message.Id = Guid.NewGuid().ToString();
                message.To = msg.From;
                log.Debug("Sending message:" + message.ToString());
                GlobalViables.XMPPConnection.Send(message);
              
               
            }
            catch (Exception e)
            {
                PHSuit.ExceptionLoger.ExceptionLog(log, e);
            }
        }
        private int GetAreaCsCount(string areaCode)
        {
            var qry = csOnlin.Keys.Where(x => x.Contains(areaCode.Substring(0, 4)));
            //区级 可被分配到 市级
            if (qry.Count() > 0)
            {
                return csOnlin[qry.First()];
            }
            else { return 0; }
        }

        private string FormatCSOnlineStatus()
        {
            string strStatus = string.Empty;
            foreach (var item in csOnlin)
            {
                strStatus += "区域:" + item.Key + ",数量:" + item.Value+";";
            }
            return strStatus;
        }
        private void CSOnline(string areaCode)
        {
            if (csOnlin.ContainsKey(areaCode))
            {
                csOnlin[areaCode]++;
            }
            else
            {
                csOnlin[areaCode] = 1;
            }
             
        }
        private void CSOffline(string areaCode)
        {
            if (csOnlin.ContainsKey(areaCode))
            {
                int count = csOnlin[areaCode];
                if(count>0)
                { 
                csOnlin[areaCode]--;
                }
            }
             

        }

        Dianzhu.RequestRestful.IRequestRestful restReq = Bootstrap.Container.Resolve<Dianzhu.RequestRestful.IRequestRestful>();
        string restApiBaseUrl = Dianzhu.Config.Config.GetAppSetting("RestApiSite");
        string token = string.Empty;
        private void XMPPConnection_OnLogin(object sender)
        {
            log.Debug("登录完成");
 
            System.Timers.Timer tmHeartBeat = new System.Timers.Timer();
            tmHeartBeat.Elapsed += TmHeartBeat_Elapsed;
            tmHeartBeat.Interval = 5 * 60 * 1000;
            tmHeartBeat.Start();



            RequestResponse resp =  restReq.RequestRestfulApiForAuthenticated(restApiBaseUrl, loginId, pwd);
            if (resp.code)
            {
                token = resp.data;
            }
            else
            {
                log.Error("认证失败");
            }
          
        }

        private void TmHeartBeat_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IQ iqHeartBeat = new IQ(IqType.get, GlobalViables.XMPPConnection.MyJID, Dianzhu.Config.Config.GetAppSetting("ImServer"));
            var pingNode = new agsXMPP.Xml.Dom.Element("ping");
            pingNode.Namespace = "urn:xmpp:ping";
            iqHeartBeat.AddChild(pingNode);

            GlobalViables.XMPPConnection.Send(iqHeartBeat);
            log.Debug("HeartBeat" + iqHeartBeat.ToString());
        }
        string loginId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
        string pwd = Dianzhu.Config.Config.GetAppSetting("DiandianLoginPwd");
        protected override void OnStart(string[] args)
        {
            
            log.Debug("打开服务器连接..");
            
            GlobalViables.XMPPConnection.Open(loginId, pwd);
 
        }

        public string CheckMessage(string MessageBody )
        {
            Ydb.Common.Infrastructure.IHttpRequest httpRequest = Bootstrap.Container.Resolve<Ydb.Common.Infrastructure.IHttpRequest>();
            string strResult = "当前区域没有客服在线，请留言..";
            Regex reg = new Regex(System.Configuration.ConfigurationManager.AppSettings["CheckRegex"].ToString());
            log.Debug("MessageBody="+ MessageBody+ ",reg="+reg.ToString());
            if (reg.IsMatch(MessageBody))
            {
                log.Debug("MessageBody=" + MessageBody + ",IsMatch=true");
                try
                {
                    string strUri = System.Configuration.ConfigurationManager.AppSettings["CheckUri"].ToString();

                    log.Debug("MessageBody=" + MessageBody + ",Uri="+ strUri);
                    //strResult = PHSuit.HttpHelper.CreateHttpRequest(strUri, "get","","");
                    //Newtonsoft.Json.Linq.JObject joData = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject("{'reward_code':'"+ MessageBody + "'}");

                    //var respDataWeChat = new NameValueCollection();
                    //respDataWeChat.Add("api_code", "DR001002");
                    //respDataWeChat.Add("stamp_times", "");
                    //respDataWeChat.Add("data", MessageBody);

                    string value = "{\r\n\tapi_code : \"DR001002\", \r\n\tdata : \t{\r\n\t\treward_code : \""+ MessageBody + "\",\r\n\t} ,\r\n\tstamp_times : \"1490192929222\"\r\n}";
                    strResult = httpRequest.CreateHttpRequest(strUri, "post", value,"");

                    log.Debug("MessageBody=" + MessageBody + ",httpRequest=" + strResult);
                    Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(strResult);
                    strResult = jo["err_Msg"].ToString();
                }
                catch
                {
                    strResult = "发生错误，请联系客服处理..";
                }
            }
            log.Debug("MessageBody=" + MessageBody + ",IsMatch=false");
            return strResult;
        }

        public string GetUserAreaCode(string userId)
        {
            string areaCode = string.Empty;
            Ydb.Common.Infrastructure.IHttpRequest httpRequest = Bootstrap.Container.Resolve<Ydb.Common.Infrastructure.IHttpRequest>();

            RequestResponse resp= restReq.RequestRestfulApiForUserCity(restApiBaseUrl, userId, token);
            if (resp.code)
            {
                areaCode = resp.data;
            }
            return areaCode;
           
        }
    }

}
