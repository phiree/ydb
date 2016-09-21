using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsc = agsXMPP.protocol.client;
using agsXMPP;
using System.Text.RegularExpressions;
using agsXMPP.protocol.client;
using System.IO;
using System.Net;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
namespace Dianzhu.DemoClient
{
    public partial class FmMain : Form
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.DemoClient");
        string csId;
        string csDisplayName;
        string customerId;
        string pwd;
        string orderID {
            get { return tbxOrderId.Text; }
            set { tbxOrderId.Text = value; }
        }
        public FmMain(string[] args)
        {

            InitializeComponent();

            if (!string.IsNullOrEmpty(csId))
            {
                lblAssignedCS.Text = csDisplayName;
            }


            GlobalViables.XMPPConnection.OnLogin += new agsXMPP.ObjectHandler(XMPPConnection_OnLogin);
            GlobalViables.XMPPConnection.OnMessage += new agsc.MessageHandler(XMPPConnection_OnMessage);
            GlobalViables.XMPPConnection.OnError += new ErrorHandler(XMPPConnection_OnError);
            GlobalViables.XMPPConnection.OnAuthError += new XmppElementHandler(XMPPConnection_OnAuthError);
            GlobalViables.XMPPConnection.OnSocketError += new ErrorHandler(XMPPConnection_OnSocketError);
            GlobalViables.XMPPConnection.OnIq += XMPPConnection_OnIq;
            GlobalViables.XMPPConnection.OnStreamError += XMPPConnection_OnStreamError;
            GlobalViables.XMPPConnection.OnClose += XMPPConnection_OnClose;

            if (args.Length == 2)
            {
                Login(args[0], args[1]);
            }
        }

        private void XMPPConnection_OnClose(object sender)
        {
            lblLoginStatus.Text = "Closed";
            lblAssignedCS.Text = string.Empty;
        }

        private void XMPPConnection_OnStreamError(object sender, agsXMPP.Xml.Dom.Element e)
        {
           // MessageBox.Show(e.ToString());
        }

        private void XMPPConnection_OnIq(object sender, IQ iq)
        {
           // MessageBox.Show(iq.ToString());
        }

