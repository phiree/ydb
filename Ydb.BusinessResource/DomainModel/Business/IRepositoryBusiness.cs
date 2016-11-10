using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
namespace Ydb.BusinessResource.DomainModel
{
   public  interface IRepositoryBusiness:IRepository<Business,Guid>
    {
        IList<Area> GetDistinctAreasOfBusiness();
        IList<Business> GetBusinessInSameCity(Area area);
        Business GetBusinessByPhone(string phone);
        Business GetBusinessByEmail(string email);
        Business GetBusinessByIdAndOwner(Guid Id, Guid ownerId);
        void SaveList(IList<Business> businesses);
        /// <summary>
        /// 全部已经启用的商铺
        /// </summary>
        /// <param name="ownerId"></param>
        IList<Business> GetBusinessListByOwner(Guid ownerId);

        /// <summary>
        /// 根据页码和页数查询商家列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        IList<Business> GetListByPage(int pageIndex, int pageSize, out long totalRecord);

        int GetEnableSum(string memberId);
    }
}
