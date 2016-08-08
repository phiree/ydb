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
        /// 查询快照
        /// </summary>
        /// <param name="ServiceID"></param>
        /// <param name="filter"></param>
        /// <param name="sna"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        IList<snapshortsObj> GetSnapshots(string ServiceID, common_Trait_Filtering filter, common_Trait_SnapshotFiltering sna, Customer customer);


    }
}
