﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.Model.Enums;

namespace Dianzhu.DAL
{
    public class DALReception : DALBase<Model.ReceptionBase>
    {
         public DALReception()
        {
             
        }
        //注入依赖,供测试使用;
         public DALReception(string fortest):base(fortest)
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
            int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount
            )
        {

            var result = BuildReceptionChatQuery(from,to, orderId, timeBegin, timeEnd);
            if(orderId!=Guid.Empty)
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

        public virtual IList<ReceptionChat> GetReceptionChatListByTargetIdAndSize(DZMembership from, DZMembership to, Guid orderId, DateTime timeBegin, DateTime timeEnd,
             int pageSize, Guid targetId, string low, enum_ChatTarget target)
        {
            ReceptionChat reChat = Session.QueryOver<ReceptionChat>().Where(x => x.Id == targetId).SingleOrDefault();

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
                result = result.Where(x => x.SavedTime < reChat.SavedTime).OrderBy(x => x.SavedTime).Desc;
            }
            else
            {
                result = result.Where(x => x.SavedTime > reChat.SavedTime).OrderBy(x => x.SavedTime).Desc;
            }
            if (to != null)
            {
                result = result.And(x => (x.From == from && x.To == to) || (x.From == to && x.To == from));
            }
            else
            {
                if (from != null)
                {
                    result = result.And(x => (x.From == from || x.To == from));
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

        private IQueryOver<ReceptionChat, ReceptionChat> BuildReceptionChatQuery(DZMembership from,DZMembership to, Guid orderId, DateTime timeBegin, DateTime timeEnd)
        {
            var result = Session.QueryOver<ReceptionChat>().Where(x => x.SavedTime >= timeBegin)
                .And(x => x.SavedTime <= timeEnd);
            if (to != null) {
                result= result.And(x => (x.From == from && x.To == to) || (x.From == to && x.To == from));
            }
            else
            {
                if(from!=null)
                {  
                result = result.And(x => (x.From == from || x.To ==from));
                }
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
