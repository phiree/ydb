using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using a = agsXMPP;
using System.IO;
using log4net;
using System.Text.RegularExpressions;
namespace Dianzhu.IM.Test
{
    public partial class Main : Form
    {
        ILog log = LogManager.GetLogger("Dianzhu.IMTest");
        a.XmppClientConnection conn;
        IList<LoginAccount> loginAccounts = new List<LoginAccount>();
        public Main()
        {

            InitializeComponent();
            LoadAccountButton();
            conn = new a.XmppClientConnection();

            conn.OnLogin += Conn_OnLogin;
            conn.OnError += Conn_OnError;
            conn.OnAuthError += Conn_OnAuthError;

            conn.OnClose += Conn_OnClose;
            conn.OnSocketError += Conn_OnSocketError;
            conn.OnStreamError += Conn_OnStreamError;
            conn.OnMessage += Conn_OnMessage;
            conn.OnPresence += Conn_OnPresence;
            conn.OnIq += Conn_OnIq;


        }

        private void Conn_OnIq(object sender, a.protocol.client.IQ iq)
        {
            Log(iq.ToString());
        }

        private void Conn_OnPresence(object sender, a.protocol.client.Presence pres)
        {
            Log(pres.ToString());
        }

        private void Conn_OnMessage(object sender, a.protocol.client.Message msg)
        {
            Log(msg.ToString(), MessageDirection.Received);
        }

