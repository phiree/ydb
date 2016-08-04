using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Complaint
{
    public interface IComplaintService
    {
        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="complaintobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        complaintObj AddComplaint(complaintObj complaintobj, Customer customer);

        /// <summary>
        /// 条件读取投诉
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="complaint"></param>
        /// <returns></returns>
        IList<complaintObj> GetComplaints(common_Trait_Filtering filter, common_Trait_ComplainFiltering complaint);


        /// <summary>
        /// 统计投诉的数量
        /// </summary>
        /// <param name="complaint"></param>
        /// <returns></returns>
        countObj GetComplaintsCount( common_Trait_ComplainFiltering complaint);
    }
}
