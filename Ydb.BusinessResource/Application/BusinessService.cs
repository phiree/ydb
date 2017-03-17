using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
using Ydb.BusinessResource.Infrastructure;
using Ydb.Common.Application;
using Ydb.Common.Domain;
using Ydb.BusinessResource.DomainModel.Service.DataStatistics;
using Ydb.BusinessResource.Infrastructure.YdbNHibernate.UnitOfWork;

namespace Ydb.BusinessResource.Application
{
   public class BusinessService:IBusinessService
    {
        IRepositoryBusiness repositoryBusiness;
        IStatisticsBusinessCount statisticsBusinessCount;
        //private ISessionFactory sessionFactory;
        public BusinessService(IRepositoryBusiness repositoryBusiness, IStatisticsBusinessCount statisticsBusinessCount)
        {
            this.repositoryBusiness = repositoryBusiness;
            this.statisticsBusinessCount = statisticsBusinessCount;
        }
        public IList<Business> GetAll()
        {
            return repositoryBusiness.Find(x => true);
        }
        public Business GetOne(Guid id)
        {
            return repositoryBusiness.FindById(id);
        }
        public void Update(Business business)
        {
            repositoryBusiness.Update(business);
        }
        public void Delete(Business business)
        {
            repositoryBusiness.Delete(business);
        }
        [UnitOfWork]
        public void Disable(Guid  businessId)
        {
            Business b = GetOne(businessId);
            b.Enabled = false;
            b.EnabledTime = DateTime.Now;
            b.EnabledMemo = "封停或删除";
            repositoryBusiness.Update(b);
        }
       [UnitOfWork] 
       public  ActionResult<Business> Add(string name, string phone,string email, Guid ownerId, string latitude, string longtitude
           , string rawAddressFromMapApi, string contact, int workingYears, int staffAmount)

        {
          
            ActionResult<Business> result = new ActionResult<Business>();
            try
            {
                Business business = new Business(name, phone, email, ownerId, latitude, longtitude
                    , rawAddressFromMapApi, contact, workingYears, staffAmount);
                repositoryBusiness.Add(business);
                result.ResultObject = business;
              
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMsg = ex.Message;
               

            }
            
            return result;

        }
        [UnitOfWork]
        public  ActionResult<Business> ChangeInfo(string businessId,string ownerId, string name, string description, string phone,
            string address, string avatarImageName)
        {
            ActionResult<Business> result = new ActionResult<Business>();
            Business business = repositoryBusiness.GetBusinessByIdAndOwner(new Guid(businessId),new Guid(ownerId));
            if (business == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "不存在此店铺,或者您不是该店铺的所有者";
                return result;
            }
            
            business.ChangeInfo(name, description, phone, address, avatarImageName);
            result.ResultObject = business;
            return result;
        }


        public IList<Area> GetAreasOfBusiness()
        {
            return repositoryBusiness.GetDistinctAreasOfBusiness();
        }
        public IList<Business> GetBusinessInSameCity(Area area)
        {
            return repositoryBusiness.GetBusinessInSameCity(area);
        }

        public Business GetBusinessByPhone(string phone)
        {
            return repositoryBusiness.FindOne(x => x.Phone == phone);
        }
        [UnitOfWork]
        public Business GetBusinessByEmail(string email)
        {
            return repositoryBusiness.FindOne(x => x.Email == email);
        }

        public int GetEnableSum(string memberId)
        {
            // return DALBusiness.GetEnableSum(member);
            // x.Owner == member).And(x => x.Enabled == true)
            Expression<Func< Business, bool>> sameOwner = i => i.OwnerId.ToString() == memberId;
            Expression<Func< Business, bool>> isEnabled = i => i.Enabled;

            var where = PredicateBuilder.And(sameOwner, isEnabled);
            int result = (int)repositoryBusiness.GetRowCount(where);
            return result;
        }

        /// <summary>
        /// 解析传递过来的 string, 
        /// </summary>
        public IList<Business> GetBusinessListByOwner(Guid memberId)
        {
            return repositoryBusiness.Find(x => x.OwnerId == memberId);
            //     return DALBusiness.GetBusinessListByOwner(memberId);
        }
        //如果图片保存不是通过编辑 Business 对象来完成的(比如 通过ajax mediaserver)



        public Business GetBusinessByIdAndOwner(Guid id, Guid ownerId)
        {
            return repositoryBusiness.FindOne(x => x.Id == id && x.OwnerId == ownerId);
            //  return DALBusiness.GetBusinessByIdAndOwner(id, ownerId);
        }
        public IList<Business> GetListByPage(int pageIndex, int pageSize, out long totalRecord)
        {
            return repositoryBusiness.GetListByPage( pageIndex, pageSize, out totalRecord);
            return repositoryBusiness.Find(x => true, pageIndex, pageSize, out totalRecord);
        }

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="strName"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public IList<Business> GetStores(TraitFilter filter, string strName, string UserID)
        {
            var where = PredicateBuilder.True<Business>();
            where = where.And(x => x.Enabled);
            if (!string.IsNullOrEmpty(UserID))
            {
                where = where.And(x => x.OwnerId == new Guid(UserID));
            }
            if (!string.IsNullOrEmpty(strName))
            {
                where = where.And(x => x.Name.Contains(strName));
            }
            Business baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repositoryBusiness.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? repositoryBusiness.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : repositoryBusiness.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }

