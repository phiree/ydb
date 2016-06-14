using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Complaint
{
    public class ComplaintService: IComplaintService
    {
        BLL.BLLComplaint bllcomplaint;
        public ComplaintService(BLL.BLLComplaint bllcomplaint)
        {
            this.bllcomplaint = bllcomplaint;
        }

        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="complaintobj"></param>
        public complaintObj AddComplaint(complaintObj complaintobj)
        {
            Model.Complaint complaint = Mapper.Map<complaintObj, Model.Complaint>(complaintobj);
            bllcomplaint.AddComplaint(complaint);
            complaintobj.status = complaint.Status;
            return complaintobj;
        }

        /// <summary>
        /// 条件读取投诉
        /// </summary>
        /// <returns>area实体list</returns>
        public IList<complaintObj> GetComplaints(common_Trait_Filtering filter, common_Trait_ComplainFiltering complaint)
        {
            IList<Model.Complaint> listcomplaint = null;

            int intsize = 0;
            int intnum = 0;
            if (filter.pageSize != null && filter.pageNum != null)
            {
                try
                {
                    intsize = int.Parse(filter.pageSize);
                    intnum = int.Parse(filter.pageNum);
                    if (intsize <= 0 || intnum < 1)
                    {
                        throw new Exception("分页参数pageSize,pageNum错误！");
                    }
                }
                catch
                {
                    throw new Exception("分页参数pageSize,pageNum错误！");
                }
            }
            listcomplaint = bllcomplaint.GetComplaints(intsize, intnum,complaint.orderID,complaint.storeID,complaint.customerServiceID);
            if (listcomplaint == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<complaintObj> complaintobj = Mapper.Map<IList<Model.Complaint>, IList<complaintObj>>(listcomplaint);
            return complaintobj;

        }
    }
}
