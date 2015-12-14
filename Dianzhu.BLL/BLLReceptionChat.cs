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
    public class BLLReceptionChat
    {
        public DALReceptionChat DALReceptionChat = null;
        public BLLReceptionChat() { DALReceptionChat = DALFactory.DALReceptionChat; }
        public BLLReceptionChat(DALReceptionChat dal)
        {
            DALReceptionChat = dal;
        }
        public void Save(ReceptionChat chat)
        {
            DALReceptionChat.Save(chat);
        }
        
        public IList<ReceptionChat> GetChatByOrder(ServiceOrder order)
        {
            return DALReceptionChat.GetChatByOrder(order);
        }
    }

}
