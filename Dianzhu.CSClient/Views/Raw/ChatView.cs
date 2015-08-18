using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dianzhu.CSClient.Views.Raw
{
    public partial class ChatView : Form,IChatView
    {
        public ChatView()
        {
            InitializeComponent();
        }

        public string MessageList
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Presenters.Raw.ChatPresenter cp = new Presenters.Raw.ChatPresenter(this);
            cp.LoadChatHistory();
        }
    }
}
