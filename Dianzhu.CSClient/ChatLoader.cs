using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsMsg=agsXMPP.protocol.client;
using System.Windows.Forms;
using Dianzhu.Model;
namespace Dianzhu.CSClient
{
    public class ChatLoaderFactory
    {
     

         
        public static ChatLoader CreateInstance(Dianzhu.Model.ReceptionChat chat)
        {
            if (chat.GetType()==typeof(ReceptionChatService))
            {
  
            }
                return null;
             
        }
    }
    /// <summary>
    /// 
    /// 纯文本信息显示.
    /// </summary>
    public  class ChatLoader
    {

        public FlowLayoutPanel PnlOneChatContainer { get; protected set; }
        protected string fromUserName;
        protected ReceptionChat chat;
        public ChatLoader(string fromUserName)
        {
            this.fromUserName = fromUserName;
            PnlOneChatContainer = new FlowLayoutPanel();
            Label lblUserName = new Label();
            lblUserName.Text = fromUserName;
        }
      //  protected abstract void BuildChatLine();
         
       
         
    }
    public class ChatLoaderText:ChatLoader
    {
        string textMessage;
        public ChatLoaderText(string fromUserName, string message):base(fromUserName)
        {
            this.textMessage = message;
        }
        //protected override void BuildChatLine()
        //{
        //    Label msg = new Label();
        //    msg.Text = textMessage;
        //    PnlOneChatContainer.Controls.Add(msg);
        //}

    }
   /// <summary>
   /// 多媒体文件显示
   /// </summary>
   
    public class ChatLoaderService:ChatLoader
    {
        public event EventHandler ButtonClickHandler;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromUserName"></param>
        /// <param name="serviceId"></param>
        /// <param name="serviceName"></param>
        /// <param name="businessName"></param>
        /// <param name="type">服务消息相关的类型.1)推送给客户的服务,2)客户确定的服务,3)支付链接的服务. </param>
        public ChatLoaderService(string fromUserName,string serviceId, string serviceName, string businessName,decimal servicePrice,string type):base(fromUserName)
        {
            Panel pnl = new Panel();
            Label lblServiceName = new Label();
            lblServiceName.Text = serviceName;
            Label lblBusinessName = new Label();
            lblBusinessName.Text = businessName;
            pnl.Controls.Add(lblServiceName);
            pnl.Controls.Add(lblBusinessName);
            Button btn = new Button();
            btn.Click+=new EventHandler(btn_Click);
            btn.Tag = serviceId;
            switch (type.ToLower())
            {
                case "push": btn.Text = "确定"; break;
                case "confirm": btn.Text = "发送支付链接"; break;
                case "pay": btn.Text = "支付"; break;
            }
           
            PnlOneChatContainer.Controls.Add(pnl);
            PnlOneChatContainer.Controls.Add(btn);
        }

        void btn_Click(object sender, EventArgs e)
        {
            if (this.ButtonClickHandler != null) this.ButtonClickHandler(sender, e);
        }
    }
    public class ChatLoaderMedia : ChatLoader
    {
        public ChatLoaderMedia(string fromUserName, string mediaUrl):base(fromUserName)
        { 
            
        }
    }

    
}
