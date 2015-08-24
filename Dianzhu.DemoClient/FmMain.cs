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

            AddLog(StringHelper.EnsureNormalUserName(msg.From.User), log);

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
        void AddLog(string user, string body)
        {

            tbxLog.AppendText(Environment.NewLine + user + ":" + body);


        }
        public void InsertImage(string fileName)
        {

            string lstrFile = fileName;
            Bitmap myBitmap = new Bitmap(lstrFile);
            // Copy the bitmap to the clipboard.
            Clipboard.SetDataObject(myBitmap);
            // Get the format for the object type.
            DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Bitmap);
            // After verifying that the data can be pasted, paste
            if (tbxLog.CanPaste(myFormat))
            {
                tbxLog.Paste(myFormat);
            }


        }

        private void btnSend_Click(object sender, EventArgs e)
        {



            GlobalViables.XMPPConnection.Send(new agsc.Message(csId + "@" + GlobalViables.ServerName, agsc.MessageType.chat, tbxMessage.Text));
            AddLog(tbxUserName.Text, tbxMessage.Text);

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
                    agsc.MessageType.chat, string.Empty);
                m.SetAttribute("media", result);
                GlobalViables.XMPPConnection.Send(m);
                AddLog(tbxUserName.Text, tbxMessage.Text);


                InsertImage(dlgSelectPic.FileName);
            }
        }
    }
}
