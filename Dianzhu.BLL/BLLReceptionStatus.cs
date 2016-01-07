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
        /// 根据当前客服获取接待用户列表
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public IList<DZMembership> GetCustomListByCS(DZMembership cs)
        {
            IList<ReceptionStatus> RS = dalRS.GetListByCustomerService(cs);
            IList<DZMembership> result = RS.Select(x => x.Customer).ToList();
            return result;
        }
        /// <summary>
        /// 根据当前客服id获取接待用户与客服关联列表
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public DZMembership GetCustomListByCSId(Guid csId)
        {
            DZMembership result = dalRS.GetListByCustomerServiceId(csId);
            return result;
        }

        /// <summary>
        /// 保存重新分配
        /// </summary>
        /// <param name="c"></param>
        /// <param name="cs"></param>
        public void SaveReAssign(DZMembership c,DZMembership cs,ServiceOrder order)
        {
            ReceptionStatus r = new ReceptionStatus { Customer = c, CustomerService = cs ,Order = order};
            ReceptionStatus or = dalRS.GetOneByCustomerAndCS(cs, c);
            if (or == null)
            {
                dalRS.Save(r);
            }
            else
            {
                or.LastUpdateTime = DateTime.Now;
                dalRS.Update(or);
            }
            
        }

        public void SaveByRS(ReceptionStatus o)
        {
            dalRS.Update(o);
        }

        /// <summary>
        /// 删除分配
        /// </summary>
        /// <param name="rs"></param>
        public void DeleteAssign(DZMembership c, DZMembership cs)
        {
            ReceptionStatus r = dalRS.GetOneByCustomerAndCS(cs, c);
            dalRS.Delete(r);
        }

        /// <summary>
        /// 根据Id删除分配
        /// </summary>
        /// <param name="rs"></param>
        public void Delete(ReceptionStatus rs)
        {
            dalRS.Delete(rs);
        }

        public void UpdateOrder(DZMembership c,DZMembership cs,ServiceOrder order)
        {
            ReceptionStatus re = dalRS.GetOneByCustomerAndCS(cs, c);
            re.Order = order;
            re.LastUpdateTime = DateTime.Now;

            dalRS.Update(re);
        }

        public ReceptionStatus GetRSByDiandian(DZMembership dd)
        {
            return dalRS.GetReceptionStatusByDiandian(dd);
        }

        public ReceptionStatus GetOrder(DZMembership c,DZMembership cs)
        {
            return dalRS.GetOrder(c, cs);
        }

        /// <summary>
        /// 根据id获取用户对应的ReceptionStatus
        /// </summary>
        /// <param name="cu"></param>
        /// <returns></returns>
        public ReceptionStatus GetOneByCustomer(Guid cu)
        {
            return dalRS.GetOneByCustomer(cu);
        }

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
        //点点
        public DZMembership diandian;
        //分配策略
        IAssignStratage stratage;
        //IM会话.
        IIMSession imSession;
        DAL.DALReceptionStatus dalRS;
        DAL.DALMembership dalMember;
        public ReceptionAssigner(IAssignStratage stratage,IIMSession imSession
            , DALReceptionStatus dalRS
            , DALMembership dalMember) 
        {
            this.stratage = stratage;
            this.imSession = imSession;
            this.dalRS = dalRS;
            this.dalMember = dalMember;
        }
        public ReceptionAssigner(IIMSession imSession)
            : this(new AssignSrratageByAssNum(new DALReceptionStatus()),
                 imSession,
                 new DALReceptionStatus(),
                 new DALMembership())
        {

        }
        public ReceptionAssigner(IAssignStratage stratage,IIMSession imSession)
            :this(stratage,
                 imSession,
                 new DALReceptionStatus(),
                 new DALMembership())
        {
             
        }
        
      
        protected IList<DZMembership> CustomerServiceList 
        {
            get { 
                if (customerServiceList == null)
                {
                    customerServiceList = new List<DZMembership>();
                     //convert sesionUser to dzmembership
                    foreach (OnlineUserSession user in imSession.GetOnlineSessionUser())
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

        public virtual DZMembership Diandian
        {
            get
            {
                if (diandian == null)
                {
                    diandian = new DZMembership();
                    //convert sesionUser to dzmembership
                    foreach (OnlineUserSession user in imSession.GetOnlineSessionUser())
                    {
                        if (user.ressource.ToLower() == "ydb_diandian")
                        {
                            diandian = dalMember.GetOne(new Guid(user.username));
                            break;
                        }
                    }
                }
                return diandian;
            }
        }




        /// <summary>
        /// 客户登录
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public virtual Dictionary<DZMembership,DZMembership>  AssignCustomerLogin(DZMembership customer)
        {
             
            Dictionary < DZMembership,DZMembership> assigned= stratage.Assign(new List<DZMembership>(new DZMembership[] { customer})
                , CustomerServiceList,Diandian);
            //获取用户的接待记录.应该为空,但是当用户异常退出时会删除失败,保留了历史数据.
            dalRS.DeleteAllCustomerAssign(customer);
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
                = stratage.Assign(customerWithCS.Select(x=>x.Customer).ToList(), CustomerServiceList,Diandian);
            
            // save assign to database 
            foreach (KeyValuePair<DZMembership, DZMembership> pair in newAssign)
            {
                ReceptionStatus rs = new ReceptionStatus
                {
                    Customer = pair.Key,
                    CustomerService = pair.Value,
                    Order = new BLLReceptionStatus().GetOrder(pair.Key, customerservice).Order,
                    LastUpdateTime = DateTime.Now
                };
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
    /// <summary>
    /// IM 会话状态的接口
    /// </summary>
    public interface IIMSession
    {
        /// <summary>
        /// 获取当前会话(亦在线用户)
        /// </summary>
        /// <returns></returns>
        IList<OnlineUserSession> GetOnlineSessionUser();
    }
    /// <summary>
    /// IM 会话状态接口之: openfire的实现
    /// 通过openfire的restapi插件获取.
    /// </summary>
    public class IMSessionsOpenfire : IIMSession
    {
        string restApiUrl, restApiSecretKey;
        public IMSessionsOpenfire(string restApiUrl, string restApiSecretKey)
        {
            this.restApiSecretKey = restApiSecretKey;
            this.restApiUrl = restApiUrl;
        }

        public IList<OnlineUserSession> GetOnlineSessionUser()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            Uri uri = new Uri(restApiUrl);
            string host = uri.Host;
            wc.Headers.Add("Authorization:"+restApiSecretKey);
            wc.Headers.Add("Host: "+host);
            wc.Headers.Add("Accept: application/json");
            System.IO.Stream returnData = wc.OpenRead(uri);
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
    }

    /// <summary>
    /// 直接查询数据库中用户状态表获取
    /// </summary>
    public class IMSessionsDB : IIMSession
    {
        string xmppResource;
        public IMSessionsDB(string xmppResource)
        {
            this.xmppResource = xmppResource;
        }

        public IList<OnlineUserSession> GetOnlineSessionUser()
        {
            IList<OnlineUserSession> resultList=new List<OnlineUserSession>();
            BLLIMUserStatus bllIMUserStatus = new BLLIMUserStatus();

            IList<IMUserStatus> imList =  bllIMUserStatus.GetListByClientName(xmppResource);
            if (imList.Count > 0)
            {
                OnlineUserSession onlineUserSession;
                foreach (IMUserStatus im in imList)
                {
                    onlineUserSession = new OnlineUserSession();
                    //onlineUserSession.creationDate=
                    onlineUserSession.hostAddress = im.OFIpAddress;
                    //onlineUserSession.hostName =
                    onlineUserSession.lastActionDate = im.LastModifyTime.ToString();
                    onlineUserSession.presenceStatus = im.Status.ToString();
                    onlineUserSession.ressource = im.ClientName;
                    onlineUserSession.username = im.UserID.ToString();
                    //onlineUserSession.sessionId

                    resultList.Add(onlineUserSession);
                }
            }            

            return resultList;
        }
    }
    #region ---------------openfire restapi 在线用户数据的结构---------------
    /// <summary>
    /// 当返回只有一条数据时处理方式
    /// </summary>
    public class OnlineUserSessionResult_OnlyOne
    {
        public  OnlineUserSession  session { get; set; }
    }
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
    #endregion

    
    /// <summary>
    /// 客服分配策略
    /// </summary>
    public interface IAssignStratage
    {
        /// <summary>
        /// 将一组用户分配给一组客服
        /// </summary>
        /// <param name="customerList">待分配客户</param>
        /// <param name="csList">客服</param>
        /// <param name="diandian">如果所有客服都不在线,则分配给该用户.</param>
        /// <returns></returns>
        Dictionary<DZMembership, DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList,DZMembership diandian);
    }
  

    /// <summary>
    ///  客服分配接口实现之: 随机客服..
    /// </summary>
    public class AssignStratageRandom : IAssignStratage
    {
        static Random r = new Random();
        /// <summary>
        /// 为一组客户分配客服
        /// </summary>
        /// <param name="customerList">待分配的客户列表</param>
        /// <param name="csList">在线的客服列表</param>
        /// <returns>分配后的字典表,key 是客户,value 是客服</returns>
        public Dictionary<DZMembership,DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList, DZMembership diandian)
        {
            Dictionary<DZMembership, DZMembership> assignList = new Dictionary<DZMembership, DZMembership>();
            if (csList.Count == 0)
            {
                //r如果没有在线客服 怎么处理
                throw new Exception("客服离线");

                //foreach (DZMembership customer in customerList)
                //{
                //    assignList.Add(customer, diandian);
                //}
            }
            else
            {
                int totalCS = csList.Count;
                foreach (DZMembership customer in customerList)
                {
                    int i = r.Next(totalCS);
                    var assignedCs= csList[i];
                    assignList.Add(customer, assignedCs);
                }
            }
            
            return assignList;
            

        }
    }

    public class AssignSrratageByAssNum : IAssignStratage
    {
        DALReceptionStatus dalRS;
        public AssignSrratageByAssNum(DALReceptionStatus dal)
        {
            dalRS = dal;
        }

        public Dictionary<DZMembership, DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList, DZMembership diandian)
        {
            Dictionary<DZMembership, DZMembership> assignList = new Dictionary<DZMembership, DZMembership>();
            if (csList.Count == 0)
            {
                //r如果没有在线客服 怎么处理
                //throw new Exception("客服离线");

                foreach (DZMembership customer in customerList)
                {
                    assignList.Add(customer, diandian);
                }
            }
            else
            {
                //todo:后面可继续优化，当前是取客服接待人数最少的分配
                var csDBList = dalRS.GetCSMinCount(diandian);
                foreach (DZMembership customer in customerList)
                {
                    //assignList.Add(customer, dalRS.GetCSMinCount());
                    if (csList.Count > csDBList.Count)
                    {
                        foreach(DZMembership cs in csList)
                        {
                            if (!csDBList.Contains(cs))
                            {
                                assignList.Add(customer, cs);
                                break;
                            }
                        }
                    }
                    else
                    {
                        assignList.Add(customer, csList[0]);
                    }
                }
            }

            return assignList;
        }
    }
    /// <summary>
    /// 手动指定客服.
    /// </summary>
    public class AssignStratageManually : IAssignStratage
    {
        Guid manuallyCsId =Guid.Empty;
        public AssignStratageManually(Guid csId)
        {
            manuallyCsId = csId;
        }
        public Dictionary<DZMembership, DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList, DZMembership diandian)
        {
            Dictionary<DZMembership, DZMembership> assignList = new Dictionary<DZMembership, DZMembership>();
             
                var assign = csList.Single(x => x.Id == manuallyCsId);
                if (assign == null) { throw new NullReferenceException("该客服未登录."); }
                foreach (DZMembership customer in customerList)
                {
                    assignList.Add(customer, assign);
                }
            

            return assignList;
        }
    }
}
