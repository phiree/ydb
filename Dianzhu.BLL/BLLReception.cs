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
using Dianzhu.Model.Enums;

namespace Dianzhu.BLL
{
    /// <summary>
    /// 接待记录.
    /// </summary>
    public class BLLReception
    {
        public DALReception DALReception = null;
        public BLLReception() {   DALReception = DALFactory.DALReception; }
        public BLLReception(DALReception dal)
        {
            DALReception = dal;
        }
        public void Save(ReceptionBase reception)
        {
            DALReception.SaveOrUpdate(reception);
        }
        
        public IList<ReceptionChat> GetReceptionChatList(DZMembership from, DZMembership to
          ,Guid orderId, DateTime begin, DateTime end,int pageIndex,int pageSize, enum_ChatTarget target, out int rowCount)
        {
            var list = DALReception.GetReceptionChatList(from, to,orderId, begin, end,pageIndex,pageSize, target,out rowCount);
            return list;
        }
        public IList<ReceptionChat> GetChatListByOrder(Guid orderId, DateTime begin, DateTime end, int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount)
        {

            return GetReceptionChatList(null, null, orderId, begin, end, pageIndex, pageSize, target, out rowCount);
        }
        public IList<ReceptionChat> GetReceptionChatListByTargetIdAndSize(DZMembership from, DZMembership to, Guid orderId, DateTime begin, DateTime end,
             int pageSize, ReceptionChat targetChat, string low, enum_ChatTarget target)
        {
            return DALReception.GetReceptionChatListByTargetIdAndSize(from, to, orderId, begin, end, pageSize, targetChat, low, target);
        }
    }

}
