using System;
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
            //Form fmLogin = new fmLogin();
            //if (fmLogin.ShowDialog() == DialogResult.OK)
            //{
            //    this.Show();
            //    
            //}
            //else
            //{
            //    Application.Exit();
            //}
            InitializeComponent();
            GlobalViables.XMPPConnection.OnMessage += new MessageHandler(XMPPConnection_OnMessage);
          
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

            tbxChatLog.Text = customerName + ":" + msg + Environment.NewLine + tbxChatLog.Text;

        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (currentCustomer==null)
            {
                return;
            }
            GlobalViables.XMPPConnection.Send(new agsXMPP.protocol.client.Message(currentCustomer.UserName + "@" + GlobalViables.Domain, MessageType.chat, tbxMsg.Text));
            AddNewMessage(GlobalViables.CurrentUserName, tbxMsg.Text);
        }

        private void tbxMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSendMsg.PerformClick();
            }
        }



        #region 1 收到新消息.
        void XMPPConnection_OnMessage(object sender, xmppMessage.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                return;
            }

            //判断该客户是否已经出现在列表中.
            //创建接待记录,保存聊天信息.
            ReceiveNewMessage(msg.From.User, msg.Body);
        }

        IList<DZMembership> l_customerList = new List<DZMembership>();
        //local variable: 当前的接待列表
        Dictionary<string, ReceptionBase> l_receptionList = new Dictionary<string, ReceptionBase>();

        private void ReceiveNewMessage(string customerLoginName, string messageBody)
        {
            //1 判断是否已经加入列表
            var isAdded = l_customerList.Any(x => x.UserName == customerLoginName);
            DZMembership customer=null;
            //已经增加
            if (isAdded)
            {
                customer = l_customerList.Single(x => x.UserName == customerLoginName);
                //ui 如果不是在当前用户的聊天窗口 则设置该按钮为 未读.
                if (currentCustomer!=null&&currentCustomer.UserName != customerLoginName)
                {
                    UI_NewMessage(customerLoginName);
                }
               
            }
            //新进来的请求
            else
            {
                //客户列表增加一个 
                customer = BLLFactory.BLLMembership.GetUserByName(customerLoginName);
                l_customerList.Add(customer);
                //新增接待列表
                
                
                //ui
                UI_AddNewCustomer(customerLoginName);
            } 
            //bl保存该消息
            ReceptionChat newRch = new ReceptionChat { From=customer, To=GlobalViables.CurrentCustomerService
                , ReceiveTime=DateTime.Now, MessageBody= messageBody};
                ReceptionBase re = new ReceptionBase { Receiver=GlobalViables.CurrentCustomerService,
                 Sender=customer};
                re.ChatHistory.Add(newRch);
                BLLFactory.BLLReception.Save(re);
            if(!isAdded)
            {
                l_receptionList.Add(customerLoginName, re);
            }
            //如果消息来自于正在聊天的客户, 则聊天窗口增加消息显示.
            if (currentCustomer != null && currentCustomer.UserName == customerLoginName)
            {
                string message = newRch.ReceiveTime.ToString("dd号 hh:mm:ss") +" "+ newRch.From.UserName + ":" + newRch.MessageBody;
                UI_Add_New_Messge(message);
            }
        }

        #endregion


        #region 2 点击客户名称

        DZMembership currentCustomer = null;
        Button currentButton = null;
        void btnCustomer_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string customerName = btn.Text;
            //判断当前被激活的用户
            //如果是第一次激活
            if (currentCustomer == null)
            {
                currentCustomer = l_customerList.Single(x => x.UserName == customerName);
            }
            else {
                //如果相等 返回
                if (currentCustomer.UserName == customerName)
                {
                    return;
                }
                else {
                    currentCustomer = l_customerList.Single(x => x.UserName == customerName);
                }
            }
            //currentuuser 不是null,而且不等于当前激活用户
            //1 ui 当前button样式变化.
            UI_Style_Current(btn);
            //2 ui 之前的currentButton变为已读
            if (currentButton != null)
            {
                UI_Style_Readed(currentButton);
            }
            //将当前button设置为被点击的button
            currentButton = btn;



            //2 聊天记录窗口 显示与该客户的聊天记录(注意,需要限制显示条数)
           ReceptionBase re= l_receptionList[customerName];
            //加载历史聊天记录
           var chatList = BLLFactory.BLLReception.GetHistoryReceptionChat(currentCustomer, GlobalViables.CurrentCustomerService, 20);
          // UI_Load_ChatHistory(  re.ChatHistory);
           UI_Load_ChatHistory(chatList);
             
        }
        #endregion


        #region UI
        private readonly string pre_customer_btn = "cb_";
        //接受到新消息后 修改button的样式.
        private void UI_NewMessage(string userLoginName)
        {
           
            Button btn = this.Controls.Find(pre_customer_btn + userLoginName, true)[0] as Button;
            UI_Style_Unread(btn);

        }
        //未读按钮
        private void UI_Style_Unread(Button btn)
        {
           
            btn.BackColor = Color.YellowGreen;
            btn.ForeColor = Color.Red;
        }
        //已读按钮
        private void UI_Style_Readed(Button btn)
        {
            btn.BackColor = Color.FromArgb(100);
            btn.ForeColor = Color.Black;
        }
        //当前按钮
        private void UI_Style_Current(Button btn)
        {
            btn.BackColor = Color.FromArgb(200);
            btn.ForeColor = Color.FromArgb(100);
        }
        private void UI_AddNewCustomer(string userLoginName)
        {
            Button btn = new Button();
            btn.Name = pre_customer_btn + userLoginName;
            btn.Text = userLoginName;
            btn.AutoSize = true;
            btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(0, 0, 0, 0);
            btn.Click += new EventHandler(btnCustomer_Click);
            gbCustomerList.Controls.Add(btn);
            UI_Style_Unread(btn);
        }
        private void UI_Load_ChatHistory(IList<ReceptionChat> chatHistory)
        {
             chatHistory.OrderBy(x => x.SendTime);
             tbxChatLog.Lines=chatHistory.OrderBy(x=>x.ReceiveTime)
                 .Select(x =>x.ReceiveTime.ToString("dd号 HH:mm:ss")
                     +" "+ x.From.UserName+":"+ x.MessageBody).ToArray();
             tbxChatLog.SelectionStart = tbxChatLog.Text.Length;
             tbxChatLog.ScrollToCaret();
       
        }
        /// <summary>
        /// 聊天记录窗口 增加一条消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="from"></param>
        private void UI_Add_New_Messge(string message)
        {
            tbxChatLog.AppendText(Environment.NewLine + message);
            tbxChatLog.SelectionStart = tbxChatLog.Text.Length;
            tbxChatLog.ScrollToCaret();
          
        }
        #endregion
    }


    public class FormData
    {
        public DZMembership Customer { get; set; }
        public ReceptionBase Reception { get; set; }
    }



}