        void XMPPConnection_OnSocketError(object sender, Exception ex)
        {
            //MessageBox.Show("socket error");
        }
        private void GetCustomerInfo(string userName,string password)
        {
            string postData = string.Format(@"{{ 
                    ""protocol_CODE"": ""USM001005"", //用户信息获取
                    ""ReqData"": {{ 
                                ""email"": ""{0}"", 
                                ""pWord"": ""{1}"", 
                                }}, 
                    ""stamp_TIMES"": ""{2}"", 
                    ""serial_NUMBER"": ""00147001015869149751"" 
                }}", userName, password, (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString());
            Newtonsoft.Json.Linq.JObject result = API.GetApiResult(postData);
            if (result["state_CODE"].ToString() != "009000")
            {
                MessageBox.Show(result["err_Msg"].ToString());
                return;
            }
            customerId = result["RespData"]["userObj"]["userID"].ToString();
            pwd = password;
            this.Text= result["RespData"]["userObj"]["name"].ToString();

        }
        public void GetCustomerService(string username,string password ,string manualAssignedCS)
        {
            string apiRequest = string.Format(@"{{ // 
                     ""protocol_CODE"": ""ORM002001"", 
                    ""ReqData"": {{ 
                    ""userID"": ""{0}"", 
                    ""pWord"": ""{1}"", 
                    ""orderID"": ""{2}"",
""manualAssignedCsId"":""{4}""   
                    }}, 
                    ""stamp_TIMES"": ""{3}"", 
                    ""serial_NUMBER"": ""00147001015869149751"" 
                }}", customerId,
                password,
                orderID,
                (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString(),
                tbxManualAssignedCS.Text
                );
            GlobalViables.log.Debug("请求参数：" + apiRequest);
            Newtonsoft.Json.Linq.JObject result = API.GetApiResult(apiRequest);
            GlobalViables.log.Debug("请求结果：" + result.ToString());
            string state_Code = result["state_CODE"].ToString();
            if (state_Code != "009000")
            {
                string errMsg = result["err_Msg"].ToString();
                MessageBox.Show(errMsg);
                lblAssignedCS.Text = "客服离线";
                throw new Exception(state_Code + "_" + errMsg);
            }
            lblAssignedCS.Text = csDisplayName = result["RespData"]["cerObj"]["alias"].ToString();// result["RespData"]["cerObj"]["alias"].ToString();

            csId = result["RespData"]["cerObj"]["userID"].ToString();// result["RespData"]["cerObj"]["userID"].ToString();
            //customerId = result["RespData"]["cerObj"]["userID"].ToString();
           orderID = result["RespData"]["orderID"].ToString();
          
        }
        public void GetCustomerService(string username, string password)
        {
            GetCustomerService( username, password,string.Empty);
        }
        void XMPPConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            MessageBox.Show("用户名/密码有误");
        }

        void XMPPConnection_OnError(object sender, Exception ex)
        {
            MessageBox.Show("OnError");
        }

        void XMPPConnection_OnMessage(object sender, agsc.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                return;
            }
            string log = msg.Body;
            var ext = msg.SelectSingleElement("ext");
            Debug.Assert(ext != null, "xmpp标准协议的消息，没有ext节点" + msg);
            if (ext == null)
            { return; }
            agsXMPP.Xml.Dom.Element msgType = msg.SelectSingleElement("ext");
            switch (msgType.Namespace.ToLower())
            {
                case "ihelper:chat:text":

                    break;
                case "ihelper:chat:media": break;
                case "ihelper:notice:cer:change":
                    csId = msgType.SelectSingleElement("cerObj").GetAttribute("userID");
                    csDisplayName = msgType.SelectSingleElement("cerObj").GetAttribute("alias");
                    lblAssignedCS.Text = csDisplayName;
                    break;
                case "ihelper:notice:cer:online":
                    
                    GetCustomerService(GlobalViables.XMPPConnection.Username, GlobalViables.XMPPConnection.Password);
                    break;
                case "ihelper:notice:draft:new":
                    orderID = msgType.SelectSingleElement("orderID").Value;
                    break;
            }
            AddLog(msg);
            GlobalViables.log.Debug("Message Received:" + msg.InnerXml);

        }

        void XMPPConnection_OnLogin(object sender)
        {
            try
            {
                GlobalViables.log.Debug("登录openfire成功");
                if (InvokeRequired)
                {
                    BeginInvoke(new ObjectHandler(XMPPConnection_OnLogin), new object[] { sender });
                    return;
                }


                XmppClientConnection conn = (XmppClientConnection)sender;
                GlobalViables.log.Debug("开始请求客服");
                GetCustomerService(conn.Username, conn.Password);

                lblAssignedCS.Text = csDisplayName;



                Presence p = new Presence(ShowType.chat, "Online");
                p.Type = PresenceType.available;
                p.To = new Jid(csId + "@" + GlobalViables.ServerName + "/" + Model.Enums.enum_XmppResource.YDBan_CustomerService);
                p.From = new Jid(customerId + "@" + GlobalViables.ServerName + "/" + Model.Enums.enum_XmppResource.YDBan_DemoClient);
                GlobalViables.XMPPConnection.Send(p);

                lblLoginStatus.Text = "登录成功";
                tbxUserName.Text = conn.Username;
            }
            catch (Exception e)
            {
                GlobalViables.log.Debug("错误：" + e.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            GlobalViables.log.Debug("登录开始");
            GlobalViables.log.Debug("Close connection");
            //bug:关闭当前登录后，不能再监听原来的事件。
            //GlobalViables.XMPPConnection.Close();//关闭当前登录
            Login(tbxUserName.Text, tbxPwd.Text);
            

        }
        private void Login(string userName, string password)
        {
            
            
            GlobalViables.log.Debug("获取用户信息开始");
            GetCustomerInfo(userName,password);
            GlobalViables.log.Debug("连接openfire服务器");
            GlobalViables.XMPPConnection.Open(customerId, password);
        }
        /// <summary>
        /// 增加一条聊天记录
        /// </summary>
        /// <param name="message"></param>
        void AddLog(agsc.Message message)
        {
            GlobalViables.log.Debug("message:" + message.InnerXml);
            tbxLog.AppendText(DateTime.Now.ToString()+":"+message.ToString() + Environment.NewLine);
            //if (csDisplayName == null)
            //{
            //    GetCustomerService();
            //    lblAssignedCS.Text = csDisplayName;

            //    Presence p = new Presence(ShowType.chat, "Online");
            //    p.Type = PresenceType.available;
            //    p.To = new Jid(csId + "@" + GlobalViables.ServerName);
            //    p.From = new Jid(customerId + "@" + GlobalViables.ServerName);
            //    GlobalViables.XMPPConnection.Send(p);
            //}
           
            string user = StringHelper.EnsureNormalUserName(message.From.User);
            string body = message.Body;
            string messageType = message.GetAttribute("MessageType");

            Label lblTime = new Label();
            Label lblFrom = new Label();
            Label lblMessage = new Label();
            FlowLayoutPanel pnlOneChat = new FlowLayoutPanel();
            pnlOneChat.Controls.AddRange(new Control[] { lblFrom, lblMessage });
            pnlOneChat.FlowDirection = FlowDirection.LeftToRight;
            _AutoSize(pnlOneChat);
            pnlOneChat.Dock = DockStyle.Top;
            foreach (Label c in pnlOneChat.Controls)
            {
                c.BorderStyle = BorderStyle.FixedSingle;
            }
            lblFrom.Text = user;
            lblMessage.Text = body;
            _AutoSize(lblMessage);
            pnlOneChat.Width = pnlChat.Size.Width - 16;

            //显示多媒体信息.

            string msgType = message.SelectSingleElement("ext").Namespace;
            switch (msgType.ToLower())
            {
                case "ihelper:chat:text":

                    break;
                case "ihelper:chat:media":
                    string mediaUrl = message.SelectSingleElement("ext").SelectSingleElement("msgObj").GetAttribute("url");
                    string mediaType = message.SelectSingleElement("ext").SelectSingleElement("msgObj").GetAttribute("type");
                    switch (mediaType)
                    {
                        case "image":
                            PictureBox pb = new PictureBox();
                            pb.Load(mediaUrl);
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                            pb.Click += Pb_Click;
                            pb.Size = new System.Drawing.Size(100, 100);
                            pnlOneChat.Controls.Add(pb);
                            break;
                        case "voice":
                            Button btnAudio = new Button();
                            btnAudio.Text = "播放音频(待实现)";
                            btnAudio.Tag = mediaUrl;
                            pnlOneChat.Controls.Add(btnAudio);
                            break;
                        case "video":
                            Button btnVideo = new Button();
                            btnVideo.Text = "播放视频(待实现)";
                            pnlOneChat.Controls.Add(btnVideo);
                            break;
                    }
                    break;
                case "ihelper:notice:cer:change":
                    csDisplayName = message.SelectSingleElement("ext").SelectSingleElement("cerObj").GetAttribute("alias");
                    lblMessage.Text += "客服更换为:" + csDisplayName;
                    break;
                case "ihelper:notice:system":
                    lblMessage.Text += "通知:" + message.Body;
                    break;
                case "ihelper:chat:orderobj":
                    //UC_PushService pushService = new UC_PushService();
                    Label lblServiceID = new Label
                    {
                        Text = "orderID:" + message.SelectSingleElement("ext").SelectSingleElement("orderID").InnerXml,
                    };
                    Button btnServiceSure = new Button
                    {
                        Text = "确认服务",
                        Tag = message
                    };
                    btnServiceSure.Click += BtnServiceSure_Click;
                    //pushService.LoadData(((ReceptionChatPushService)chat).PushedServices[0]);
                    //pushService.FlowDirection = FlowDirection.LeftToRight;
                    //chat.MessageBody = string.Empty;
                    pnlOneChat.Controls.Add(lblServiceID);
                    pnlOneChat.Controls.Add(btnServiceSure);
                    break;
                case "ihelper:notice:draft:new":
                    lblMessage.Text += "通知:新的草稿单" +orderID;
                    break;

                case "ihelper:notice:order":
                    //lblMessage.Text += message.Body;
                    break;

                default:
                    log.Warn("未知的聊天类型:" + message);
                    MessageBox.Show("未知的聊天类型");
                    break;
            }

            pnlChat.Controls.Add(pnlOneChat);
            pnlChat.ScrollControlIntoView(pnlOneChat);
            //GlobalViables.log.Debug("message send:" + message.InnerXml);

        }

        private void BtnServiceSure_Click(object sender, EventArgs e)
        {
            agsc.Message message = (agsc.Message)((Button)sender).Tag;

            string apiRequest = string.Format(
                @"{{ 
                    ""protocol_CODE"": ""ORM001008"", 
                    ""ReqData"": 
                    {{ 
                        ""userID"": ""{0}"", 
                        ""pWord"": ""{1}"", 
                        ""orderID"": ""{2}"",
                        ""svcID"":""{3}""   
                    }}, 
                    ""stamp_TIMES"": ""{4}"", 
                    ""serial_NUMBER"": ""00147001015869149751"" 
                }}",
                customerId,
                pwd,
                message.SelectSingleElement("ext").SelectSingleElement("orderID").InnerXml,
                message.SelectSingleElement("ext").SelectSingleElement("svcObj").GetAttribute("svcID"),
                (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString()
                );
            GlobalViables.log.Debug("请求参数：" + apiRequest);
            Newtonsoft.Json.Linq.JObject result = API.GetApiResult(apiRequest);
            GlobalViables.log.Debug("请求结果：" + result.ToString());
            string state_Code = result["state_CODE"].ToString();
            if (state_Code != "009000")
            {
                string errMsg = result["err_Msg"].ToString();
                MessageBox.Show(errMsg);
                lblAssignedCS.Text = "客服离线";
                throw new Exception(state_Code + "_" + errMsg);
            }

            ((Button)sender).Enabled = false;
        }

        private void Pb_Click(object sender, EventArgs e)
        {

            new ShowFullImage(((PictureBox)sender).Image).Show();


        }

        private Label CreateNewLabel(string text)
        {
            Label lbl = new Label();
            lbl.Text = text;
            return lbl;
        }



        private void btnSend_Click(object sender, EventArgs e)
        {
            agsc.Message message = new MessageBuilder()
                .Create(customerId, csId, tbxMessage.Text, orderID)
                .BuildText();
            message.Id = Guid.NewGuid().ToString();
            GlobalViables.XMPPConnection.Send(message);
            AddLog(message);

        }

        private void tbxMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSend.PerformClick();
            }
        }

        private void FmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Presence p = new Presence(ShowType.chat, "Offline");
            p.Type = PresenceType.unavailable;
            p.To = new Jid(csId + "@" + GlobalViables.ServerName + "/" + Model.Enums.enum_XmppResource.YDBan_CustomerService);
            p.From = new Jid(StringHelper.EnsureOpenfireUserName(tbxUserName.Text) + "@" + GlobalViables.ServerName + "/" + Model.Enums.enum_XmppResource.YDBan_DemoClient);
            GlobalViables.XMPPConnection.Send(p);
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            var result
                = SelectFile("ChatImage","image");
            if (!string.IsNullOrEmpty(result))
            {
                agsc.Message msgnew = new MessageBuilder()
                    .Create(customerId, csId, string.Empty, orderID)
                    .BuildMedia("image",   result)
                    ;

                GlobalViables.XMPPConnection.Send(msgnew);
                AddLog(msgnew);
            }

        }
        private string SelectFile(string domainName,string fileType)
        {
            string result = string.Empty;
            if (dlgSelectPic.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream fs = dlgSelectPic.OpenFile() as FileStream;
                string fileExtension = Path.GetExtension(dlgSelectPic.FileName);
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                string s = Convert.ToBase64String(bytes);

                result = GlobalViables.MediaGetUrl + MediaServer.HttpUploader.Upload(GlobalViables.MediaUploadUrl, s, domainName, fileType);

            }
            return  result;
        }

        private void _AutoSize(Control c)
        {
            c.Size = new Size(0, 0);
            if (c is Label)
            {
                ((Label)c).AutoSize = true;
            }
            if (c is Panel)
            {
                ((Panel)c).AutoSize = true;
            }


        }
        private void AssignCS()
        {

        }
        private void btnGetCS_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnAudio_Click(object sender, EventArgs e)
        {
            var result
               = SelectFile("ChatAudio", "voice");
            if (!string.IsNullOrEmpty(result))
            {
                agsc.Message msgnew = new MessageBuilder()
                    .Create(customerId, csId, string.Empty, orderID)
                    .BuildMedia("voice",  result)
                    ;

                GlobalViables.XMPPConnection.Send(msgnew);
                AddLog(msgnew);
            }

        }

        private void btnOnlineUsers_Click(object sender, EventArgs e)
        {
            IQ iq = new MessageBuilder().BuildIq();
            GlobalViables.XMPPConnection.Send(iq);
        }

        private void btnAdvList_Click(object sender, EventArgs e)
        {

            AdvList adv = new AdvList();
            adv.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            GlobalViables.XMPPConnection.Close();
        }
    }
}
