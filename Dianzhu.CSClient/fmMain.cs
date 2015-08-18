﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsXMPP;
using xmppMessage = agsXMPP.protocol.client;
using agsXMPP.protocol.client;
using Dianzhu.Model;
using Dianzhu.BLL;
namespace Dianzhu.CSClient
{
    public partial class fmMain : Form
    {
        /// <summary>
        /// 聊天主窗口
        /// </summary>
        public fmMain()
        {
            Form fmLogin=new fmLogin();
            if (fmLogin.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }
            else
            {
            InitializeComponent();
            GlobalViables.XMPPConnection.OnMessage += new MessageHandler(XMPPConnection_OnMessage);
            }
        }

        
        string CurrentCustomerId = string.Empty;
        
        void btnCustomer_Click(object sender, EventArgs e)
        {
            foreach (Control c in gbCustomerList.Controls)
            {
                c.ForeColor = Color.Blue;
                if (c.GetType() == typeof(Button))
                {
                    
                  c.ForeColor = Color.Blue;
                        
                }
            }
            Button btn = (Button)sender;
            CurrentCustomerId = btn.Text;
            btn.ForeColor = Color.Red;
        }

        /// <summary>
        /// 新增一个客户
        /// 顶部增加一个item,并高亮显示(未读标签),声音提示
        /// </summary>
         

       
        /// <summary>
        /// 在聊天窗口显示新信息
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="msg"></param>
        private void AddNewMessage(string customerName, string msg)
        {
           
            tbxChatLog.Text = customerName + ":" + msg + Environment.NewLine+tbxChatLog.Text;
            
        }
        
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentCustomerId))
            {
                return;
            }
            GlobalViables.XMPPConnection.Send(new agsXMPP.protocol.client.Message(CurrentCustomerId + "@"+GlobalViables.Domain,MessageType.chat, tbxMsg.Text));
            AddNewMessage(GlobalViables.CurrentUserName, tbxMsg.Text);
        }

        private void tbxMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSendMsg.PerformClick();
            }
        }
        
        #region xmpp
        void XMPPConnection_OnMessage(object sender, xmppMessage.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                return;
            }

            //判断该客户是否已经出现在列表中.
            //创建接待记录,保存聊天信息.
            bool isAdded = false;
            foreach (Control c in gbCustomerList.Controls)
            {
                c.ForeColor = Color.Blue;
                if (c.GetType() == typeof(Button))
                {
                    if (c.Text == msg.From.User)
                    {
                        c.ForeColor = Color.Red;
                        CurrentCustomerId = c.Text;
                        isAdded = true;
                    }
                }
            }
            //新增加的用户.
            if (!isAdded)
            {
                Button btn = new Button();
                btn.ForeColor = Color.Red;
                CurrentCustomerId = btn.Text = msg.From.User;
                btn.Click += new EventHandler(btnCustomer_Click);
                btn.AutoSize = true;
                btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(0, 0, 0, 0);
                gbCustomerList.Controls.Add(btn);
            }



            AddNewMessage(msg.From.User, msg.Body);
        }
        #endregion
    }


    public class FormData
    {
        public DZMembership Customer { get; set; }
        public ReceptionBase Reception { get; set; }
    }



}
