using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALReception : DALBase<Model.ReceptionBase>
    {
         public DALReception()
        {
            Session = new HybridSessionBuilder().GetSession();
        }
        //注入依赖,供测试使用;
         public DALReception(string fortest)
        {
            
        }
        /// <summary>
        /// 查询接待记录
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        
        /// <summary>
        /// search reception 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="orderId"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <param name="pageIndex">base on 0</param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount">out</param>
        /// <returns></returns>
        public virtual IList<ReceptionBase> GetReceptionList(DZMembership from, DZMembership to, DateTime timeBegin, DateTime timeEnd,
            int pageIndex,int pageSize,out int rowCount
            )
        {

            var result = Session.QueryOver<ReceptionBase>().Where(x => x.TimeBegin >= timeBegin)
                .And(x => x.TimeBegin <= timeEnd)
                .And(x => (x.Sender == from && x.Receiver == to) || (x.Sender == to && x.Receiver == from));

            rowCount = result.RowCount();
            result.OrderBy(x => x.TimeBegin).Desc.Skip(pageIndex*pageSize).Take(pageSize).List();
            return result.List();
        }

        
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="orderId"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public virtual IList<ReceptionChat> GetReceptionChatList(DZMembership from,DZMembership to, Guid orderId, DateTime timeBegin, DateTime timeEnd,
            int pageIndex, int pageSize, out int rowCount
            )
        {

            var result = BuildReceptionChatQuery(from,to, orderId, timeBegin, timeEnd);
            if(orderId!=Guid.Empty)
            {
                result = result.And(x => x.ServiceOrder.Id == orderId);
            }
            rowCount = result.RowCount();
            result.OrderBy(x => x.SavedTime).Desc.Skip(pageIndex * pageSize).Take(pageSize).List();
            return result.List();
        }

        private IQueryOver<ReceptionChat, ReceptionChat> BuildReceptionChatQuery(DZMembership from,DZMembership to, Guid orderId, DateTime timeBegin, DateTime timeEnd)
        {
            var result = Session.QueryOver<ReceptionChat>().Where(x => x.SavedTime >= timeBegin)
                .And(x => x.SavedTime <= timeEnd);
            if (to != null) {
                result= result.And(x => (x.From == from && x.To == to) || (x.From == to && x.To == from));
            }
            else
            {
                result = result.And(x => (x.From == from || x.To ==from));
            }
            if (orderId != Guid.Empty)
            {
                result = result.And(x => x.ServiceOrder.Id == orderId);
            }
            return result;
        }
        private IList<ReceptionChat> BuildChatList(IList<ReceptionBase> list, int limit)
        {
            var chatList = new List<ReceptionChat>();
            foreach (ReceptionBase re in list)
            {
                if (chatList.Count > limit)
                { break; }
                chatList.AddRange(re.ChatHistory.OrderByDescending(x => x.SavedTime));
            }
            return chatList.OrderBy(x => x.SavedTime).ToList();
        }
    }
}
