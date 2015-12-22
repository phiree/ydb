using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using System.Collections;
using NHibernate.Transform;

namespace Dianzhu.DAL
{
    public class DALReceptionChat : DALBase<Model.ReceptionChat>
    {
         public DALReceptionChat()
        {
             
        }
        //注入依赖,供测试使用;
         public DALReceptionChat(string fortest):base(fortest)
        {
            
        }

        public new void Save(ReceptionChat chat)
        {
            if (chat.Id != Guid.Empty)
            {
                base.Save(chat, chat.Id);
            }
            else {
                base.Save(chat);
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
            var list = Session.QueryOver<ReceptionChat>().Where(x => x.ServiceOrder == order).OrderBy(x=>x.SavedTime).Desc.List();

            return list;
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

                    chat.Id =new Guid( ht["Id"].ToString());
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
                    chat.ServiceOrder =so.GetOne( new Guid(ht["ServiceOrder_id"].ToString()));
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
    }
}
