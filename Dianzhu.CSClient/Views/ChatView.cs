using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormsMvp.Forms;
namespace Dianzhu.CSClient.Views
{
    public partial class ChatView :
         //UserControl
        MvpUserControl<Models.ChatModel>,ViewsContracts.IChatView
    {
        //只是和控件相关的东西.
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeComponent();
            //在
            tbxMsgList.Text = string.Join(Environment.NewLine, Model.ChatHistory);
        }
        
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            SendMsg(this,e);
             
        }

        public event EventHandler SendMsg;
    }
}
