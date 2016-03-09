using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dianzhu.Model;
using Dianzhu.CSClient.IView;
using log4net;
namespace Dianzhu.CSClient.WinformView
{
    public partial class UC_ChatList:UserControl,IView.IViewChatList
    {
        ILog log = log4net.LogManager.GetLogger("Dianzhu.UC_ChatList");
        public UC_ChatList()
        {
            InitializeComponent();
        }
        public event SendTextClick SendTextClick;
        public string MessageText {
            get { return tbxMessage.Text; }
            set { tbxMessage.Text = value; }
        }
        public IList<ReceptionChat> ChatList
        {
            set
            {
                pnlChatList.Controls.Clear();
                foreach (ReceptionChat chat in value)
                {
                    AddOneChat(chat);
                }
            }
            get {
                IList<ReceptionChat> chatList = new List<ReceptionChat>();
                foreach (Label lbl in this.pnlChatList.Controls)
                {
                    if (lbl.Tag != null)
                    {
                        if(lbl.Tag.GetType()==typeof(ReceptionChat))
                        { 
                        chatList.Add((ReceptionChat)lbl.Tag);
                        }
                    }
                    
                }
                return chatList;
            }
        }
        public void AddOneChat(ReceptionChat chat)
        {
         
            Action lambda = () =>
            {
                Label lblChat = new Label { Text = "chat:" + chat.MessageBody, AutoSize = true, Name = Guid.NewGuid().ToString() };
                lblChat.Tag = chat;
              this.pnlChatList.Controls.Add(lblChat);


            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else { lambda(); }
        }
        private void btnSendText_Click(object sender, EventArgs e)
        {
            SendTextClick();
        }
    }
}
