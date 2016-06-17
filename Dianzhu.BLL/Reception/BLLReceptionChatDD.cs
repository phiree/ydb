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
    public class BLLReceptionChatDD
    {
        public IDAL.IDALReceptionChatDD DALReceptionChatDD;
        public BLLReceptionChatDD(IDAL.IDALReceptionChatDD dal)
        {
            DALReceptionChatDD = dal;
        }
        public void Save(ReceptionChatDD chatDD)
        {
            DALReceptionChatDD.Add(chatDD);
        }

        public IList<ReceptionChatDD> GetChatDDListByOrder(IList<DZMembership> list)
        {
            return DALReceptionChatDD.GetChatDDListByOrder(list);
        }

        public IList<ServiceOrder> GetCustomListDistinctFrom(int num)
        {
            return DALReceptionChatDD.GetCustomListDistinctFrom(num);
        }
    }

}
