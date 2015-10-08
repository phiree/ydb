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
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public virtual IList<ReceptionBase> Search(DZMembership from,DZMembership to, DateTime timeBegin, DateTime timeEnd,int limit)
        {
            int rowCount;
            return Search(from, to, Guid.Empty, timeBegin, timeEnd, 0, limit, out rowCount );
        }
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
        public virtual IList<ReceptionBase> Search(DZMembership from, DZMembership to,Guid orderId, DateTime timeBegin, DateTime timeEnd,
            int pageIndex,int pageSize,out int rowCount
            )
        {

            var result = BuildReceptionQuery(from, to, orderId, timeBegin, timeEnd);
            rowCount = result.RowCount();
            result.OrderBy(x => x.TimeBegin).Desc.Skip(pageIndex*pageSize).Take(pageSize).List();
            return result.List();
        }
        
        private IQueryOver<ReceptionBase,ReceptionBase> BuildReceptionQuery(DZMembership from, DZMembership to, Guid orderId, DateTime timeBegin, DateTime timeEnd)
        {
            var result = Session.QueryOver<ReceptionBase>().Where(x => x.TimeBegin >= timeBegin)
                .And(x => x.TimeBegin <= timeEnd)
                .And(x => (x.Sender == from && x.Receiver == to) || (x.Sender == to && x.Receiver == from));
            if (orderId != Guid.Empty)
            {
                result = result.And(x => x.ServiceOrder.Id == orderId);
            }
            return result;
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
        public virtual IList<ReceptionChat> SearchChat(DZMembership user, Guid orderId, DateTime timeBegin, DateTime timeEnd,
            int pageIndex, int pageSize, out int rowCount
            )
        {

            var result = BuildReceptionChatQuery(user, orderId, timeBegin, timeEnd);
            rowCount = result.RowCount();
            result.OrderBy(x => x.SavedTime).Desc.Skip(pageIndex * pageSize).Take(pageSize).List();
            return result.List();
        }

        private IQueryOver<ReceptionChat, ReceptionChat> BuildReceptionChatQuery(DZMembership user, Guid orderId, DateTime timeBegin, DateTime timeEnd)
        {
            var result = Session.QueryOver<ReceptionChat>().Where(x => x.SavedTime >= timeBegin)
                .And(x => x.SavedTime <= timeEnd)
                .And(x => (x.From == user || x.To == user));
            if (orderId != Guid.Empty)
            {
                result = result.And(x => x.ServiceOrder.Id == orderId);
            }
            return result;
        }

    }
}
