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

        public ServiceDiandian()
        {
            InitializeComponent();

            GlobalViables.XMPPConnection.OnLogin += XMPPConnection_OnLogin;
            GlobalViables.XMPPConnection.OnMessage += XMPPConnection_OnMessage;
            GlobalViables.XMPPConnection.OnError += XMPPConnection_OnError;
            GlobalViables.XMPPConnection.OnAuthError += XMPPConnection_OnAuthError;
            GlobalViables.XMPPConnection.OnSocketError += XMPPConnection_OnSocketError;
            GlobalViables.XMPPConnection.OnIq += XMPPConnection_OnIq;
            GlobalViables.XMPPConnection.OnStreamError += XMPPConnection_OnStreamError;
            GlobalViables.XMPPConnection.OnClose += XMPPConnection_OnClose;
        }

        private void XMPPConnection_OnClose(object sender)
        {
            //MessageBox.Show("Connection has been Closed");
        }

        private void XMPPConnection_OnStreamError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            //MessageBox.Show(e.ToString());
        }

        private void XMPPConnection_OnIq(object sender, IQ iq)
        {
            //MessageBox.Show(iq.ToString());
        }

        private void XMPPConnection_OnSocketError(object sender, Exception ex)
        {
            //MessageBox.Show("socket error");
        }

        private void XMPPConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            //MessageBox.Show("用户名/密码有误");
        }

        private void XMPPConnection_OnError(object sender, Exception ex)
        {
            //MessageBox.Show("OnError");
        }

        private void XMPPConnection_OnMessage(object sender, agsc.Message msg)
        {
            //if (InvokeRequired)
            //{
            //    BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
            //    return;
            //}
            orderID = msg.SelectSingleElement("ext").SelectSingleElement("orderID").Value;
            customerId = msg.From.User;
            string log = msg.Body;
            string msgType = msg.SelectSingleElement("ext").Namespace;
            switch (msgType.ToLower())
            {
                case "ihelper:chat:text":

                    break;
                case "ihelper:chat:media": break;
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
                                }}, 
                    ""stamp_TIMES"": ""{6}"", 
                    ""serial_NUMBER"": ""00147001015869149751"" 
                }}", msg.Id, msg.To.User, msg.From.User, msg.Body, msgType, orderID, (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString());
            Newtonsoft.Json.Linq.JObject result = API.GetApiResult(postData);
            string code = result["state_CODE"].ToString();
            if (code != "009000")
            {
                return;
            }

            //自动回复消息
            string reply = "当前没有客服在线，请留言..";
            agsc.Message message = new MessageBuilder().Create(csId, customerId, reply, orderID).BuildText();
            message.Id = Guid.NewGuid().ToString();
            GlobalViables.XMPPConnection.Send(message);
            //AddLog(message);
        }

        //private void AddLog(agsc.Message msg)
        //{
        //    throw new NotImplementedException();
        //}

        private void XMPPConnection_OnLogin(object sender)
        {
            //if (InvokeRequired)
            //{
            //    BeginInvoke(new ObjectHandler(XMPPConnection_OnLogin), new object[] { sender });
            //    return;
            //}

            //lblLoginStatus.Text = "登录成功";
        }

        private void GetCustomerInfo(string userName, string pwd)
        {
            string postData = string.Format(@"{{ 
                    ""protocol_CODE"": ""USM001005"", //用户信息获取
                    ""ReqData"": {{ 
                                ""email"": ""{0}"", 
                                ""pWord"": ""{1}"", 
                                }}, 
                    ""stamp_TIMES"": ""{2}"", 
                    ""serial_NUMBER"": ""00147001015869149751"" 
                }}", userName, pwd, (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString());
            Newtonsoft.Json.Linq.JObject result = API.GetApiResult(postData);
            csId = result["RespData"]["userObj"]["userID"].ToString();
        }

        protected override void OnStart(string[] args)
        {
            GlobalViables.XMPPConnection.Close();//关闭当前登录
            string userName = "diandian@ydban.cn";
            string password = "diandian";
            //string userName = "aa@aa.aa";
            //string password = "123456";
            string userNameForOpenfire = userName;
            if (Regex.IsMatch(userName, @"^[^\.@]+@[^\.@]+\.[^\.@]+$"))
            {
                userNameForOpenfire = userName.Replace("@", "||");
            }
            GetCustomerInfo(userName, password);
            GlobalViables.XMPPConnection.Open(csId, password);
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
