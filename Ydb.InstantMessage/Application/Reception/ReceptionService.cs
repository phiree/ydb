using Castle.Facilities.NHibernateIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using NHibernate;
namespace Ydb.InstantMessage.Application.Reception
{
    /// <summary>
    /// 分配关系
    /// </summary>
    public class ReceptionService : IReceptionService
    {
        DomainModel.Reception.IRepositoryReception receptionRepository;
        DomainModel.Reception.IReceptionSession receptionSession;
        
        IReceptionAssigner receptionAssigner;
       
        ISession session;
        public ReceptionService(IRepositoryReception receptionRepository,ISession session,IReceptionAssigner assigner)
        {
            this.receptionRepository = receptionRepository;
            this.session = session;
            this.receptionAssigner = assigner;
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

        public string AssignToCustomerService(string customerId,string orderId)
        {
            using (var tr = session.BeginTransaction())
            {
                var existedReceptionForCustomer = receptionRepository.FindByCustomerId(customerId);
        
            string assignedCustomerServiceId= receptionAssigner.AssignCustomerServiceToCustomer(existedReceptionForCustomer, customerId);

            ReceptionStatus rs = ReceptionStatus.Create(customerId, assignedCustomerServiceId, DateTime.Now, orderId);
          
                receptionRepository.Add(rs);
                tr.Commit();
                return assignedCustomerServiceId;
            }
          
        }
    }
}
