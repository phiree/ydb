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
        /// 获取聊天记录,
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IList<ReceptionChat> GetHistoryReceptionChat(DZMembership from, DZMembership to,
            DateTime begin,DateTime end,
            int limit)
        {
          var list=  DALReception.Search(from, to, begin, end,limit);

          var chatList = new List<ReceptionChat>();
          foreach (ReceptionBase re in list)
          {
              if (chatList.Count > limit)
              { break; }
              chatList.AddRange(re.ChatHistory.OrderByDescending(x=>x.SavedTime));
          }
          return chatList.OrderBy(x=>x.SavedTime).ToList();
        }

    }

}
