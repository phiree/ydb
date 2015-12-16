using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

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
        public bool FindChatByOrder(ServiceOrder order)
        {
            //var chatList = Session.QueryOver<ReceptionChat>().Where(x => x.ServiceOrder == order).List();
            //if (chatList.Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            ReceptionChatDD c = null;
            var list = Session.QueryOver<ReceptionChat>()
                .Left.JoinAlias(x => x.Id, () => c)
                .Where(x => x.ServiceOrder == order)
                .And(()=>c.CopyFrom == null)
                .List<ReceptionChat>();
            //string sql = "SELECT r FROM ReceptionChat r" +
            //    " LEFT JOIN r.receptionchatdd rdd " +
            //    " WHERE r.ServiceOrder_id = '" + order.Id + "'" +
            //    " AND rdd.CopyFrom_id IS NULL";

            //string sql = "select s from DZService s " +
            //             //"  left join fetch s.Business b " +
            //             //" inner join b.business_abs " +
            //             //" inner join s.servicetype " +
            //             " where s.Name like '%" + keywords + "%' ";

            //IQuery query = Session.CreateQuery(sql);
            //var result = query.List<ReceptionChat>();
            if (list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
