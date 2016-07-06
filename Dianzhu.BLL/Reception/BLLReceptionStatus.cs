using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
using System.Diagnostics;
namespace Dianzhu.BLL
{
    public class BLLReceptionStatus
    {
        
        IDAL.IDALReceptionStatus dalRS;
        public BLLReceptionStatus(IDAL.IDALReceptionStatus dalRs)
        {
            this.dalRS = dalRs;
        }
        
       
        public IList<ReceptionStatus> GetAllList()
        {
            return dalRS.Find(x => true);
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
        public IList<ReceptionStatus> GetRsListByCS(DZMembership cs)
        {
            return dalRS.GetListByCustomerService(cs);
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
                dalRS.Add(r);
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
            if (re != null)
            {
                re.Order = order;
                re.LastUpdateTime = DateTime.Now;

                dalRS.Update(re);
            }
        }

        /// <summary>
        /// 根据点点获取当前在线用户
        /// </summary>
        /// <param name="dd"></param>
        /// <returns></returns>
        public IList<ReceptionStatus> GetRSListByDiandian(DZMembership dd,int num)
        {
            return dalRS.GetRSListByDiandian(dd,num);
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
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.ReceptionAssigner");


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
        IDAL.IDALReceptionStatus dalRS;
        IDAL.IDALMembership dalMember;
        public ReceptionAssigner(IAssignStratage stratage,IIMSession imSession
            , IDAL.IDALReceptionStatus dalRS
            , IDAL.IDALMembership dalMember) 
        {
            this.stratage = stratage;
            this.imSession = imSession;
            this.dalRS = dalRS;
            this.dalMember = dalMember;
        }
       

       
        
      
        protected IList<DZMembership> CustomerServiceList 
        {
            get {
                //if (customerServiceList != null && customerServiceList.Count > 0)
                //{

                //}
                //else
                //{
                    customerServiceList = new List<DZMembership>();
                    //convert sesionUser to dzmembership
                    foreach (OnlineUserSession user in imSession.GetOnlineSessionUser(Model.Enums.enum_XmppResource.YDBan_CustomerService.ToString()))
                    {
                        DZMembership cs = dalMember.FindById(new Guid(user.username));
                        customerServiceList.Add(cs);
                    }
                //}
                return customerServiceList;
            }
        }

        public virtual DZMembership Diandian
        {
            get
            {
                if (diandian == null )
                {
                    IList<OnlineUserSession> onlineUsers = imSession.GetOnlineSessionUser(Model.Enums.enum_XmppResource.YDBan_DianDian.ToString());
                    Debug.Assert(onlineUsers.Count != 1, "点点没有登录，或者有多个点点账户登录");
                    string errMsg = string.Empty;
                    if (onlineUsers.Count == 1)
                    {
                        diandian = dalMember.FindById(new Guid(onlineUsers[0].username));
                    }
                    else if (onlineUsers.Count == 0)
                    {
                        errMsg = "点点没有登录成功";
                        Logging.Log.Error(errMsg);
                        throw new Exception(errMsg);
                    }
                    else
                    {
                        errMsg = "多个点点账号登录";
                        Logging.Log.Error(errMsg);
                        throw new Exception(errMsg);
                    }
                    //convert sesionUser to dzmembership
                    
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
            foreach(KeyValuePair<DZMembership,DZMembership> item in assigned)
            {
                log.Debug("数据中用户id：" + item.Key.Id.ToString());
                log.Debug("数据中客服id：" + item.Value.Id.ToString());
            }
            dalRS.DeleteAllCustomerAssign(customer);
             ReceptionStatus newRs = new ReceptionStatus
                {
                    Customer = customer,
                    CustomerService = assigned[customer],
                    LastUpdateTime = DateTime.Now
                };
            log.Debug("待分配的用户id：" + customer.Id.ToString());
            log.Debug("待分配的客服id：" + assigned[customer].Id.ToString());
            
            dalRS.Add(newRs);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

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
            //CustomerServiceList.Remove(customerservice);
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
                    Order = dalRS.GetOrder(pair.Key, customerservice).Order,
                    LastUpdateTime = DateTime.Now
                };
                dalRS.Add(rs);
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            }

            // delete old assign to database
            foreach (ReceptionStatus oldrs in customerWithCS)
            {
                dalRS.Delete(oldrs);
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
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
        IList<OnlineUserSession> GetOnlineSessionUser(string xmppResource);
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
            wc.Headers.Add("Authorization:" + restApiSecretKey);
            wc.Headers.Add("Host: " + host);
            wc.Headers.Add("Accept: application/json");
            //System.Threading.Thread.Sleep(2000);
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
        public IList<OnlineUserSession> GetOnlineSessionUser(string xmppResource)
        {
            IList<OnlineUserSession> onlineUsers = GetOnlineSessionUser();
            if (onlineUsers == null)
            {
                return null;
            }
            var filteredByResourceName = onlineUsers.Where(x => x.ressource == xmppResource);
            return filteredByResourceName.ToList();
        }
    }

    /// <summary>
    /// 直接查询数据库中用户状态表获取
    /// </summary>
    public class IMSessionsDB : IIMSession
    {
        IDAL.IDALIMUserStatus dalIMUserStatus;
        public IMSessionsDB(IDAL.IDALIMUserStatus dal)
        {
            dalIMUserStatus = dal;
        }

        public IList<OnlineUserSession> GetOnlineSessionUser(string xmppResource)
        {
            IList<OnlineUserSession> resultList=new List<OnlineUserSession>();

            IList<IMUserStatus> imList = dalIMUserStatus.GetOnlineListByClientName(xmppResource);
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
    public abstract class IAssignStratage
    {
        /// <summary>
        /// 将一组用户分配给一组客服
        /// </summary>
        /// <param name="customerList">待分配客户</param>
        /// <param name="csList">客服</param>
        /// <param name="diandian">如果所有客服都不在线,则分配给该用户.</param>
        /// <returns></returns>
      public  abstract Dictionary<DZMembership, DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList, DZMembership diandian);

        /// <summary>
        /// 更新客户的呼叫次数
        /// </summary>
        /// <param name="customerList"></param>
         
    }
  

    /// <summary>
    ///  客服分配接口实现之: 随机客服..
    /// </summary>
    public class AssignStratageRandom : IAssignStratage
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.AssignStratageRandom");
        static Random r = new Random();
        /// <summary>
        /// 为一组客户分配客服
        /// </summary>
        /// <param name="customerList">待分配的客户列表</param>
        /// <param name="csList">在线的客服列表</param>
        /// <returns>分配后的字典表,key 是客户,value 是客服</returns>
        public override Dictionary<DZMembership,DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList, DZMembership diandian)
        {
            log.Debug("调用随机分配");
            Dictionary<DZMembership, DZMembership> assignList = new Dictionary<DZMembership, DZMembership>();
            if (csList.Count == 0)
            {
                //如果没有在线客服 怎么处理
                //throw new Exception("客服离线");

                //如果没有在线客服，分配给点点
                foreach (DZMembership customer in customerList)
                {
                    assignList.Add(customer, diandian);
                }
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
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.AssignSrratageByAssNum");
        DALReceptionStatus dalRS;
        public AssignSrratageByAssNum(DALReceptionStatus dal)
        {
            dalRS = dal;
        }

        public override Dictionary<DZMembership, DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList, DZMembership diandian)
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
                //emergency:点点账户是否登录 影响到 客服是否分配， 需要改善。 
                //todo:后面可继续优化，当前是取客服接待人数最少的分配
                var csDBList = dalRS.GetCSMinCount(diandian);
                log.Debug("receptionstatus中按接待人数最少查询出的数组的数量是：" + csDBList.Count);
                foreach (DZMembership customer in customerList)
                {
                    log.Debug("待分配用户id：" + customer.Id.ToString());
                    //assignList.Add(customer, dalRS.GetCSMinCount());
                    if (csList.Count > csDBList.Count)
                    {
                        log.Debug("客服列表用户数量大于已分配列表中客服的数量");
                        foreach(DZMembership cs in csList)
                        {
                            log.Debug("待分配客服列表中第一个客服id：" + cs.Id.ToString());
                            if (!csDBList.Contains(cs))
                            {
                                log.Debug("当前已分配列表中没有该客服，将用户分配给该客服");
                                assignList.Add(customer, cs);
                                break;
                            }
                        }
                    }
                    else
                    {
                        log.Debug("直接分配");
                        log.Debug("待分配的客服id：" + csDBList[0].Id.ToString());
                        assignList.Add(customer, csDBList[0]);
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
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.AssignStratageManually");
        Guid manuallyCsId =Guid.Empty;
        public AssignStratageManually(Guid csId)
        {
            manuallyCsId = csId;
        }
        public override Dictionary<DZMembership, DZMembership> Assign(IList<DZMembership> customerList, IList<DZMembership> csList, DZMembership diandian)
        {
            log.Debug("调用手动分配");
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
