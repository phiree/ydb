using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALReceptionChatDD : DALBase<Model.ReceptionChatDD>
    {
         public DALReceptionChatDD()
        {
             
        }
        //注入依赖,供测试使用;
         public DALReceptionChatDD(string fortest):base(fortest)
        {
            
        }

        public new void Save(ReceptionChatDD chat)
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
        /// 根据订单获取聊天记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IList<ReceptionChatDD> GetChatListByOrder(ServiceOrder order)
        {
            var list = Session.QueryOver<ReceptionChatDD>().Where(x => x.ServiceOrder == order).OrderBy(x=>x.SavedTime).Desc.List();

            return list;
        }

        public bool FindChatByOrder(ServiceOrder order)
        {
            ReceptionChat chat = null;

            //var list = Session.QueryOver<ReceptionChatDD>()
            //    .Right.JoinAlias(x => x.Reception, () => chat)
            //    .Where(x => x.ServiceOrder == order)
            //    .And(x => x.CopyFrom == null)
            //    .List();

            string sql = "SELECT rdd FROM ReceptionChatDD rdd" +
                " RIGHT OUTER JOIN rdd.ReceptionChat r " +
                " WHERE r.ServiceOrder_id = '" + order.Id + "'"+
                " AND rdd.CopyFrom_id IS NULL";

            //string sql = "select s from DZService s " +
            //             //"  left join fetch s.Business b " +
            //             //" inner join b.business_abs " +
            //             //" inner join s.servicetype " +
            //             " where s.Name like '%" + keywords + "%' ";

            IQuery query = Session.CreateQuery(sql);
            var list = query.List<ReceptionChatDD>();
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
