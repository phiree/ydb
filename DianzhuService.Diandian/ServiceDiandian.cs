using System.ServiceProcess;
using agsc = agsXMPP.protocol.client;
using agsXMPP.protocol.client;
using agsXMPP;
using System;
using System.Text.RegularExpressions;

namespace DianzhuService.Diandian
{
    public partial class ServiceDiandian : ServiceBase
    {
        string csId;
        string customerId;
        string orderID;

        static log4net.ILog log = GlobalViables.log;

        int csOnLine = 0;

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
                switch (msgType.ToLower())
                {
                    case "ihelper:chat:text":

                        break;
                    case "ihelper:chat:media":
                        msgObj_url = msg.SelectSingleElement("ext").SelectSingleElement("msgObj").GetAttribute("url");
                        msgObj_type = msg.SelectSingleElement("ext").SelectSingleElement("msgObj").GetAttribute("type");
                        break;
                    case "ihelper:notice:cer:online":
                        csOnLine++;
                        log.Debug("收到客服上线通知,当前在线客服人数:"+csOnLine);                        
                        return;
                    case "ihelper:notice:cer:offline":
                        if (csOnLine > 0)
                        {
                            csOnLine--;
                        }
                        log.Debug("收到客服离线通知,当前在线客服人数:"+csOnLine);                        
                        return;
                    default:
                        log.Warn("请求的类型" + msgType.ToLower() + "无法处理，直接返回");
                        return;
                }

                if (csOnLine == 1)
                {
                    //发送客服离线消息给用户
                    string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
                    string noticeDraftNew = string.Format(@"<message xmlns = ""jabber:client"" type = ""headline"" id = ""{2}"" to = ""{0}"" from = ""{1}"">
                                                  <active xmlns = ""http://jabber.org/protocol/chatstates""></active><body>客服已上线</body><ext xmlns=""ihelper:notice:cer:online""></ext></message>",
                                                      msg.From.User + "@" + server + "/" + Dianzhu.Model.Enums.enum_XmppResource.YDBan_User, msg.To.User, Guid.NewGuid());
                    GlobalViables.XMPPConnection.Send(noticeDraftNew);
                    log.Debug("send message:" + noticeDraftNew);
                    return;
                }

                orderID = msg.SelectSingleElement("ext").SelectSingleElement("orderID").Value;
                //AddLog(msg);

                //ReceptionChat chat = new ReceptionChat();
                //chat.Id = msg.Id;
                //chat.To = msg.To.User;
                //chat.From = msg.From.User;
                //chat.Body = msg.Body;
                //chat.Ext = msgType;
                //chat.OrderId = orderID;

                //string postData = string.Format(@"{{ 
                //    ""protocol_CODE"": ""SYS001001"", //用户信息获取
                //    ""ReqData"": {{ 
                //                ""id"": ""{0}"", 
                //                ""to"": ""{1}"", 
                //                ""from"": ""{2}"", 
                //                ""body"": ""{3}"", 
                //                ""ext"": ""{4}"", 
                //                ""orderID"": ""{5}"", 
                //                ""msgObj_url"": ""{6}"", 
                //                ""msgObj_type"": ""{7}"",
                //                ""from_resource"": ""{9}"",
                //           }}, 
                //    ""stamp_TIMES"": ""{8}"", 
                //    ""serial_NUMBER"": ""00147001015869149751"" 
                //}}", msg.Id, msg.To.User, msg.From.User, msg.Body, msgType, orderID, msgObj_url.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"), ""), msgObj_type, (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString(), msg.From.Resource);
                //log.Debug("开始获取用户信息");
                //Newtonsoft.Json.Linq.JObject result = API.GetApiResult(postData);

                //if (result == null)
                //{
                //    log.Error("获取失败，返回值为空");
                //}
                //string code = result["state_CODE"].ToString();
                //if (code != "009000")
                //{
                //    return;
                //}

                //自动回复消息
                string reply = "当前没有客服在线，请留言..";
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

        //private void AddLog(agsc.Message msg)
        //{
        //    throw new NotImplementedException();
        //}

        private void XMPPConnection_OnLogin(object sender)
        {
            log.Debug("登录完成");
            //if (InvokeRequired)
            //{
            //    BeginInvoke(new ObjectHandler(XMPPConnection_OnLogin), new object[] { sender });
            //    return;
            //}

            //lblLoginStatus.Text = "登录成功";

            //每隔一段时间给服务发送一个ping,防止连接超时.
            System.Timers.Timer tmHeartBeat = new System.Timers.Timer();
            tmHeartBeat.Elapsed += TmHeartBeat_Elapsed;
            tmHeartBeat.Interval = 5 * 60 * 1000;
            tmHeartBeat.Start();
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

        protected override void OnStart(string[] args)
        {
           // GlobalViables.XMPPConnection.Close();//关闭当前登录
            log.Debug("打开服务器连接..");
            string loginId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            string pwd = Dianzhu.Config.Config.GetAppSetting("DiandianLoginPwd");
            GlobalViables.XMPPConnection.Open(loginId, pwd);
          
            //lblLoginStatus.Text = "正在登录，请稍候...";

            ////每隔一段时间给服务发送一个ping,防止连接超时.
            //System.Timers.Timer tmHeartBeat = new System.Timers.Timer();
            //tmHeartBeat.Elapsed += TmHeartBeat_Elapsed;
            //tmHeartBeat.Interval = 5 * 60 * 1000;
            //tmHeartBeat.Start();
            ////重新启动服务
            //using (System.ServiceProcess.ServiceController control = new ServiceController("Diandian"))
            //{
            //    if (control.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
            //    {
            //        //MessageBox.Show("服务启动......");
            //        control.Start();
            //        //lblMessage.Text = "服务已经启动......";
            //    }
            //}
        }
 
    }

}
