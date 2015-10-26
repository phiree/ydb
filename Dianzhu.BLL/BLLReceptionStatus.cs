using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLReceptionStatus
    {
        DAL.DALReceptionStatus dalRS;
        public BLLReceptionStatus(DALReceptionStatus dalRs)
        {
            this.dalRS = dalRs;
        }
        public BLLReceptionStatus()
        {
            this.dalRS = DALFactory.DALReceptionStatus;
        }
        /// <summary>
        /// 客服登录.
        /// </summary>
        /// <param name="customerService"></param>
        public void CustomerServiceLogin(DZMembership customerService)
        {
            IList<ReceptionStatus> existedRS = dalRS.GetListByCustomerService(customerService);
            if (existedRS.Count > 0)
            {
                return;
            }
            ReceptionStatus rs = new ReceptionStatus { CustomerService = customerService };
            dalRS.Save(rs);
        }
        /// <summary>
        /// 客服下线,将用户分配给其他客服.
        /// </summary>
        public IList<ReceptionStatus> CustomerServiceLogout(DZMembership customerService)
        {
            //将当前所有客户分配给其他在线客服
            IList<ReceptionStatus> existedRS = dalRS.GetListByCustomerService(customerService);
 
            for (int i = 0; i < existedRS.Count;i++) {
                dalRS.Delete(existedRS[i]);
            }

            IList<ReceptionStatus> reAssignedList = ReAssign(existedRS, customerService);
 
            dalRS.SaveList(reAssignedList);
            return reAssignedList;
            

        }
        protected IList<ReceptionStatus> ReAssign(IList<ReceptionStatus> existedRS, DZMembership excludeCS)
        {
            //将当前所有客户分配给其他在线客服

            IList<ReceptionStatus> reAssignedList = new List<ReceptionStatus>();
            foreach (ReceptionStatus rs in existedRS)
            {

                if (rs.Customer != null)
                {
                    DZMembership reassignedCS = Assign(rs.Customer, excludeCS);
                    ReceptionStatus reassignedRS = new ReceptionStatus { Customer = rs.Customer, CustomerService = reassignedCS, LastUpdateTime = DateTime.Now };
                    reAssignedList.Add(reassignedRS);
                }
            }
            return reAssignedList;
        }
        public void CustomerLogOut(DZMembership customer)
        {
            IList<ReceptionStatus> existedRS = dalRS.GetListByCustomer(customer);
            if (existedRS.Count != 1)
            {
                //log error            
            }
            foreach (ReceptionStatus rs in existedRS)
            {
                dalRS.Delete(rs);
            }
        }
        /// <summary>
        /// 给客户分配一个客服,默认使用随机方式.
        /// </summary>
        /// <param name="customerUserName"></param>
        /// <param name="excluedCS">被排除的客服</param>
        /// <returns></returns>

        public DZMembership Assign(DZMembership customer, DZMembership excluedCS)
        {
            
            ReceptionAssigner assigner = new ReceptionAssigner(new AssignStratageRandom()) { dalRS = dalRS, excluedCustomerService = excluedCS };
            //todo:检查数据库内是否有已经存在的分配.以防异常退出导致的残留数据


            DZMembership cs = assigner.Assign(customer);
           
            System.Diagnostics.Debug.Assert(cs != null, "如果没有在线客服,如何处理");
            if (cs == null)
            {
                //没有在线客服.
                 
            }
            ReceptionStatus exitestedAssign = dalRS.GetOneByCustomerAndCS(cs, customer);
            if (exitestedAssign == null)
            {
                //将之前的分配设置为空.
                ReceptionStatus rs = new ReceptionStatus { Customer = customer, CustomerService = cs, LastUpdateTime = DateTime.Now };
                dalRS.Save(rs);
            }
            else
            {
                exitestedAssign.LastUpdateTime = DateTime.Now;
                dalRS.Update(exitestedAssign);
            }

            return cs;
        }


    }
    public class ReceptionAssigner
    {
        public DAL.DALReceptionStatus dalRS { get; set; }

        //被排除的客服.
        public DZMembership excluedCustomerService { get; set; }

        //在线客服列表
        IList<DZMembership> customerServiceList;
        IAssignStratage stratage;
        public ReceptionAssigner(IAssignStratage stratage)
        {
            this.stratage = stratage;
        }
        protected IList<DZMembership> CustomerServiceList
        {
            get
            {
                if (customerServiceList == null)
                {
                    var allOnlineCs = dalRS.GetAll<ReceptionStatus>();
                    if (excluedCustomerService != null)
                    {
                        allOnlineCs.Where(x => x.CustomerService.Id != excluedCustomerService.Id);
                    }

                    customerServiceList = allOnlineCs.Select(x=>x.CustomerService).Distinct().ToArray();
                }
                return customerServiceList;
            }
        }
        /// <summary>
        /// 被排除的客服
        /// </summary>
        /// <param name="excluedCS"></param>


        protected int CustomerServiceCount { get { return CustomerServiceList.Count; } }

        public virtual DZMembership Assign(DZMembership customer)
        {

            return stratage.Assign(customer, CustomerServiceList);
        }

    }
    /// <summary>
    /// 客服分配接口..
    /// </summary>
    public interface IAssignStratage
    {
        DZMembership Assign(DZMembership customer, IList<DZMembership> csList);
    }
    /// <summary>
    ///  随机客服.
    /// </summary>
    public class AssignStratageRandom : IAssignStratage
    {
        static Random r = new Random();
        public DZMembership Assign(DZMembership customer, IList<DZMembership> csList)
        {
            if (csList.Count == 0)
            {
                //r如果没有在线客服 怎么处理
                throw new Exception("客服离线");
            }
            int totalCS = csList.Count;
           
            int i = r.Next(totalCS);
            return csList[i];

        }
    }
}
