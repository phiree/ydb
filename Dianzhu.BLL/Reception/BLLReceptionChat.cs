using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.IDAL;
using System.Device.Location;
using Dianzhu.Model;
using System.Web.Security;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using DDDCommon;
using Dianzhu.Model.Enums;
using System.Linq.Expressions;

namespace Dianzhu.BLL
{
    /// <summary>
    /// 接待记录.
    /// </summary>
    public class BLLReceptionChat
    {
        
        private IDAL.IDALReceptionChat DALReceptionChat;
        private IDAL.IDALMembership dalMembership;
        public BLLReceptionChat(IDAL.IDALReceptionChat dal,IDALMembership dalMembership)
        {
            DALReceptionChat = dal;
            this.dalMembership = dalMembership;
        }
        public ReceptionChat GetOne(Guid id)
        {
            //20160621_longphui_modify
            //return DALReceptionChat.GetOne(id);
            return DALReceptionChat.FindById(id);
        }
        public void Save(ReceptionChat chat)
        {
            DALReceptionChat.Add(chat);
        }
   

        

        /// <summary>
        /// 赛选器
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromTarget"></param>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        private Expression<Func<ReceptionChat, bool>> BuildQuery(string type, string fromTarget, Guid orderID, Guid userID, string userType)
        {
 
 
            var where = PredicateBuilder.True<ReceptionChat>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.SessionId == orderID.ToString());
            }
            if (!string.IsNullOrEmpty(type))
            {
               // throw new NotImplementedException();
                switch (type)
                {
                    case "pushOrder":
                        //ReceptionChatPushService
                        //Type.GetType("MyFormName").IsAssignableFrom(x.GetType())
                       where = where.And(x=>x is ReceptionChatPushService);
                        break;
                    case "chat":
                        
                        break;
                    default:
                        where = where.And(x => x is ReceptionChatMedia);
                      //  where = where.And(x => x.ChatType == enum_ChatType.Media && ((ReceptionChatMedia)x).MediaType == type);
                        break;
                }
            }
            if (userType == "customer" || userType == "customerservice")
            {
                switch (fromTarget)
                {
                    case "store":
                        where = where.And(x => (x.FromId == userID.ToString() && x.ToResource == enum_XmppResource.YDBan_Store)
                                            || (x.ToId == userID.ToString() && x.FromResource ==  enum_XmppResource.YDBan_Store));
                        break;
                    case "customerService":
                        where = where.And(x => (x.FromId == userID.ToString() && (x.ToResource ==  enum_XmppResource.YDBan_CustomerService || x.ToResource == enum_XmppResource.YDBan_DianDian)) 
                     || (x.ToId == userID.ToString() && (x.FromResource ==  enum_XmppResource.YDBan_CustomerService ||x.FromResource== enum_XmppResource.YDBan_DianDian)));
                        break;
                    case "customer":
                        where = where.And(x => (x.FromId == userID.ToString() && x.ToResource == enum_XmppResource.YDBan_User)
                        || (x.ToId == userID.ToString() && x.FromResource== enum_XmppResource.YDBan_User));
                        break;
                    default:
                        where = where.And(x => x.FromId == userID.ToString() || x.ToId == userID.ToString());
                        break;
                }
            }
            else
            {
                switch (fromTarget)
                {
                    case "customerService":
                        where = where.And(x => (x.FromResource == enum_XmppResource.YDBan_Store &&
                        x.ToResource == enum_XmppResource.YDBan_CustomerService) ||
                        (x.ToResource ==  enum_XmppResource.YDBan_Store && x.FromResource ==  enum_XmppResource.YDBan_CustomerService));
                        break;
                    case "customer":
                        where = where.And(x => (x.FromResource == enum_XmppResource.YDBan_Store
                         && x.ToResource == enum_XmppResource.YDBan_User) || 
                         (x.ToResource ==  enum_XmppResource.YDBan_Store&& x.FromResource == enum_XmppResource.YDBan_User   ));
                        break;
                    default:
                        where = where.And(x => x.FromResource == enum_XmppResource.YDBan_Store  || x.ToResource == enum_XmppResource.YDBan_Store  );
                        break;
                }
            }
 

            return where;
        }


        /// <summary>
        /// 条件读取聊天记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="type"></param>
        /// <param name="fromTarget"></param>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public IList<ReceptionChat> GetChats(Model.Trait_Filtering filter, string type, string fromTarget,  Guid orderID,Guid userID,string userType)
        {
            var where = BuildQuery(type, fromTarget, orderID, userID, userType);
            ReceptionChat baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = DALReceptionChat.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? DALReceptionChat.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : DALReceptionChat.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }

        /// <summary>
        /// 统计聊天信息的数量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromTarget"></param>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public long GetChatsCount(string type, string fromTarget, Guid orderID, Guid userID, string userType)
        {
            var where = BuildQuery(type, fromTarget, orderID, userID, userType);
 
            long count = DALReceptionChat.GetRowCount(where);
            return count;
        }


 
        /// <summary>
        /// 条件读取未读聊天记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="type"></param>
        /// <param name="fromTarget"></param>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public IList<ReceptionChat> GetUnreadChatsAndSetReaded( Guid userID)
        {
            var where = PredicateBuilder.True<ReceptionChat>();
            where = where.And(x => x.ToId== userID.ToString());
            where = where.And(x => x.IsReaded==false);
            long t = 0;
            var list =  DALReceptionChat.Find(where).ToList();
            foreach (var chat in list)
            {
                chat.SetReaded();
            }
            return list;
        }

        /// <summary>
        /// 统计未读聊天信息的数量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromTarget"></param>
        /// <param name="orderID"></param>
        /// <param name="userID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public long GetUnreadChatsCount( Guid userID)
        {
            var where = PredicateBuilder.True<ReceptionChat>();
            where = where.And(x => x.ToId == userID.ToString());
            where = where.And(x => x.IsReaded == false);
            long count = DALReceptionChat.GetRowCount(where);
            return count;
 
        }

        public IList<ReceptionChat> GetReceptionChatList(Guid fromId, Guid toId, Guid orderId, DateTime begin, DateTime end, 
            int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount)
        {
            var list = DALReceptionChat.GetReceptionChatList(fromId, toId, orderId, begin, end, pageIndex, pageSize, target, out rowCount);
            return list;
        }
        public IList<ReceptionChat> GetChatListByOrder(Guid orderId, DateTime begin, DateTime end, int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount)
        {
            return GetReceptionChatList(Guid.Empty, Guid.Empty, orderId, begin, end, pageIndex, pageSize, target, out rowCount);
        }
        public IList<ReceptionChat> GetReceptionChatListByTargetIdAndSize(Guid fromId, Guid toId, Guid orderId, DateTime begin, DateTime end,
             int pageSize, ReceptionChat targetChat, string low, enum_ChatTarget target)
        {
            return DALReceptionChat.GetReceptionChatListByTargetIdAndSize(fromId, toId, orderId, begin, end, pageSize, targetChat, low, target);

        }
        
    }

}
