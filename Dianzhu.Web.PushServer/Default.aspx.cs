using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Push;
using Ydb.InstantMessage.DomainModel.Enums;
using Dianzhu.Model.Enums;
using Ydb.InstantMessage.DomainModel.Chat;

public partial class _Default : System.Web.UI.Page
{

    
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    string msg;
    private void Push()
    {
        
          
            msg += "-----------" + DateTime.Now + "-----------------" + Environment.NewLine;
        string appType = rblAppType.SelectedValue;
        string message = tbxMessage.Text+DateTime.Now;
          
          
            
    
        PushType pushType = rblPushTarget.SelectedValue == "user" ? PushType.PushToUser : PushType.PushToBusiness;
            PushMessageBuilder pushMessageBuilder = new PushMessageBuilder();
            PushMessage pushMessage=   pushMessageBuilder.BuildPushMessage(message, ChatType, FromResource, "test_fromusername",
                tbxOrderId.Text, "test_businessName", "test_serialNo", OrderStatus, rblOrderStatus.Text);
                 IPush push = PushFactory.Create(pushType, appType, pushMessage);
            string pushResult= push.Push(  PushToken,1);

            msg += string.Format("推送结果{0}({1})", pushResult, PushToken)+Environment.NewLine;
       
    }

    string PushToken {
        get {
            string customerToken = tbxToken.Text;
            return string.IsNullOrEmpty(customerToken) ? rblTokenList.SelectedValue : customerToken;
        }
    }

    Type ChatType {
        get {
            Type t = null;
            switch (rblChatType.SelectedValue)
            {
                case "Chat":t = typeof(ReceptionChat); break;
                case "OrderNotice": t = typeof(ReceptionChatNoticeOrder); break;
                case "SysNotice": t = typeof(ReceptionChatNoticeSys); break;
            }
            return t;
        }
    }
        Ydb.InstantMessage.DomainModel.Enums.XmppResource FromResource
    {
        get {
            return (XmppResource) Enum.Parse(typeof( XmppResource), rblChatFromResource.SelectedValue);
        }
    }
   enum_OrderStatus OrderStatus
    {
        get
        {
            return (enum_OrderStatus)Enum.Parse(typeof(enum_OrderStatus), rblOrderStatus.SelectedValue);
        }
    }
    protected void btnPush_Click(object sender, EventArgs e)
    {
        
        Push();
        tbxLog.Text += msg;
    }
}