using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using System.Collections;
using NHibernate.Transform;
using Dianzhu.Model.Enums;

namespace Dianzhu.DAL
{
    public class DALReceptionChat : NHRepositoryBase<ReceptionChat, Guid>, IDAL.IDALReceptionChat //: DALBase<Model.ReceptionChat>
    {
 
        public   void Save(ReceptionChat chat)
        {
 
            if (chat.Id != Guid.Empty)
            {
                Add(chat, chat.Id);
            }
            else
            {
                Add(chat);
            }
 
        }


        /// <summary>
        /// 根据订单获取聊天记录列表
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IList<ReceptionChat> GetChatByOrder(string  orderId)
        {
            return Find(x => x.SessionId ==orderId).OrderByDescending(x => x.SavedTimestamp).ToList();
        }

        /// <summary>
        /// 查询聊天记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
          public virtual IList<ReceptionChat> GetReceptionChatList(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd,
            int pageIndex, int pageSize, enum_ChatTarget? target, out int rowCount)
        {
            var result = BuildReceptionChatQuery(fromId, toId, orderId, timeBegin, timeEnd);
            if (orderId != Guid.Empty)
            {
                result = result.And(x => x.SessionId == orderId.ToString());
            }
            if (target != null)
            {
                result= result.And(x => x.ChatTarget == target.Value);
            }
             
            result = result.And(x => x.ChatType == enum_ChatType.Chat);
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

        public virtual IList<ReceptionChat> GetReceptionChatListByTargetIdAndSize(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd,
             int pageSize, double targetChatSavedTimestamp, string low, enum_ChatTarget? target)
        {
            
                var result = Session.QueryOver<ReceptionChat>();
                if (target != null)
                {
                    result = result.And(x => x.ChatTarget == target.Value);
                }
            if (low == "Y")
                {
                    result = result.Where(x => x.SavedTimestamp < targetChatSavedTimestamp).OrderBy(x => x.SavedTimestamp).Desc;
                }
                else
                {
                    result = result.Where(x => x.SavedTimestamp > targetChatSavedTimestamp).OrderBy(x => x.SavedTimestamp).Desc;
                }
                if (toId != Guid.Empty)
                {
                    result = result.And(x => (x.FromId == fromId.ToString() && x.ToId == toId.ToString())
                    || (x.FromId == toId.ToString()&& x.FromId == fromId.ToString()));
                }
                else
                {
                    if (fromId != Guid.Empty)
                    {
                        result = result.And(x => (x.FromId == fromId.ToString()|| x.ToId == fromId.ToString()));
                    }
                }
                if (orderId != Guid.Empty)
                {
                    result = result.And(x => x.SessionId == orderId.ToString());
                }

                IList<ReceptionChat> receptionChatList = new List<ReceptionChat>();
                receptionChatList = result.Take(pageSize).List().OrderBy(x => x.SavedTimestamp).ToList();
                return receptionChatList;
            
        }

        private IQueryOver<ReceptionChat, ReceptionChat> BuildReceptionChatQuery(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd)
        {
            
                var result = Session.QueryOver<ReceptionChat>().Where(x => x.SavedTime >= timeBegin)
                .And(x => x.SavedTime <= timeEnd);
                if (toId != Guid.Empty)
                {
                    result = result.And(x => (x.FromId== fromId.ToString() && x.ToId== toId.ToString())
                    || (x.FromId== toId.ToString() && x.ToId == fromId.ToString()));
                }
                else
                {
                    if (fromId != Guid.Empty)
                    {
                        result = result.And(x => (x.FromId== fromId.ToString() || x.ToId == fromId.ToString()));
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
