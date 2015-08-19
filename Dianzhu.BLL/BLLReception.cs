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
        public DALReception DALReception = DALFactory.DALReception;
        
        public void Save(ReceptionBase reception)
        {
            DALReception.SaveOrUpdate(reception);
        }
        public IList<ReceptionChat> GetHistoryReceptionChat(DZMembership from, DZMembership to,int limit)
        {
          var list=  DALReception.Search(from, to, DateTime.MinValue, DateTime.MaxValue,limit);

          var chatList = new List<ReceptionChat>();
          foreach (ReceptionBase re in list)
          {
              if (chatList.Count > limit)
              { break; }
              chatList.AddRange(re.ChatHistory.OrderBy(x=>x.ReceiveTime));
          }
          return chatList;
        }

    }

}
