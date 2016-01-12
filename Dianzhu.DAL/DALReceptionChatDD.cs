using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate.Criterion;

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
        /// 根据用户获取点点聊天表中未复制过的聊天记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IList<ReceptionChatDD> GetChatDDListByOrder(IList< DZMembership> fromList)
        {
            List<ReceptionChatDD> result = new List<ReceptionChatDD>();
            
            foreach (DZMembership from in fromList)
            {
                var list = Session.QueryOver<ReceptionChatDD>().Where(x => x.From == from).And(x => x.IsCopy == false).List();

                result.AddRange(list);
            }

            return result;
        }

        /// <summary>
        /// 根据数量获取用户列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetCustomListDistinctFrom(int num)
        {
            var cList = Session.QueryOver<ReceptionChatDD>().SelectList(
                list => list
                .Select(Projections.Distinct(Projections.Property<ReceptionChatDD>(x => x.ServiceOrder)))).Where(x => x.IsCopy == false).Take(num).List<ServiceOrder>();

            return cList;
        }
    }
}
