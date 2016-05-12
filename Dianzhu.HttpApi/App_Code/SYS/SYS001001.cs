using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Net;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseSYS001001:BaseResponse
{
    BLLReceptionChatDD bllReceptionChatDD;
    DZMembershipProvider bllMember;
    public IBLLServiceOrder bllOrder { get; set; }
    public ResponseSYS001001(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataSYS001001 requestData = this.request.ReqData.ToObject<ReqDataSYS001001>();

        bllReceptionChatDD = new BLLReceptionChatDD();
        bllMember = new DZMembershipProvider();
      
        try
        {
            ReceptionChat chat = reqDataToChat(requestData);
            SaveMessage(chat, false);

            this.state_CODE = Dicts.StateCode[0];
            return;
        }
        catch (Exception ex)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = ex.Message;
            return;
        }
    }

    /// <summary>
    /// 通过发送的数据构建ReceptionChat
    /// </summary>
    /// <param name="reqData"></param>
    /// <returns></returns>
    private ReceptionChat reqDataToChat(ReqDataSYS001001 reqData)
    {
        enum_ChatType chatType;
        switch (reqData.ext.ToLower())
        {
            case "ihelper:chat:text":
                chatType = enum_ChatType.Text;
                break;
            case "ihelper:chat:media":
                chatType = enum_ChatType.Media;
                break;
            case "ihelper:notice:system":

            case "ihelper:notice:order":

            case "ihelper:notice:promote":

            case "ihelper:notice:cer:change":
                chatType = enum_ChatType.Notice;
                break;
            default:
                throw new Exception("未知的命名空间");
        }
        ReceptionChat chat = ReceptionChat.Create(chatType);

        chat.Id = new Guid(reqData.id);
        chat.To = bllMember.GetUserById(new Guid(reqData.to));
        chat.From = bllMember.GetUserById(new Guid(reqData.from));
        chat.ServiceOrder = bllOrder.GetOne(new Guid(reqData.orderId));
        chat.MessageBody = reqData.body;
        chat.FromResource = (enum_XmppResource)Enum.Parse(typeof(enum_XmppResource), reqData.from_resource);
        chat.ReceiveTime = DateTime.Now;
        chat.SavedTime = DateTime.Now;
        if (chatType == enum_ChatType.Media)
        {
            var mediaUrl = reqData.msgObj_url;
            var mediaType = reqData.msgObj_type;
            ((ReceptionChatMedia)chat).MedialUrl = mediaUrl;
            ((ReceptionChatMedia)chat).MediaType = mediaType;
        }
        if (chat.To.UserType == enum_UserType.customerservice || chat.From.UserType == enum_UserType.customerservice)
        {
            chat.ChatTarget = enum_ChatTarget.cer;
        }
        else if (chat.To.UserType == enum_UserType.business || chat.From.UserType == enum_UserType.business)
        {
            chat.ChatTarget = enum_ChatTarget.store;
        }

        return chat;
    }

    /// <summary>
    /// 保存消息.
    /// 此方法不知道消息方向.
    /// </summary>
    /// <param name="message"></param>
    /// 
    private void SaveMessage(ReceptionChat chat, bool isSend)
    {
        #region 保存聊天消息

        DZMembership fromCustomer = isSend ? chat.To : chat.From;
        string customerName = fromCustomer.UserName;
        string message = chat.MessageBody;


        DateTime now = DateTime.Now;

        if (isSend)
        {
            chat.SendTime = now;
        }
        else
        {
            chat.ReceiveTime = now;
        }
        if (chat is ReceptionChatMedia)
        {
            if (((ReceptionChatMedia)chat).MediaType != "url")
            {
                string mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                string localFileName = PHSuit.StringHelper.ParseUrlParameter(mediaUrl, string.Empty);

                using (var client = new WebClient())
                {
                    string savedPath = HttpRuntime.AppDomainAppPath + Dianzhu.Config.Config.GetAppSetting("LocalMediaSaveDir") + localFileName;
                    PHSuit.IOHelper.EnsureFileDirectory(savedPath);
                    client.DownloadFile(mediaUrl, savedPath);
                }
            }
        }

        ReceptionChatDD chatDD = new ReceptionChatDD();
        chatDD.Id = chat.Id;
        chatDD.MessageBody = chat.MessageBody;
        chatDD.ReceiveTime = chat.ReceiveTime;
        chatDD.SendTime = chat.SendTime;
        chatDD.To = chat.To;
        chatDD.From = chat.From;
         chatDD.SavedTime = DateTime.Now;
        chatDD.ChatType = chat.ChatType;
        chatDD.FromResource = chat.FromResource;
        chatDD.ServiceOrder = chat.ServiceOrder;
        chatDD.Version = chat.Version;
        chatDD.IsCopy = false;
        if (chat is ReceptionChatMedia)
        {
            chatDD.MedialUrl = ((ReceptionChatMedia)chat).MedialUrl.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"), "");
            chatDD.MediaType = ((ReceptionChatMedia)chat).MediaType;
        }
        chatDD.ChatTarget = chat.ChatTarget;
        
        bllReceptionChatDD.Save(chatDD);

        #endregion
    }
}