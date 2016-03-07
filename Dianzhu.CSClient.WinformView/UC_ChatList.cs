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

namespace Dianzhu.CSClient.WinformView
{
    public partial class UC_ChatList:UserControl,IView.IViewChatList
    {
        public UC_ChatList()
        {
            InitializeComponent();
            // pnlChatList.Controls.Add(new Label { Text = "sdfasdfsadfdsf" });
         //   Label lblChat = new Label { Text ="ccccccccccc" };
         //   pnlChatList.Controls.Add(lblChat);
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
        }
        public void AddOneChat(ReceptionChat chat)
        {
         
            Action lambda = () =>
            {
                Label lblChat = new Label { Text = "chat:" + chat.MessageBody, AutoSize = true, Name = Guid.NewGuid().ToString() };
              this.pnlChatList.Controls.Add(lblChat);


            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else { lambda(); }
        }
        public void AddOneChat(string message)
        {
            Action lambda = () =>
            {
                Label lblChat = new Label { Text = "chat:" + message, AutoSize = true };
                this.pnlChatList.Controls.Add(lblChat);
                
            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else { lambda(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddOneChat("dsfasdfdsafdsf");
        }
    }
}
