using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ydb.InstantMessage.DomainModel.Chat;
using NHibernate;
 
using System.Linq;
using Ydb.InstantMessage.DomainModel.Chat.Enums;
using Ydb.Common.Specification;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.Common.Repository;
namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate
{
    public class RepositoryChat : NHRepositoryBase<ReceptionChat,Guid>,IRepositoryChat
    {
       

        public IList<ReceptionChat> GetChatByOrder(string orderId)
        {
            return Find(x => x.SessionId == orderId).OrderByDescending(x => x.SavedTimestamp).ToList();
        }

        public long GetUnreadChatsCount(string userID)
        {
            var where = PredicateBuilder.True<ReceptionChat>();
            where = where.And(x => x.ToId == userID);
            where = where.And(x => x.IsReaded == false);
            long count = GetRowCount(where);
            return count;
        }

        public IList<ReceptionChat> GetUnreadChatsAndSetReaded(string userID)
        {
            var where = PredicateBuilder.True<ReceptionChat>();
            where = where.And(x => x.ToId == userID);
            where = where.And(x => x.IsReaded == false);
            long t = 0;
            var list = Find(where).ToList();
            foreach (var chat in list)
            {
                chat.SetReaded();
            }
            return list;
        }

        public long GetChatsCount(string type, string fromTarget, string orderID, string userID, string userType)
        {
            var where = BuildQuery(type, fromTarget, orderID, userID, userType);

            long count = GetRowCount(where);
            return count;
        }

        public IList<ReceptionChat> GetChats(TraitFilter filter, string type, string fromTarget, string orderID, string userID, string userType)
        {
            var where = BuildQuery(type, fromTarget, orderID, userID, userType);
            ReceptionChat baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }

        public IList<ReceptionChat> GetReceptionChatList(string fromId, string toId, string orderId, DateTime timeBegin, DateTime timeEnd, int pageIndex, int pageSize, ChatTarget? target, out int rowCount)
        {
            var result = BuildReceptionChatQuery(fromId, toId, orderId, timeBegin, timeEnd);
            if (!string.IsNullOrEmpty(orderId))
            {
                result = result.And(x => x.SessionId == orderId.ToString());
            }
            if (target != null)
            {
                result = result.And(x => x.ChatTarget == target);
            }

            result = result.And(x => x.ChatType == ChatType.Chat);
            rowCount = result.RowCount();
            IList<ReceptionChat> receptionChatList = new List<ReceptionChat>();
            if (pageIndex < 0 && pageSize < 0)
            {
                receptionChatList = result.OrderBy(x => x.SavedTimestamp).Desc.List().OrderBy(x => x.SavedTimestamp).ToList();
            }
            else
            {
                receptionChatList = result.OrderBy(x => x.SavedTimestamp).Desc.Skip(pageIndex * pageSize).Take(pageSize).List().OrderBy(x => x.SavedTimestamp).ToList();
            }

            return receptionChatList;
        }

        public IList<ReceptionChat> GetReceptionChatListByTargetId(string fromId, string toId, string orderId, DateTime timeBegin, DateTime timeEnd, int pageSize, double targetChatSavedTimestamp, string low, ChatTarget? target)
        {
            var result = session.QueryOver<ReceptionChat>();
            if (target != null)
            {
                result = result.And(x => x.ChatTarget == target);
            }
            if (low == "Y")
            {
                result = result.Where(x => x.SavedTimestamp < targetChatSavedTimestamp).OrderBy(x => x.SavedTimestamp).Desc;
            }
            else
            {
                result = result.Where(x => x.SavedTimestamp > targetChatSavedTimestamp).OrderBy(x => x.SavedTimestamp).Asc;
            }
            if (!string.IsNullOrEmpty( toId))
            {
                result = result.And(x => (x.FromId == fromId.ToString() && x.ToId == toId.ToString())
                || (x.FromId == toId.ToString() && x.FromId == fromId.ToString()));
            }
            else
            {
                if (!string.IsNullOrEmpty( fromId))
                {
                    result = result.And(x => (x.FromId == fromId.ToString() || x.ToId == fromId.ToString()));
                }
            }
            if (!string.IsNullOrEmpty( orderId))
            {
                result = result.And(x => x.SessionId == orderId.ToString());
            }

            IList<ReceptionChat> receptionChatList = new List<ReceptionChat>();
            receptionChatList = result.Take(pageSize).List();
            return receptionChatList;
        }


        private IQueryOver<ReceptionChat, ReceptionChat> BuildReceptionChatQuery(string fromId, string toId, string orderId, DateTime timeBegin, DateTime timeEnd)
        {

            var result = session.QueryOver<ReceptionChat>().Where(x => x.SavedTime >= timeBegin)
            .And(x => x.SavedTime <= timeEnd);
            if (!string.IsNullOrEmpty( toId))
            {
                result = result.And(x => (x.FromId == fromId.ToString() && x.ToId == toId.ToString())
                || (x.FromId == toId.ToString() && x.ToId == fromId.ToString()));
            }
            else
            {
                if (!string.IsNullOrEmpty( fromId))
                {
                    result = result.And(x => (x.FromId == fromId.ToString() || x.ToId == fromId.ToString()));
                }
            }
            if (!string.IsNullOrEmpty( orderId))
            {
                result = result.And(x => x.SessionId == orderId.ToString());
            }
            return result;

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
        private Expression<Func<ReceptionChat, bool>> BuildQuery(string type, string fromTarget, string orderID, string userID, string userType)
        {


            var where = PredicateBuilder.True<ReceptionChat>();
            if (!string.IsNullOrEmpty(orderID))
            {
                where = where.And(x => x.SessionId == orderID);
            }
            if (!string.IsNullOrEmpty(type))
            {
                // throw new NotImplementedException();
                switch (type)
                {
                    case "pushOrder":
                        //ReceptionChatPushService
                        //Type.GetType("MyFormName").IsAssignableFrom(x.GetType())
                        where = where.And(x => x is ReceptionChatPushService);
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
                        where = where.And(x => (x.FromId == userID && x.ToResource == XmppResource.YDBan_Store)
                                            || (x.ToId == userID && x.FromResource == XmppResource.YDBan_Store));
                        break;
                    case "customerService":
                        where = where.And(x => (x.FromId == userID && (x.ToResource == XmppResource.YDBan_CustomerService || x.ToResource == XmppResource.YDBan_DianDian))
                     || (x.ToId == userID && (x.FromResource == XmppResource.YDBan_CustomerService || x.FromResource == XmppResource.YDBan_DianDian)));
                        break;
                    case "customer":
                        where = where.And(x => (x.FromId == userID && x.ToResource == XmppResource.YDBan_User)
                        || (x.ToId == userID && x.FromResource == XmppResource.YDBan_User));
                        break;
                    default:
                        where = where.And(x => x.FromId == userID || x.ToId == userID);
                        break;
                }
            }
            else
            {
                switch (fromTarget)
                {
                    case "customerService":
                        where = where.And(x => (x.FromResource == XmppResource.YDBan_Store &&
                        x.ToResource == XmppResource.YDBan_CustomerService) ||
                        (x.ToResource == XmppResource.YDBan_Store && x.FromResource == XmppResource.YDBan_CustomerService));
                        break;
                    case "customer":
                        where = where.And(x => (x.FromResource == XmppResource.YDBan_Store
                         && x.ToResource == XmppResource.YDBan_User) ||
                         (x.ToResource == XmppResource.YDBan_Store && x.FromResource == XmppResource.YDBan_User));
                        break;
                    default:
                        where = where.And(x => x.FromResource == XmppResource.YDBan_Store || x.ToResource == XmppResource.YDBan_Store);
                        break;
                }
            }


            return where;
        }
    }
}
