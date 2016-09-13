using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Dianzhu.Model.Enums;

namespace Dianzhu.Api.Model
{
    public class RespDataCHAT_chatObj
    {
        public string id { get; set; }
        public string to { get; set; }
        public string from { get; set; }
        public string orderID { get; set; }
        public string body { get; set; }
        public string type { get; set; }
        public string date { get; set; }
        public RespDataCHAT_chatObj Adapt(ReceptionChat chat)
        {
            this.id = chat.Id.ToString();
            this.to = chat.ToId;
            this.from = chat.FromId;
            this.orderID = chat.SessionId;
            this.type = "chat";
            this.date = chat.SavedTime.ToString("yyyyMMddHHmmss");
            if (chat is ReceptionChatMedia)
            {
                this.type = ((ReceptionChatMedia)chat).MediaType;
                this.body = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + ((ReceptionChatMedia)chat).MedialUrl;
            }
            else if (chat is ReceptionChatReAssign)
            {
                this.type = "reassign";
                this.body = "(Reassign to)" + ((ReceptionChatReAssign)chat).ReAssignedCustomerServiceId;
            }
            else if(chat is ReceptionChatPushService)
            {
                this.type = "pushOrder";
                this.body = string.Empty;
            }
            else
            {
                this.body = chat.MessageBody;
            }
            return this;

        }
    }

    #region CHAT001004
    public class ReqDataCHAT001004
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string target { get; set; }
        public Dianzhu.Model.Enums.enum_ChatTarget Target
        {
            get
            {
                Dianzhu.Model.Enums.enum_ChatTarget tar;
                bool isType = Enum.TryParse<Dianzhu.Model.Enums.enum_ChatTarget>(target, out tar);
                if (!isType) { throw new Exception("不可识别的用户类型"); }
                return tar;
            }
        }
    }
    public class RespDataCHAT001004
    {
        public string sum { get; set; }
    }
    #endregion

    #region CHAT001006
    public class ReqDataCHAT001006
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string target { get; set; }
        public Dianzhu.Model.Enums.enum_ChatTarget Target
        {
            get
            {
                Dianzhu.Model.Enums.enum_ChatTarget tar;
                bool isType = Enum.TryParse<Dianzhu.Model.Enums.enum_ChatTarget>(target, out tar);
                if (!isType) { throw new Exception("不可识别的用户类型"); }
                return tar;
            }
        }
        public string pageSize { get; set; }
        public string pageNum { get; set; }
    }
    public class RespDataCHAT001006
    {
        public IList<RespDataCHAT_chatObj> arrayData { get; set; }
        public RespDataCHAT001006()
        {
            arrayData = new List<RespDataCHAT_chatObj>();
        }
        public void AdapList(IList<ReceptionChat> chatList)
        {
            foreach (ReceptionChat chat in chatList)
            {
                RespDataCHAT_chatObj chatObj = new RespDataCHAT_chatObj().Adapt(chat);
                arrayData.Add(chatObj);
            }
        }
    }
    #endregion

    #region CHAT001007
    public class ReqDataCHAT001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string target { get; set; }
        public Dianzhu.Model.Enums.enum_ChatTarget Target
        {
            get
            {
                Dianzhu.Model.Enums.enum_ChatTarget tar;
                bool isType = Enum.TryParse<Dianzhu.Model.Enums.enum_ChatTarget>(target, out tar);
                if (!isType) { throw new Exception("不可识别的用户类型"); }
                return tar;
            }
        }
        public string pageSize { get; set; }
        public string targetID { get; set; }
        public string low { get; set; }
    }
    public class RespDataCHAT001007
    {
        public IList<RespDataCHAT_chatObj> arrayData { get; set; }
        public RespDataCHAT001007()
        {
            arrayData = new List<RespDataCHAT_chatObj>();
        }
        public void AdapList(IList<ReceptionChat> chatList)
        {
            foreach (ReceptionChat chat in chatList)
            {
                RespDataCHAT_chatObj chatObj = new RespDataCHAT_chatObj().Adapt(chat);
                arrayData.Add(chatObj);
            }
        }
    }
    #endregion

    #region CHAT001008
    public class ReqDataCHAT001008
    {
        public string message { get; set; }
    }
    #endregion

}
