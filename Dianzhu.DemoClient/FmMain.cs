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

namespace Dianzhu.DemoClient
{
    public partial class FmMain : Form
    {
        string csId = string.Empty;

        public FmMain()
        {
            InitializeComponent();

            GlobalViables.XMPPConnection.OnLogin += new agsXMPP.ObjectHandler(XMPPConnection_OnLogin);
            GlobalViables.XMPPConnection.OnMessage += new agsc.MessageHandler(XMPPConnection_OnMessage);
            GlobalViables.XMPPConnection.OnError += new ErrorHandler(XMPPConnection_OnError);
            GlobalViables.XMPPConnection.OnAuthError += new XmppElementHandler(XMPPConnection_OnAuthError);
        }

        void XMPPConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            MessageBox.Show("用户名/密码有误");
        }

        void XMPPConnection_OnError(object sender, Exception ex)
        {
            // MessageBox.Show("聊天服务器错误");
        }

        void XMPPConnection_OnMessage(object sender, agsc.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                return;
            }
            string log = msg.Body;
            foreach (var att in msg.Attributes.Keys)
            {
                if (new string[] { "to", "from", "type" }.Contains(att))
                {
                    continue;
                }
                if (att.ToString() == "service_id")
                {
                    log += "[" + att + ":" + msg.Attributes[att] + "]";
                }
                if (att.ToString() == "service_name")
                {
                    log += "[" + att + ":" + msg.Attributes[att] + "]";
                }
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
            if (string.IsNullOrEmpty(csId))
            {
                csId = "e||e.e";
            }
            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            p.To = csId + "@" + GlobalViables.ServerName;
            p.From = StringHelper.EnsureOpenfireUserName(tbxUserName.Text) + "@" + GlobalViables.ServerName;
            GlobalViables.XMPPConnection.Send(p);
            lblLoginStatus.Text = "登录成功";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //为该用户分配客服
            string userName = tbxUserName.Text;
            string userNameForOpenfire = userName;
            if (Regex.IsMatch(userName, @"^[^\.@]+@[^\.@]+\.[^\.@]+$"))
            {
                userNameForOpenfire = userName.Replace("@", "||");
            }
            GlobalViables.XMPPConnection.Open(userNameForOpenfire, tbxPwd.Text);

        }
        void AddLog(agsc.Message message)
        {
           string user = StringHelper.EnsureNormalUserName(message.From.User);
         string   body = message.Body;
          string  mediaUrl = message.GetAttribute("media");
          string serviceId = message.GetAttribute("service_id");
             string serviceName = message.GetAttribute("service_name");

            Label lblTime = new Label();
            Label lblFrom = new Label();
            Label lblMessage = new Label();
            FlowLayoutPanel pnlOneChat = new FlowLayoutPanel();
            pnlOneChat.Controls.AddRange(new Control[] { lblMessage });
            pnlOneChat.FlowDirection = FlowDirection.LeftToRight;
            _AutoSize(pnlOneChat);
            pnlOneChat.Dock = DockStyle.Top;
            foreach (Label c in pnlOneChat.Controls)
            {
                c.BorderStyle = BorderStyle.FixedSingle;
            }
            lblFrom.Text =user;
            lblMessage.Text = body;
            _AutoSize(lblMessage);
            pnlOneChat.Width = pnlChat.Size.Width - 16;

            //显示多媒体信息.

            if (!string.IsNullOrEmpty(mediaUrl))
            {

                PictureBox pb = new PictureBox();
                pb.Size = new System.Drawing.Size(100, 100);
                pb.Load("http://localhost:8033" + mediaUrl);
                pnlOneChat.Controls.Add(pb);
            }
            if (!string.IsNullOrEmpty(serviceId))
            {
                FlowLayoutPanel pnlservice = new FlowLayoutPanel();

                Label lblServiceName = new Label();
                lblServiceName.Text= serviceName;
                Button btnConfirm = new Button();
                btnConfirm.Text = "选取";
                btnConfirm.Tag = serviceId;
                btnConfirm.Click += new EventHandler(btnConfirm_Click);
                pnlservice.Controls.AddRange(new Control[]{lblServiceName, btnConfirm});
                pnlOneChat.Controls.Add(pnlservice);
            }
            
            


            pnlChat.Controls.Add(pnlOneChat);
            pnlChat.ScrollControlIntoView(pnlOneChat);


        }

        void btnConfirm_Click(object sender, EventArgs e)
        {
            string serviceId=((Button)sender).Tag.ToString();
            agsc.Message message = new agsc.Message(csId + "@" + GlobalViables.ServerName,
                StringHelper.EnsureOpenfireUserName(tbxUserName.Text) + "@" + GlobalViables.ServerName,
                agsc.MessageType.chat,"已选择服务");
            message.SetAttribute("selected_service_id", serviceId);
            GlobalViables.XMPPConnection.Send(message);
            AddLog(message);
        }
      
        private void btnSend_Click(object sender, EventArgs e)
        {

            agsc.Message message = new agsc.Message(csId + "@" + GlobalViables.ServerName,
                StringHelper.EnsureOpenfireUserName(tbxUserName.Text) + "@" + GlobalViables.ServerName, agsc.MessageType.chat, tbxMessage.Text);

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
            p.To = csId + "@" + GlobalViables.ServerName;
            p.From = StringHelper.EnsureOpenfireUserName(tbxUserName.Text) + "@" + GlobalViables.ServerName;
            GlobalViables.XMPPConnection.Send(p);
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
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

                string result = PHSuit.IOHelper.UploadFileHttp("http://localhost:8033/ajaxservice/FileUploadCommon.ashx",
                      string.Empty, bytes, fileExtension);
                agsc.Message m = new agsc.Message(csId + "@" + GlobalViables.ServerName,
                    StringHelper.EnsureOpenfireUserName(tbxUserName.Text) + "@" + GlobalViables.ServerName,
                    agsc.MessageType.chat, string.Empty);
                m.SetAttribute("media", result);
                GlobalViables.XMPPConnection.Send(m);
                AddLog(m);
            }
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

    }
}
