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
        /// <summary>
        /// 获取聊天记录,根据接待记录
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IList<ReceptionChat> GetHistoryReceptionChat(DZMembership from, DZMembership to,
            DateTime begin,DateTime end,
            int limit)
        {
            int rowCount;
            return GetHistoryReceptionChat(from, to, Guid.Empty, begin, end, 0,limit,out rowCount );
        }
        public IList<ReceptionChat> GetHistoryReceptionChat(DZMembership from, DZMembership to
          ,Guid orderId, DateTime begin, DateTime end,
           int pageIndex,int pageSize,out int rowCount)
        {
            var list = DALReception.Search(from, to,orderId, begin, end,pageIndex,pageSize,out rowCount);

            return BuildChatList(list, pageSize);
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


        public IList<ReceptionChat> GetHistoryReceptionChat(DZMembership user, Guid orderId, out int rowCount)
        {
            return DALReception.SearchChat(user, orderId, DateTime.MinValue, DateTime.MaxValue, 0, 1, out rowCount);
        }
    }

}
