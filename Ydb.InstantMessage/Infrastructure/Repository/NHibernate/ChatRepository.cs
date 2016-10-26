using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ydb.InstantMessage.DomainModel.Chat;
using NHibernate;
using Ydb.Common.Repository;
using System.Linq;
using Ydb.InstantMessage.DomainModel.Chat.Enums;
using Ydb.Common.Repository;
namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate
{
    public class RepositoryChat : NHRepositoryBase<ReceptionChat,Guid>,IRepositoryChat
    {
       

        public IList<ReceptionChat> GetChatByOrder(string orderId)
        {
            return Find(x => x.SessionId == orderId).OrderByDescending(x => x.SavedTimestamp).ToList();
        }

        public IList<ReceptionChat> GetReceptionChatList(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd, int pageIndex, int pageSize, ChatTarget? target, out int rowCount)
        {
            var result = BuildReceptionChatQuery(fromId, toId, orderId, timeBegin, timeEnd);
            if (orderId != Guid.Empty)
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

        public IList<ReceptionChat> GetReceptionChatListByTargetId(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd, int pageSize, double targetChatSavedTimestamp, string low, ChatTarget? target)
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
            if (toId != Guid.Empty)
            {
                result = result.And(x => (x.FromId == fromId.ToString() && x.ToId == toId.ToString())
                || (x.FromId == toId.ToString() && x.FromId == fromId.ToString()));
            }
            else
            {
                if (fromId != Guid.Empty)
                {
                    result = result.And(x => (x.FromId == fromId.ToString() || x.ToId == fromId.ToString()));
                }
            }
            if (orderId != Guid.Empty)
            {
                result = result.And(x => x.SessionId == orderId.ToString());
            }

            IList<ReceptionChat> receptionChatList = new List<ReceptionChat>();
            receptionChatList = result.Take(pageSize).List();
            return receptionChatList;
        }


        private IQueryOver<ReceptionChat, ReceptionChat> BuildReceptionChatQuery(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd)
        {

            var result = session.QueryOver<ReceptionChat>().Where(x => x.SavedTime >= timeBegin)
            .And(x => x.SavedTime <= timeEnd);
            if (toId != Guid.Empty)
            {
                result = result.And(x => (x.FromId == fromId.ToString() && x.ToId == toId.ToString())
                || (x.FromId == toId.ToString() && x.ToId == fromId.ToString()));
            }
            else
            {
                if (fromId != Guid.Empty)
                {
                    result = result.And(x => (x.FromId == fromId.ToString() || x.ToId == fromId.ToString()));
                }
            }
            if (orderId != Guid.Empty)
            {
                result = result.And(x => x.SessionId == orderId.ToString());
            }
            return result;

        }
    }
}
