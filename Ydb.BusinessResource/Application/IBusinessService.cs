using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
using Ydb.Common.Application;
using Ydb.Common.Domain;
namespace Ydb.BusinessResource.Application
{
   public interface  IBusinessService
    {

         IList<Business> GetAll();
         Business GetOne(Guid id);
         void Update(Business business);
         void Delete(Business business);
        ActionResult<Business> Add(string name, string phone,string email, Guid ownerId, string latitude, string longtitude
           , string rawAddressFromMapApi,string contact,int workingYears,int StaffAmount);
        ActionResult<Business> ChangeInfo(string businessId, string ownerId, string name, string description, string phone,
        string address, string avatarImageName);

         IList<Area> GetAreasOfBusiness();
         IList<Business> GetBusinessInSameCity(Area area);

         Business GetBusinessByPhone(string phone);
         Business GetBusinessByEmail(string email);

         int GetEnableSum(string memberId);

        /// <summary>
        /// 解析传递过来的 string, 
        /// </summary>
         IList<Business> GetBusinessListByOwner(Guid memberId);
        //如果图片保存不是通过编辑 Business 对象来完成的(比如 通过ajax mediaserver)



         Business GetBusinessByIdAndOwner(Guid id, Guid ownerId);
         IList<Business> GetListByPage(int pageIndex, int pageSize, out long totalRecord);

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="strName"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
         IList<Business> GetStores(TraitFilter filter, string strName, string UserID);

        /// <summary>
        /// 统计店铺数量
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
         long GetStoresCount(string alias, Guid ownerId);
          void Disable(Guid businessId);
        void ApprovedEnable(Guid businessId);
        void ApprovedDisable(Guid businessId);


        /// <summary>
        /// 昨日新增店铺
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        long GetCountOfNewBusinessesYesterdayByArea(IList<string> areaList);

        /// <summary>
        /// 当前店铺总量
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        long GetCountOfAllBusinessesByArea(IList<string> areaList);

        /// <summary>
        /// 当前代理区域的所有店铺列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        IList<Business> GetAllBusinessesByArea(IList<string> areaList);

        /// <summary>
        /// 统计店铺每日或每时新增数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsNewBusinessesCountListByTime(IList<string> areaList, DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 统计店铺每日或每时累计数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllBusinessesCountListByTime(IList<string> areaList, DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 根据店铺年限统计店铺数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllBusinessesCountListByLife(IList<string> areaList);

        /// <summary>
        /// 根据店铺员工数量统计店铺数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllBusinessesCountListByStaff(IList<string> areaList);

        /// <summary>
        /// 根据店铺所在子区域统计店铺数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllBusinessesCountListByArea(IList<Area> areaList);

        /// <summary>
        /// 计算上个月同比
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        string GetStatisticsRatioYearOnYear(IList<string> areaList);

        /// <summary>
        /// 计算上个月环比
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        string GetStatisticsRatioMonthOnMonth(IList<string> areaList);

    }
}
