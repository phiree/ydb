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
using Dianzhu.Model;
using DDDCommon;
using Dianzhu.Model.Enums;

namespace Dianzhu.BLL
{
    /// <summary>
    /// 接待记录.
    /// </summary>
    public class BLLReceptionChat
    {
        
        public IDAL.IDALReceptionChat DALReceptionChat;
        public BLLReceptionChat(IDAL.IDALReceptionChat dal)
        {
            DALReceptionChat = dal;
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
        
        public IList<ReceptionChat> GetChatByOrder(ServiceOrder order)
        {
            return DALReceptionChat.GetChatByOrder(order);
        }

        public IList<ReceptionChat> FindChatByOrder(ServiceOrder order)
        {
            return DALReceptionChat.FindChatByOrder(order);
        }

        /// <summary>
        /// 条件读取聊天记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="type"></param>
        /// <param name="fromTarget"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public IList<ReceptionChat> GetChats(Model.Trait_Filtering filter, string type, string fromTarget,  Guid orderID)
        {
            var where = PredicateBuilder.True<ReceptionChat>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.ServiceOrder.Id == orderID);
            }
            if (type != null && type != "")
            {
                where = where.And(x => x.ChatType.ToString() == type);
            }
            if (fromTarget != null && fromTarget != "")
            {
                where = where.And(x => x.From.UserType.ToString() == fromTarget);
            }

            ReceptionChat baseone = null;
            if (filter.baseID != null && filter.baseID != "")
            {
                try
                {
                    baseone = DALReceptionChat.FindById(new Guid(filter.baseID));
                }
                catch
                {
                    baseone = null;
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
        /// <returns></returns>
        public long GetChatsCount(string type, string fromTarget, Guid orderID)
        {
            var where = PredicateBuilder.True<ReceptionChat>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.ServiceOrder.Id == orderID);
            }
            if (type != null && type != "")
            {
                where = where.And(x => x.ChatType.ToString() == type);
            }
            if (fromTarget != null && fromTarget != "")
            {
                where = where.And(x => x.From.UserType.ToString() == fromTarget);
            }
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
