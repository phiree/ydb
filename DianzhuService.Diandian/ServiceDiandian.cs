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
        string csDisplayName;
        string customerId;
        string orderID;
      static   log4net.ILog log = GlobalViables.log;
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
            //MessageBox.Show(e.ToString());
        }

        private void XMPPConnection_OnIq(object sender, IQ iq)
        {
            log.Debug("XMPPConnection_OnIq:"+iq.ToString());
            //MessageBox.Show(iq.ToString());
        }

        private void XMPPConnection_OnSocketError(object sender, Exception ex)
        {
            log.Debug("XMPPConnection_OnSocketError"+ex.Source+Environment.NewLine+"CallStack"+ex.StackTrace);
            //MessageBox.Show("socket error");
        }

        private void XMPPConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            log.Debug("用户名/密码有误");
            //MessageBox.Show("用户名/密码有误");
        }

        private void XMPPConnection_OnError(object sender, Exception ex)
        {
            log.Debug("XMPPConnection_OnError"+ex.Message);
            //MessageBox.Show("OnError");
        }

        private void XMPPConnection_OnMessage(object sender, agsc.Message msg)
        {
            log.Debug("XMPPConnection_OnMessage:"+msg.ToString());
            //if (InvokeRequired)
            //{
            //    BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
            //    return;
            //}
            orderID = msg.SelectSingleElement("ext").SelectSingleElement("orderID").Value;
            customerId = msg.From.User;
            string body = msg.Body;
            string msgObj_url = String.Empty;
            string msgObj_type = String.Empty;
            string msgType = msg.SelectSingleElement("ext").Namespace;
            switch (msgType.ToLower())
            {
                case "ihelper:chat:text":

                    break;
                case "ihelper:chat:media":
                    msgObj_url = msg.SelectSingleElement("ext").SelectSingleElement("MsgObj").GetAttribute("url");
                    msgObj_type = msg.SelectSingleElement("ext").SelectSingleElement("MsgObj").GetAttribute("type");
                    break;
                case "ihelper:notice:cer:change":
                    customerId = msg.SelectSingleElement("ext").SelectSingleElement("cerObj").GetAttribute("UserID");
                    csDisplayName = msg.SelectSingleElement("ext").SelectSingleElement("cerObj").GetAttribute("alias");
                    //lblAssignedCS.Text = csDisplayName;
                    break;
            }
            //AddLog(msg);

            //ReceptionChat chat = new ReceptionChat();
            //chat.Id = msg.Id;
            //chat.To = msg.To.User;
            //chat.From = msg.From.User;
            //chat.Body = msg.Body;
            //chat.Ext = msgType;
            //chat.OrderId = orderID;

            string postData = string.Format(@"{{ 
                    ""protocol_CODE"": ""SYS001001"", //用户信息获取
                    ""ReqData"": {{ 
                                ""id"": ""{0}"", 
                                ""to"": ""{1}"", 
                                ""from"": ""{2}"", 
                                ""body"": ""{3}"", 
                                ""ext"": ""{4}"", 
                                ""orderID"": ""{5}"", 
                                ""msgObj_url"": ""{6}"", 
                                ""msgObj_type"": ""{7}"",
                           }}, 
                    ""stamp_TIMES"": ""{8}"", 
                    ""serial_NUMBER"": ""00147001015869149751"" 
                }}", msg.Id, msg.To.User, msg.From.User, msg.Body, msgType, orderID, msgObj_url, msgObj_type, (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString());
            Newtonsoft.Json.Linq.JObject result = API.GetApiResult(postData);
            string code = result["state_CODE"].ToString();
            if (code != "009000")
            {
                return;
            }

            //自动回复消息
            string reply = "当前没有客服在线，请留言..";
            csId = msg.To.User;
            agsc.Message message = new MessageBuilder().Create(csId, customerId, reply, orderID).BuildText();
            message.Id = Guid.NewGuid().ToString();
            log.Debug("Sending message:" + message.ToString());
            GlobalViables.XMPPConnection.Send(message);
            //AddLog(message);
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
        }

       

        protected override void OnStart(string[] args)
        {
            GlobalViables.XMPPConnection.Close();//关闭当前登录
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

        protected override void OnStop()
        {
        }
    }

}
