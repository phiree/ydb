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
        /// 客服下线,将用户分配给其他客服.
        /// </summary>
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

        

    }
    /// <summary>
    /// 为客户分配客服.
    /// </summary>
    public class ReceptionAssigner
    {

         
        
        //被排除的客服.
        public DZMembership excluedCustomerService { get; set; }
        //待分配客户列表
        public IList<DZMembership> CustomerList { get; set; }

        //在线客服列表
        IList<DZMembership> customerServiceList;
        //分配策略
        IAssignStratage stratage;
        DAL.DALReceptionStatus dalRS;
        DAL.DALMembership dalMember;
        public ReceptionAssigner(IAssignStratage stratage
            , DALReceptionStatus dalRS
            , DALMembership dalMember) 
        {
            this.stratage = stratage;
            this.dalRS = dalRS;
            this.dalMember = dalMember;
        }
        public ReceptionAssigner(IAssignStratage stratage)
            :this(stratage
                 ,new DALReceptionStatus(),
                 new DALMembership())
        {
             
        }
        
        public string OpenfireRestAPISecredkey { get; set; }
        public string RestAPIUrl { get; set; }
        protected IList<DZMembership> CustomerServiceList
        {
            get
            {
                if (customerServiceList == null)
                {
                    customerServiceList = new List<DZMembership>();
                     
                    foreach (OnlineUserSession user in GetOnlineSessionUser())
                    {
                        if (user.ressource.ToLower()=="ydb_cstool")
                        { 
                        DZMembership cs = dalMember.GetOne(new Guid( user.username));
                        customerServiceList.Add(cs);
                        }
                    }
                }
                return customerServiceList;
            }
        }
        
         public virtual IList<OnlineUserSession> GetOnlineSessionUser()
        {
            //调用 openfire 的restapi接口,获取在线会话,即在线用户
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Headers.Add("Authorization:an4P0ja6v3rykV4H");
            wc.Headers.Add("Host: ydban.cn:9090");
            wc.Headers.Add("Accept: application/json");
            System.IO.Stream returnData = wc.OpenRead("http://ydban.cn:9090/plugins/restapi/v1/sessions");
            System.IO.StreamReader reader = new System.IO.StreamReader(returnData);
            string result = reader.ReadToEnd();
            try
            {
                OnlineUserSessionResult sessionResult
                    = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<OnlineUserSessionResult>(result);
                return sessionResult.session;
            }
            catch (Exception ex)
            {
                OnlineUserSessionResult_OnlyOne sessionResult
                   = Newtonsoft.Json.JsonConvert
                   .DeserializeObject<OnlineUserSessionResult_OnlyOne>(result);
                return new List<OnlineUserSession>(new OnlineUserSession[] { sessionResult.session });
            }
            
        }
        protected int CustomerServiceCount { get { return CustomerServiceList.Count; } }

        /// <summary>
        /// 客户登录
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public virtual Dictionary<DZMembership,DZMembership>  AssignCustomerLogin(DZMembership customer)
        {
            //使用策略 获取

            Dictionary < DZMembership,DZMembership> assigned= stratage.Assign(new List<DZMembership>(new DZMembership[] { customer})
                , CustomerServiceList);

           IList<ReceptionStatus> res= dalRS.GetListByCustomer(  customer);
            foreach (ReceptionStatus rs in res)
            {
                dalRS.Delete(rs);
            }
             ReceptionStatus newRs = new ReceptionStatus
                {
                    Customer = customer,
                    CustomerService = assigned[customer],
                    LastUpdateTime = DateTime.Now
                };
            
            dalRS.SaveOrUpdate(newRs);
            return assigned;
        }
        /// <summary>
        /// 客服下线
        /// </summary>
        /// <param name="customerservice"></param>
        /// <returns></returns>
        public virtual Dictionary<DZMembership, DZMembership> AssignCSLogoff(DZMembership customerservice)
        {
            //remove cs from list
            CustomerServiceList.Remove(customerservice);
            //get customers recepted by the cs , from database
            IList<ReceptionStatus> customerWithCS = dalRS.GetListByCustomerService(customerservice); 
            //re assign
            Dictionary<DZMembership, DZMembership> newAssign
                = stratage.Assign(customerWithCS.Select(x=>x.Customer).ToList(), CustomerServiceList);
            // save assign to database 
            foreach (KeyValuePair<DZMembership, DZMembership> pair in newAssign)
            {
                ReceptionStatus rs = new ReceptionStatus { Customer=pair.Key, CustomerService= pair.Value,
                 LastUpdateTime=DateTime.Now};
                dalRS.Save(rs);
            }
            // delete old assign to database
            foreach (ReceptionStatus oldrs in customerWithCS)
            {
                dalRS.Delete(oldrs);
            }
            //return new assign
            return newAssign;
        }

    }
    public class OnlineUserSessionResult_OnlyOne
    {
        public  OnlineUserSession  session { get; set; }
    }
    //openfire
    public class OnlineUserSessionResult {
       public IList<OnlineUserSession> session { get; set; }
         
    }
    public class OnlineUserSession {
        public DateTime creationDate { get; set; }
        public string hostAddress { get; set; }
        public string hostName { get; set; }
        public string lastActionDate { get; set; }
        public string presenceStatus { get; set; }
        public string ressource { get; set; }
        public string username { get; set; }
        public string sessionId { get; set; }
    }
    
     
    /// <summary>
    /// 客服分配接口..
    /// </summary>
    public interface IAssignStratage
    {
        Dictionary<DZMembership, DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList);
    }
    /// <summary>
    ///  随机客服.
    /// </summary>
    public class AssignStratageRandom : IAssignStratage
    {
        static Random r = new Random();
        public Dictionary<DZMembership,DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList)
        {
            Dictionary<DZMembership, DZMembership> assignList = new Dictionary<DZMembership, DZMembership>();
            if (csList.Count == 0)
            {
                //r如果没有在线客服 怎么处理
                throw new Exception("客服离线");
            }
            int totalCS = csList.Count;
            foreach (DZMembership customer in customerList)
            {
                int i = r.Next(totalCS);
                var assignedCs= csList[i];
                assignList.Add(customer, assignedCs);
            }
            return assignList;
            

        }
    }
}
