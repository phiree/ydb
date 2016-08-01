using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Snapshot
{
    public interface ISnapshotService
    {
        /// <summary>
        /// 查询订单合集
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderfilter"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IList<snapshortsObj> GetSnapshots(string ServiceID, common_Trait_Filtering filter, common_Trait_SnapshotFiltering sna, common_Trait_Headers headers);


    }
}
