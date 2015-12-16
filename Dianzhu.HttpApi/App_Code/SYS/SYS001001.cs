using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Net;
using Dianzhu.Model.Enums;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseSYS001001:BaseResponse
{
    BLLReceptionChat bllReceptionChat;
    DZMembershipProvider bllMember;
    BLLServiceOrder bllOrder;

    public ResponseSYS001001(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataSYS001001 requestData = this.request.ReqData.ToObject<ReqDataSYS001001>();

        bllReceptionChat = new BLLReceptionChat();
        bllMember = new DZMembershipProvider();
        bllOrder = new BLLServiceOrder();

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
        chat.ReceiveTime = DateTime.Now;
        chat.SavedTime = DateTime.Now;
        if (chatType == enum_ChatType.Media)
        {
            var mediaUrl = reqData.msgObj_url;
            var mediaType = reqData.msgObj_type;
            ((ReceptionChatMedia)chat).MedialUrl = mediaUrl;
            ((ReceptionChatMedia)chat).MediaType = mediaType;
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
                    string savedPath = Environment.CurrentDirectory + System.Configuration.ConfigurationManager.AppSettings.Get("LocalMediaSaveDir") + localFileName;
                    PHSuit.IOHelper.EnsureFileDirectory(savedPath);
                    client.DownloadFile(mediaUrl, savedPath);
                }
            }
        }

        //ReceptionChatDD chatDD = new ReceptionChatDD();
        //chatDD.MessageBody = chat.MessageBody;
        //chatDD.ReceiveTime = chat.ReceiveTime;
        //chatDD.SendTime = chat.SendTime;
        //chatDD.To = chat.To;
        //chatDD.From = chat.From;
        //chatDD.Reception = chat.Reception;
        //chatDD.SavedTime = chat.SavedTime;
        //chatDD.ChatType = chat.ChatType;
        //chatDD.ServiceOrder = chat.ServiceOrder;
        //chatDD.Version = chat.Version;
        bllReceptionChat.Save(chat);

        #endregion
    }
}

public class ReqDataSYS001001
{
    public string id { get; set; }
    public string to { get; set; }
    public string from { get; set; }
    public string body { get; set; }
    public string ext { get; set; }
    public string orderId { get; set; }
    public string msgObj_url { get; set; }
    public string msgObj_type { get; set; }
    //public MsgObj msgObj { get; set; }
}
public class MsgObj
{
    public string url { get; set; }
    public string type { get; set; }
}