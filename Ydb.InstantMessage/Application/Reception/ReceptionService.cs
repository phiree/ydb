using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 分配关系
    /// </summary>
    public class ReceptionService : IReceptionService
    {
        DomainModel.Reception.IRepositoryReception repositoryReception;
        DomainModel.Reception.IReceptionSession receptionSession;
        public ReceptionService(DomainModel.Reception.IRepositoryReception repositoryReception,
            DomainModel.Reception.IReceptionSession receptionSession)
        {
            this.repositoryReception = repositoryReception;
            this.receptionSession = receptionSession;
        }
        /// <summary>
        /// 一个用户申请分配客服.
        /// 他可能已经被分配到一个客服
        /// </summary>
        /// <param name="customerServiceId"></param>
        public void AssignToOtherCustomerService(string customerId)
        {
            //是否已经存在分配关系
 
           
        }

        public void DeleteReception(string customerId)
        {
            throw new NotImplementedException();
        }

        public void GetOnlineCustomers(string customerServiceId)
        {
            throw new NotImplementedException();
        }

        public string AssignToCustomerService(string customerId)
        {
            var existedReceptions = repositoryReception.Find(x => x.CustomerId == customerId);
            //存在分配,且客服在线
            //assert: 如果存在分配关系,则客服必定是在线的,而且只有一对分配关系
             
            string assignedCustomerServiceId = string.Empty;
            if (existedReceptions.Count == 1)
            {
                var theone = existedReceptions[0];
                if (receptionSession.IsUserOnline(theone.CustomerServiceId))
                {
                    assignedCustomerServiceId = theone.CustomerServiceId;
                }
            }
            else if (existedReceptions.Count == 0)
            {
                //获取在线客服,并按照策略分配

            }
            else {

            }


            foreach (ReceptionStatus rs in existedReceptions)
            {

            }
            throw new NotImplementedException();
        }
    }
}
