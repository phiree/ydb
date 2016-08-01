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
         public DALReceptionChat()
        {
             
        }
        //注入依赖,供测试使用;
        // public DALReceptionChat(string fortest):base(fortest)
        //{
            
        //}

        public new void Save(ReceptionChat chat)
        {
            //20160621_longphui_modify
            //if (chat.Id != Guid.Empty)
            //{
            //    base.Save(chat, chat.Id);
            //}
            //else {
            //    base.Save(chat);
            //}
            if (chat.Id != Guid.Empty)
            {
                Add(chat, chat.Id);
            }
            else
            {
                Add(chat);
            }


            //if (chat.Id != null)
            //{ Session.Save(chat, chat.Id); }
            //else {
            //    Session.Save(chat);
            //}
            //return chat;
        }


        /// <summary>
        /// 根据订单获取聊天记录列表
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IList<ReceptionChat> GetChatByOrder(ServiceOrder order)
        {
            return Find(x => x.ServiceOrder.Id == order.Id).OrderByDescending(x => x.SavedTime).ToList();
        }

        /// <summary>
        /// 查询是否有该订单的聊天记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IList<ReceptionChat> FindChatByOrder(ServiceOrder order)
        {
            
                string sql = @"SELECT * 
                    FROM receptionchat r 
                    LEFT JOIN receptionchatdd rdd ON rdd.ReceptionChat_id = r.Id 
                    WHERE r.ServiceOrder_id = '" + order.Id + @"' 
                    AND rdd.CopyFrom_id IS NULL ";

                IList list = Session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToEntityMap).List();

                if (list.Count > 0)
                {
                    IList<ReceptionChat> chatList = new List<ReceptionChat>();
                    ReceptionChat chat = new ReceptionChat();
                    DALMembership member = new DALMembership();
                    DALServiceOrder so = new DALServiceOrder();

                    for (int i = 0; i < list.Count; i++)
                    {
                        Hashtable ht = (Hashtable)list[i];

                        chat.Id = new Guid(ht["Id"].ToString());
                        if (ht["MessageBody"] != null)
                        {
                            chat.MessageBody = ht["MessageBody"].ToString() != "" ? ht["MessageBody"].ToString() : "";
                        }
                        else
                        {
                            chat.MessageBody = "";
                        }
                        chat.ReceiveTime = DateTime.Parse(ht["ReceiveTime"].ToString());
                        chat.SendTime = DateTime.Parse(ht["SendTime"].ToString());
                        //chat.To.Id = new Guid(ht["To_id"].ToString());
                        chat.To = member.GetMemberById(new Guid(ht["To_id"].ToString()));
                        chat.From = member.GetMemberById(new Guid(ht["From_id"].ToString()));
                        //chat.From.Id = new Guid(ht["From_id"].ToString());
                        //chat.Reception.Id = new Guid(ht["ReceptionBase_id"].ToString());
                        chat.SavedTime = DateTime.Parse(ht["SavedTime"].ToString());
                        int type = Int32.Parse(ht["ChatType"].ToString());
                        chat.ChatType = (Model.Enums.enum_ChatType)type;
                        //chat.ServiceOrder.Id = new Guid(ht["ServiceOrder_id"].ToString());
                        chat.ServiceOrder = so.GetOne(new Guid(ht["ServiceOrder_id"].ToString()));
                        chat.Version = Int32.Parse(ht["Version"].ToString());

                        chatList.Add(chat);
                    }
                     
                    return chatList;
                }
                else
                {
                   
                    return null;
                }
           
        }
        public virtual IList<ReceptionChat> GetReceptionChatList(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd,
            int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount)
        {
            var result = BuildReceptionChatQuery(fromId, toId, orderId, timeBegin, timeEnd);
            if (orderId != Guid.Empty)
            {
                result = result.And(x => x.ServiceOrder.Id == orderId);
            }

            switch (target)
            {
                case enum_ChatTarget.cer:
                    result = result.And(x => x.ChatTarget == enum_ChatTarget.cer);
                    break;
                case enum_ChatTarget.store:
                    result = result.And(x => x.ChatTarget == enum_ChatTarget.store);
                    break;
            }
            result = result.And(x => x.ChatType != enum_ChatType.ReAssign).And(x => x.ChatType != enum_ChatType.Notice);
            rowCount = result.RowCount();
            IList<ReceptionChat> receptionChatList = new List<ReceptionChat>();
            if (pageIndex < 0 && pageSize < 0)
            {
                receptionChatList = result.OrderBy(x => x.SavedTime).Desc.List().OrderBy(x => x.SavedTime).ToList();
            }
            else
            {
                receptionChatList = result.OrderBy(x => x.SavedTime).Desc.Skip(pageIndex * pageSize).Take(pageSize).List().OrderBy(x => x.SavedTime).ToList();
            }

            return receptionChatList;
        }

        public virtual IList<ReceptionChat> GetReceptionChatListByTargetIdAndSize(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd,
             int pageSize, ReceptionChat targetChat, string low, enum_ChatTarget target)
        {
            
                var result = Session.QueryOver<ReceptionChat>();
                switch (target)
                {
                    case enum_ChatTarget.cer:
                        result = result.And(x => x.ChatTarget == enum_ChatTarget.cer);
                        break;
                    case enum_ChatTarget.store:
                        result = result.And(x => x.ChatTarget == enum_ChatTarget.store);
                        break;
                }
                if (low == "Y")
                {
                    result = result.Where(x => x.SavedTime < targetChat.SavedTime).OrderBy(x => x.SavedTime).Desc;
                }
                else
                {
                    result = result.Where(x => x.SavedTime > targetChat.SavedTime).OrderBy(x => x.SavedTime).Desc;
                }
                if (toId != Guid.Empty)
                {
                    result = result.And(x => (x.From.Id == fromId && x.To.Id == toId) || (x.From.Id == toId && x.To.Id == fromId));
                }
                else
                {
                    if (fromId != Guid.Empty)
                    {
                        result = result.And(x => (x.From.Id == fromId || x.To.Id == fromId));
                    }
                }
                if (orderId != Guid.Empty)
                {
                    result = result.And(x => x.ServiceOrder.Id == orderId);
                }

                IList<ReceptionChat> receptionChatList = new List<ReceptionChat>();
                receptionChatList = result.Take(pageSize).List().OrderBy(x => x.SavedTime).ToList();
               
                return receptionChatList;
            
        }

        private IQueryOver<ReceptionChat, ReceptionChat> BuildReceptionChatQuery(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd)
        {
            
                var result = Session.QueryOver<ReceptionChat>().Where(x => x.SavedTime >= timeBegin)
                .And(x => x.SavedTime <= timeEnd);
                if (toId != Guid.Empty)
                {
                    result = result.And(x => (x.From.Id == fromId && x.To.Id == toId) || (x.From.Id == toId && x.To.Id == fromId));
                }
                else
                {
                    if (fromId != Guid.Empty)
                    {
                        result = result.And(x => (x.From.Id == fromId || x.To.Id == fromId));
                    }
                }
                if (orderId != Guid.Empty)
                {
                    result = result.And(x => x.ServiceOrder.Id == orderId);
                }

              

                return result;
            
        }
    }
}
