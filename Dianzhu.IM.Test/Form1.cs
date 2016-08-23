using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using a=agsXMPP;
using System.IO;
namespace Dianzhu.IM.Test
{
    public partial class Form1 : Form
    {
       a.XmppClientConnection   conn;
        IList<LoginAccount> loginAccounts = new List<LoginAccount>();
        public Form1()
        {
            
            InitializeComponent();
            LoadAccountButton();
            conn = new a.XmppClientConnection();
            conn.Resource = "YDBan_User";
            conn.OnLogin += Conn_OnLogin;
            conn.OnError += Conn_OnError;
            conn.OnAuthError += Conn_OnAuthError;
          
            conn.OnClose += Conn_OnClose;
            conn.OnSocketError += Conn_OnSocketError;
            conn.OnStreamError += Conn_OnStreamError;
        }
        private void Conn_OnLogin(object sender)
        {
            Action lambda = () =>
            {
                lblLoginMsg.Text = "登录成功";
                LoadTestButton();
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
            foreach (string file in files)
            {
                if (!file.Contains(server)) continue;
               string fileName= Path.GetFileNameWithoutExtension(file);
              
                string name= fileName.Split('-')[1];
                Button btn = new Button { Text = name, Tag = file};
                 
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
                btnLogin.Name="btnLogin_"+account.GetHashCode();
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
            

            Login(account.server,account.loginid,account.password);
        }

        private void  ReadAccountFromFile()
        {
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\account.txt");
            foreach (string line in lines)
            {
                if (line.StartsWith("#")) continue;
                string[] sl = line.Split('|');
                string server = sl[0];
                string loginid = sl[1];
                string password = sl[2];
                loginAccounts.Add(new LoginAccount { loginid = loginid, password = password, server = server });

            } 
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            string fielPath = ((Button)sender).Tag.ToString();

            string xml = File.ReadAllText(fielPath); 
            xml = string.Format(xml, tbxTargetUser.Text, server);
            conn.Send(xml);
        }
        #region xmpp event
        private void Conn_OnStreamError(object sender, a.Xml.Dom.Element e)
        {
            throw new NotImplementedException();
        }

        private void Conn_OnSocketError(object sender, Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        private void Conn_OnClose(object sender)
        {
            MessageBox.Show("请重新启动程序");
        }

        

        private void Conn_OnAuthError(object sender, a.Xml.Dom.Element e)
        {
            MessageBox.Show("用户名密码有误"); 
        }

        private void Conn_OnError(object sender, Exception ex)
        {
            throw new NotImplementedException();
        }
        #endregion
        string username, server;
        private void btnLogin_Click(object sender, EventArgs e)
        {
           
          
            
        }
        private void Login(string server, string username, string password)
        {
            conn.Server =server;
            conn.ConnectServer = server;
            conn.AutoResolveConnectServer = false;
            conn.Open(username, password);
            this.username = username;
            this.server = server;
          
        }

    
 
        private string GetMessageFromFile(string name)
        {
          return   File.ReadAllText(Environment.CurrentDirectory + "\\messages\\" + name + ".xml");
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
        struct LoginAccount
        {
          public   string server { get; set; }
            public string loginid { get; set; }
            public string password { get; set; }

            public override string ToString()
            {
                return server + "##" + loginid + "##";
            }
        }
    }
}
