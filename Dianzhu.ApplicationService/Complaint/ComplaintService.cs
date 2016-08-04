﻿using System;
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
        /// <param name="customer"></param>
        /// <returns></returns>
        public complaintObj AddComplaint(complaintObj complaintobj,Customer customer)
        {
            if (string.IsNullOrEmpty(complaintobj.senderID))
            {
                throw new FormatException("发送者 ID不能为空！");
            }
            if (string.IsNullOrEmpty(complaintobj.orderID))
            {
                throw new FormatException("投诉的订单ID不能为空！");
            }
            if (complaintobj.target.ToString ()!="cer" && complaintobj.target.ToString ()!="store")
            {
                throw new FormatException("投诉对象只能是客户(cer)和店铺(store)！");
            }
            if (string.IsNullOrEmpty(complaintobj.content))
            {
                throw new FormatException("投诉的描述不能为空！");
            }
            if (complaintobj.senderID != customer.UserID)
            {
                throw new Exception("不能帮别人投诉！");
            }
            Model.Complaint complaint = Mapper.Map<complaintObj, Model.Complaint>(complaintobj);
            for (int i = 0; i < complaintobj.resourcesUrl.Count; i++)
            {
                complaint.ComplaitResourcesUrl.Add(utils.GetFileName(complaintobj.resourcesUrl[i]));
            }
            //Guid g = new Guid();
            //bool b = g == complaint.Id;
            bllcomplaint.AddComplaint(complaint);
            //b = g == complaint.Id;
            // if(complaint.Id==new Guid())
            //complaintobj.status = complaint.Status;
            //complaint = bllcomplaint.GetComplaintById(complaint.Id);
            //if (complaint == null)
            //{
            //    throw new Exception("没有获取到新增的数据，可能新增失败！");
            //}
            complaintobj = Mapper.Map<Model.Complaint,complaintObj > (complaint);
            for (int i = 0; i < complaintobj.resourcesUrl.Count; i++)
            {
                complaintobj.resourcesUrl[i] = complaintobj.resourcesUrl[i] != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + complaintobj.resourcesUrl[i] : "";
            }
            return complaintobj;
        }

        /// <summary>
        /// 条件读取投诉
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="complaint"></param>
        /// <returns></returns>
        public IList<complaintObj> GetComplaints(common_Trait_Filtering filter, common_Trait_ComplainFiltering complaint)
        {
            IList<Model.Complaint> listcomplaint = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "Complaint");
            listcomplaint = bllcomplaint.GetComplaints(filter1, utils.CheckGuidID(complaint.orderID, "orderID"), utils.CheckGuidID(complaint.storeID, "storeID"), utils.CheckGuidID(complaint.customerServiceID, "customerServiceID"));
            if (listcomplaint == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<complaintObj> complaintobj = Mapper.Map<IList<Model.Complaint>, IList<complaintObj>>(listcomplaint);
            return complaintobj;

        }

        /// <summary>
        /// 统计投诉的数量
        /// </summary>
        /// <param name="complaint"></param>
        /// <returns></returns>
        public countObj GetComplaintsCount(common_Trait_ComplainFiltering complaint)
        {
            countObj c = new countObj();
            c.count = bllcomplaint.GetComplaintsCount(utils.CheckGuidID(complaint.orderID, "orderID"), utils.CheckGuidID(complaint.storeID, "storeID"), utils.CheckGuidID(complaint.customerServiceID, "customerServiceID")).ToString();
            return c; 

        }
    }
}
