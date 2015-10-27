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
namespace Dianzhu.DemoClient
{
    public partial class FmMain : Form
    {
        string csId;
        string csDisplayName;
        string customerId;
        string orderID;
        public FmMain()
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
        }

        void XMPPConnection_OnSocketError(object sender, Exception ex)
        {
            MessageBox.Show("socket error");
        }
        private void GetCustomerInfo(string userName)
        {
            string postData = string.Format(@"{{ 
                    ""protocol_CODE"": ""USM001005"", //用户信息获取
                    ""ReqData"": {{ 
                                ""email"": ""{0}"", 
                                ""pWord"": ""{1}"", 
                                }}, 
                    ""stamp_TIMES"": ""1490192929212"", 
                    ""serial_NUMBER"": ""00147001015869149751"" 
                }}", tbxUserName.Text, tbxPwd.Text);
            Newtonsoft.Json.Linq.JObject result = API.GetApiResult(postData);
            customerId = result["RespData"]["userObj"]["userID"].ToString();

        }
        public void GetCustomerService()
        {
            Newtonsoft.Json.Linq.JObject result = API.GetApiResult(
                string.Format(@"{{ // 
                     ""protocol_CODE"": ""ORM002001"", 
                    ""ReqData"": {{ 
                    ""userID"": ""{0}"", 
                    ""pWord"": ""{1}"", 
                    ""orderID"": ""{2}""   
                    }}, 
                    ""stamp_TIMES"": ""1490192929212"", 
                    ""serial_NUMBER"": ""00147001015869149751"" 
                }}", customerId, tbxPwd.Text, tbxOrderId.Text));
            string state_Code = result["state_CODE"].ToString();
            if (state_Code != "009000")
            {
                string errMsg = result["err_Msg"].ToString();
                MessageBox.Show(errMsg);
                lblAssignedCS.Text = "客服离线";
                throw new Exception(state_Code + "_" + errMsg);
            }
            csDisplayName = "e@e.e";// result["RespData"]["cerObj"]["alias"].ToString();

            csId = "d53147d9-1a1e-4df8-b4d0-a4f90129ad25";// result["RespData"]["cerObj"]["userID"].ToString();
            customerId = result["RespData"]["cerObj"]["userID"].ToString();
            tbxOrderId.Text = orderID = result["RespData"]["orderID"].ToString();
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
            string msgType = msg.SelectSingleElement("ext").Namespace;
            switch (msgType.ToLower())
            {
                case "ihelper:chat:text":

                    break;
                case "ihelper:chat:media": break;
                case "ihelper:cer:change":
                    csId = msg.SelectSingleElement("ext").SelectSingleElement("cerObj").GetAttribute("UserID");
                    csDisplayName = msg.SelectSingleElement("ext").SelectSingleElement("cerObj").GetAttribute("alias");
                    lblAssignedCS.Text = csDisplayName;
                    break;
                case "ihelper:cer:notce":
                    break;
            }
            AddLog(msg);

        }

        void XMPPConnection_OnLogin(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(XMPPConnection_OnLogin), new object[] { sender });
                return;
            }
            //if (string.IsNullOrEmpty(csName))
            //{
            //    csName = "e||e.e";
            //}


            //客户自己的信息
            //GetCustomerInfo(tbxUserName.Text);
            GetCustomerService();
            lblAssignedCS.Text = csDisplayName;


            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            p.To = new Jid(csId + "@" + GlobalViables.ServerName);
            p.From = new Jid(customerId + "@" + GlobalViables.ServerName);
            GlobalViables.XMPPConnection.Send(p);

            lblLoginStatus.Text = "登录成功";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            GlobalViables.XMPPConnection.Close();
            string userName = tbxUserName.Text;
            string userNameForOpenfire = userName;
            if (Regex.IsMatch(userName, @"^[^\.@]+@[^\.@]+\.[^\.@]+$"))
            {
                userNameForOpenfire = userName.Replace("@", "||");
            }
            GetCustomerInfo(userName);
            GlobalViables.XMPPConnection.Open(customerId, tbxPwd.Text);

        }
        /// <summary>
        /// 增加一条聊天记录
        /// </summary>
        /// <param name="message"></param>
        void AddLog(agsc.Message message)
        {
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
                    string mediaUrl = message.SelectSingleElement("ext").SelectSingleElement("MsgObj").GetAttribute("url");
                    string mediaType = message.SelectSingleElement("ext").SelectSingleElement("MsgObj").GetAttribute("type");
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
                case "ihelper:cer:change":
                    csDisplayName = message.SelectSingleElement("ext").SelectSingleElement("cerObj").GetAttribute("alias");
                    lblMessage.Text += "客服更换为:" + csDisplayName;
                    break;
                case "ihelper:cer:notce":
                    lblMessage.Text += "通知:" + message.Body;
                    break;
            }

            pnlChat.Controls.Add(pnlOneChat);
            pnlChat.ScrollControlIntoView(pnlOneChat);


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
            p.To = new Jid(csId + "@" + GlobalViables.ServerName);
            p.From = new Jid(StringHelper.EnsureOpenfireUserName(tbxUserName.Text) + "@" + GlobalViables.ServerName);
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


                result = GlobalViables.MediaGetUrl + MediaServer.HttpUploader.Upload(
                  GlobalViables.MediaUploadUrl, 
                  s, dlgSelectPic.FileName,
                  domainName, fileType);

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
    }
}
