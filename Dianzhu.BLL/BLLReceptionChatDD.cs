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
        public DALReceptionChatDD DALReceptionChatDD = null;
        public BLLReceptionChatDD() { DALReceptionChatDD = DALFactory.DALReceptionChatDD; }
        public BLLReceptionChatDD(DALReceptionChatDD dalDD)
        {
            DALReceptionChatDD = dalDD;
        }
        public void Save(ReceptionChatDD chatDD)
        {
            DALReceptionChatDD.Save(chatDD);
        }

        public IList<ReceptionChatDD> GetChatDDListByOrder(ServiceOrder order)
        {
            return DALReceptionChatDD.GetChatDDListByOrder(order);
        }
    }

}
