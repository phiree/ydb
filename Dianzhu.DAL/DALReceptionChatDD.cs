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
    }
}