        private void Conn_OnLogin(object sender)
        {
            Action lambda = () =>
            {

                tbxMyJID.Text = conn.MyJID;
                btnLogOut.Visible = true;
                btnCopy.Visible = true;
                LoadTestButton();
                this.Text = conn.MyJID;
            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else { lambda(); }

        }
        private void LoadTestButton()
        {
            string[] files = Directory.GetFiles(Environment.CurrentDirectory + "\\messages\\");
            flowLayoutPanel1.Controls.Clear();
            foreach (string file in files)
            {
                if (!file.Contains(server)) continue;
                string fileName = Path.GetFileNameWithoutExtension(file);

                string name = fileName.Split('-')[1];
                Button btn = new Button { Text = name, Tag = file };

                btn.Click += Btn_Click;
                flowLayoutPanel1.Controls.Add(btn);
            }

        }
        private void LoadAccountButton()
        {
            ReadAccountFromFile();
            foreach (LoginAccount account in loginAccounts)
            {
                Button btnLogin = new Button();
                btnLogin.Name = "btnLogin_" + account.GetHashCode();
                btnLogin.Text = account.ToString();
                btnLogin.AutoSize = true;
                btnLogin.Tag = account;
                btnLogin.Click += BtnLogin_Click;
                pnlLoginButtons.Controls.Add(btnLogin);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            LoginAccount account = (LoginAccount)((Button)sender).Tag;


            Login(account.server, account.loginid, account.password, account.resource);
        }

        private void ReadAccountFromFile()
        {
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\account.txt");
            foreach (string line in lines)
            {
                if (line.StartsWith("#")) continue;
                string[] sl = line.Split('|');
                string server = sl[0];
                string loginid = sl[1];
                string password = sl[2];
                string resource = sl[3];
                loginAccounts.Add(new LoginAccount { loginid = loginid, password = password, server = server, resource = resource });

            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            string fielPath = ((Button)sender).Tag.ToString();

            string xml = File.ReadAllText(fielPath);
            xml = string.Format(xml, tbxTargetUser.Text, server);


            if (string.IsNullOrEmpty(tbxTargetUser.Text))
            {
                xml = Regex.Replace(xml, "\\s+to\\s?=\\s?\\\".+?\\\"", " ", RegexOptions.IgnoreCase);
            }
            if (!cbxIncludeFrom.Checked)
            {
                xml = Regex.Replace(xml, "\\s+from\\s?=\\s?\\\".+?\\\"", " ", RegexOptions.IgnoreCase);
            }

            SendMessage(xml);
        }
        #region xmpp event
        private void Conn_OnStreamError(object sender, a.Xml.Dom.Element e)
        {

            Log(e.ToString(), MessageDirection.Received);
        }

        private void Conn_OnSocketError(object sender, Exception ex)
        {
            Log(ex.Message, MessageDirection.Received);
        }

        private void Conn_OnClose(object sender)
        {
            Log("Conn_Closed");
        }



        private void Conn_OnAuthError(object sender, a.Xml.Dom.Element e)
        {
            Log("Conn_AuthError");
        }

        private void Conn_OnError(object sender, Exception ex)
        {
            Log(ex.Message);
        }
        #endregion
        string username, server;
        private void btnLogin_Click(object sender, EventArgs e)
        {



        }
        private void Login(string server, string username, string password, string resource)
        {
            conn.Close();

            conn.Server = server;
            conn.Resource = resource;
            conn.ConnectServer = server;
            conn.AutoResolveConnectServer = false;
            conn.Open(username, password);
            this.username = username;
            this.server = server;

        }



        private string GetMessageFromFile(string name)
        {
            return File.ReadAllText(Environment.CurrentDirectory + "\\messages\\" + name + ".xml");
        }
        private string ReadLoginAccount(string server)
        {
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\account.txt");
            foreach (string line in lines)
            {
                if (line.StartsWith("#")) continue;
                string[] sl = line.Split('|');
                if (sl[0] == server)
                {
                    return line;
                }
            }
            throw new Exception("配置文件有误");
        }

        private void btnReloadMessages_Click(object sender, EventArgs e)
        {
            LoadTestButton();
        }

        struct LoginAccount
        {
            public string server { get; set; }
            public string loginid { get; set; }
            public string password { get; set; }
            public string resource { get; set; }

            public override string ToString()
            {
                return server + "##" + loginid + "##" + resource;
            }
        }
        enum MessageDirection
        {
            Received,
            Sent
        }
        private void Log(string content)
        {
            Log(content, MessageDirection.Received);
        }
        private void Log(string content, MessageDirection direction)
        {
            string formatedContent = "-------------" + DateTime.Now + "--------------" + Environment.NewLine + content + Environment.NewLine;
            InvokeIfRequired(this, () =>
            {
                switch (direction)
                {
                    case MessageDirection.Received:
                        tbxLogReceived.Text += formatedContent;
                        tbxLogReceived.SelectionStart = tbxLogReceived.Text.Length;
                        tbxLogReceived.ScrollToCaret();
                        break;
                    case MessageDirection.Sent:
                        tbxLogSent.Text += formatedContent;

                        tbxLogSent.SelectionStart = tbxLogSent.Text.Length;
                        tbxLogSent.ScrollToCaret();
                        break;
                }
                log.Debug(direction.ToString() + content);
            });

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
   

            string[] messages = PHSuit.StringHelper.RegexSpliter("---{3,}", tbxManualMessage.Text);

            foreach (string message in messages)
            {
                SendMessage(message);

            }
            //  conn.Send(tbxManualMessage.Text);
        }


        private void SendMessage(string message)
        {
            conn.Send(message);
            Log(message, MessageDirection.Sent);
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            conn.Close();
            tbxMyJID.Clear();
            flowLayoutPanel1.Controls.Clear();
            btnLogOut.Visible = false;
            btnCopy.Visible = false;
            this.Text = "未登录";

        }

        Timer tLabelDisplay = new Timer();
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if(conn.MyJID!=null)
            {
                Clipboard.SetText(conn.MyJID.User);
                lblCopyResult.Text = "已复制";

                tLabelDisplay.Interval = 5000;
                tLabelDisplay.Tick += TLabelDisplay_Tick;
                tLabelDisplay.Start();
                    }
        }

        private void TLabelDisplay_Tick(object sender, EventArgs e)
        {
            lblCopyResult.Text = string.Empty;
            tLabelDisplay.Stop();
        }

        public void InvokeIfRequired(ISynchronizeInvoke obj,
                                         MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                var args = new object[0];
                obj.Invoke(action, args);
            }
            else
            {
                action();
            }
        }

    }
}
