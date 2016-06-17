using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using System.Device.Location;
using Dianzhu.Model;
using System.Web.Security;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Dianzhu.Model;
using Dianzhu.Model.Enums;

namespace Dianzhu.BLL
{
    /// <summary>
    /// 接待记录.
    /// </summary>
    public class BLLReceptionChat
    {
        public IDAL.IDALReceptionChat DALReceptionChat;
        public BLLReceptionChat(IDAL.IDALReceptionChat dal)
        {
            DALReceptionChat = dal;
        }
        public ReceptionChat GetOne(Guid id)
        {
            return DALReceptionChat.FindById(id);
        }
        public void Save(ReceptionChat chat)
        {
            DALReceptionChat.Add(chat);
        }
        
        public IList<ReceptionChat> GetChatByOrder(ServiceOrder order)
        {
            return DALReceptionChat.GetChatByOrder(order);
        }

        public IList<ReceptionChat> FindChatByOrder(ServiceOrder order)
        {
            return DALReceptionChat.FindChatByOrder(order);
        }

        public IList<ReceptionChat> GetReceptionChatList(DZMembership from, DZMembership to
          , Guid orderId, DateTime begin, DateTime end, int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount)
        {
            var list = DALReceptionChat.GetReceptionChatList(from, to, orderId, begin, end, pageIndex, pageSize, target, out rowCount);
            return list;
        }
        public IList<ReceptionChat> GetChatListByOrder(Guid orderId, DateTime begin, DateTime end, int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount)
        {

            return GetReceptionChatList(null, null, orderId, begin, end, pageIndex, pageSize, target, out rowCount);
        }
        public IList<ReceptionChat> GetReceptionChatListByTargetIdAndSize(DZMembership from, DZMembership to, Guid orderId, DateTime begin, DateTime end,
             int pageSize, ReceptionChat targetChat, string low, enum_ChatTarget target)
        {
            return DALReceptionChat.GetReceptionChatListByTargetIdAndSize(from, to, orderId, begin, end, pageSize, targetChat, low, target);
        }
    }

}
