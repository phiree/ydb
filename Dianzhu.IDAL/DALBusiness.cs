using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;

namespace Dianzhu.IDAL
{
    public interface IDALBusiness
    {



        IList<Area> GetAreasOfBusiness();
        IList<Business> GetBusinessInSameCity(Area area);
        Business GetBusinessByPhone(string phone);
        Business GetBusinessByEmail(string email);
        Business GetBusinessByIdAndOwner(Guid Id, Guid ownerId);
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

        int GetEnableSum(DZMembership member);
    }
}
