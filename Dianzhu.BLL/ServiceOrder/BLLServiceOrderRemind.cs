using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;

using Dianzhu.DAL;
using Dianzhu.Model.Enums;
using Dianzhu.Pay;
namespace Dianzhu.BLL
{

    public class BLLServiceOrderRemind
    {
        public DALServiceOrderRemind dalServiceOrderRemind = DALFactory.DALServiceOrderRemind;

        public void SaveOrUpdate(ServiceOrderRemind Remind)
        {
            dalServiceOrderRemind.SaveOrUpdate(Remind);
        }

        public ServiceOrderRemind GetOneByIdAndUserId(Guid Id, Guid UserId)
        {
            return dalServiceOrderRemind.GetOneByIdAndUserId(Id, UserId);
        }

        public int GetSumByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        {
            return dalServiceOrderRemind.GetSumByUserIdAndDatetime(userId, startTime, endTime);
        }

        public IList<ServiceOrderRemind> GetListByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        {
            IList<ServiceOrderRemind> remindList = new List<ServiceOrderRemind>();

            if (startTime < endTime)
            {
                remindList = dalServiceOrderRemind.GetListByUserIdAndDatetime(userId, startTime, endTime);
            }

            return remindList;
        }
    }


}