        /// <summary>
        /// 统计店铺数量
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public long GetStoresCount(string alias, Guid ownerId)
        {
            var where = PredicateBuilder.True<Business>();
            where = where.And(x => x.Enabled);
            if (ownerId != Guid.Empty)
            {
                where = where.And(x => x.OwnerId == ownerId);
            }
            if (!string.IsNullOrEmpty(alias))
            {
                where = where.And(x => x.Name.Contains(alias));
            }
            long count = repositoryBusiness.GetRowCount(where);
            return count;
        }

        public void ApprovedEnable(Guid businessId)
        {
            Business b = GetOne(businessId);
            b.IsApplyApproved = true;
        }

        public void ApprovedDisable(Guid businessId)
        {
            Business b = GetOne(businessId);
            b.IsApplyApproved = false;
        }


        /// <summary>
        /// 昨日新增店铺
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public long GetCountOfNewBusinessesYesterdayByArea(IList<string> areaList)
        {
            DateTime beginTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            return repositoryBusiness.GetBusinessesCountByArea(areaList, beginTime, beginTime.AddDays(1));
        }

        /// <summary>
        /// 当前店铺总量
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public long GetCountOfAllBusinessesByArea(IList<string> areaList)
        {
            return repositoryBusiness.GetBusinessesCountByArea(areaList, DateTime.MinValue, DateTime.MinValue);
        }

        /// <summary>
        /// 当前代理区域的所有店铺列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public IList<Business> GetAllBusinessesByArea(IList<string> areaList)
        {
            return repositoryBusiness.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue);
        }

        /// <summary>
        /// 当前代理区域的所有店铺列表区分是否可用
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public IList<Business> GetAllBusinessesByArea(IList<string> areaList,bool enable)
        {
            return repositoryBusiness.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue).Where(x=>x.Enabled==enable).ToList();
        }

        /// <summary>
        /// 统计店铺每日或每时新增数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsNewBusinessesCountListByTime(IList<string> areaList, DateTime beginTime, DateTime endTime)
        {
            IList<Business> businessList = repositoryBusiness.GetBusinessesByArea(areaList, beginTime, endTime);
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsNewBusinessesCountListByTime(businessList, beginTime, endTime, beginTime.ToString("yyyyMMdd") == endTime.AddDays(-1).ToString("yyyyMMdd"));
            return statisticsInfo;
        }
        /// <summary>
        /// 统计店铺每日或每时累计数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllBusinessesCountListByTime(IList<string> areaList, DateTime beginTime, DateTime endTime)
        {
            IList<Business> businessList = repositoryBusiness.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue);
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsAllBusinessesCountListByTime(businessList, beginTime, endTime, beginTime.ToString("yyyyMMdd") == endTime.AddDays(-1).ToString("yyyyMMdd"));
            return statisticsInfo;
        }

        /// <summary>
        /// 根据店铺年限统计店铺数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllBusinessesCountListByLife(IList<string> areaList)
        {
            IList<Business> businessList = repositoryBusiness.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue);
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsAllBusinessesCountListByLife(businessList);
            return statisticsInfo;
        }

        /// <summary>
        /// 根据店铺员工数量统计店铺数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllBusinessesCountListByStaff(IList<string> areaList)
        {
            IList<Business> businessList = repositoryBusiness.GetBusinessesByArea(areaList, DateTime.MinValue, DateTime.MinValue);
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsAllBusinessesCountListByStaff(businessList);
            return statisticsInfo;
        }

        /// <summary>
        /// 根据店铺所在子区域统计店铺数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllBusinessesCountListByArea(IList<Area> areaList)
        {
            IList<string> AreaIdList = areaList.Select(x => x.Id.ToString()).ToList();
            IList<Business> businessList = repositoryBusiness.GetBusinessesByArea(AreaIdList, DateTime.MinValue, DateTime.MinValue);
            StatisticsInfo statisticsInfo = statisticsBusinessCount.StatisticsAllBusinessesCountGroupByArea(businessList, areaList);
            return statisticsInfo;
        }

        /// <summary>
        /// 计算上个月同比
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public string GetStatisticsRatioYearOnYear(IList<string> areaList)
        {
            DateTime dtBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            long businessCountLastMonth = repositoryBusiness.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-1), dtBase);
            long businessCountBeforeLastMonth = repositoryBusiness.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-1).AddYears(-1), dtBase.AddYears(-1));
            return Ydb.Common.MathHelper.GetCalculatedRatio(businessCountLastMonth, businessCountBeforeLastMonth);
        }

        /// <summary>
        /// 计算上个月环比
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public string GetStatisticsRatioMonthOnMonth(IList<string> areaList)
        {
            DateTime dtBase = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            long businessCountLastMonth = repositoryBusiness.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-1), dtBase);
            long businessCountBeforeLastMonth = repositoryBusiness.GetBusinessesCountByArea(areaList, dtBase.AddMonths(-2), dtBase.AddMonths(-1));
            return Ydb.Common.MathHelper.GetCalculatedRatio(businessCountLastMonth, businessCountBeforeLastMonth);
        }

    }
}
