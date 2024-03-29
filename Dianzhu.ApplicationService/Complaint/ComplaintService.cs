﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ydb.Common.Specification;
using Ydb.Order.Application;
using m=Ydb.Order.DomainModel;



namespace Dianzhu.ApplicationService.Complaint
{
    public class ComplaintService: IComplaintService
    {
        Ydb.Order.Application.IComplaintService complaintService;
        IServiceOrderService  ibllServiceOrder;
        public ComplaintService(Ydb.Order.Application.IComplaintService complaintService, IServiceOrderService ibllServiceOrder)
        {
            this.complaintService = complaintService;
            this.ibllServiceOrder = ibllServiceOrder;
        }

        /// <summary>
        /// 新建投诉
        /// </summary>
        /// <param name="complaintobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public complaintObj AddComplaint(complaintObj complaintobj,Customer customer)
        {
            //if (string.IsNullOrEmpty(complaintobj.senderID))
            //{
            //    throw new FormatException("发送者 ID不能为空！");
            //}
            complaintobj.senderID = customer.UserID;
            if (string.IsNullOrEmpty(complaintobj.orderID))
            {
                throw new FormatException("投诉的订单ID不能为空！");
            }
            if (complaintobj.target!= "customerService" && complaintobj.target!= "store")
            {
                throw new FormatException("投诉对象只能是客服和店铺！");
            }
            //if(complaintobj.target == "customerService")
            //{
            //    complaintobj.target = "cer";
            //}

            Ydb.Common.enum_ComplaintTarget target;
            if (!Enum.TryParse<Ydb.Common.enum_ComplaintTarget>(complaintobj.target, out target))
            {
                throw new FormatException("投诉对象格式不正确！");
            }
            if (string.IsNullOrEmpty(complaintobj.content))
            {
                throw new FormatException("投诉的描述不能为空！");
            }
            if (complaintobj.senderID != customer.UserID)
            {
                throw new Exception("不能帮别人投诉！");
            }
            m.ServiceOrder order = ibllServiceOrder.GetOne(utils.CheckGuidID(complaintobj.orderID, "complaintobj.orderID"));
            if (order == null)
            {
                throw new Exception("投诉的订单不存在！");
            }
            if (order.CustomerId != complaintobj.senderID)
            {
                throw new Exception("不能投诉别人的订单！");
            }
            m.Complaint complaint = Mapper.Map<complaintObj, m.Complaint>(complaintobj);
            if (complaint.Target == Ydb.Common.enum_ComplaintTarget.store)
            {
                complaint.BusinessId = order.BusinessId;
                complaint.CustomerServiceId = "";
            }
            else
            {
                complaint.BusinessId = "";
                complaint.CustomerServiceId = order.CustomerServiceId;
            }
            //complaint.Target= (enum_ComplaintTarget)Enum.Parse(typeof(enum_ComplaintTarget), complaintobj.target); 
            for (int i = 0; i < complaintobj.resourcesUrls.Count; i++)
            {
                complaint.ComplaitResourcesUrl.Add(utils.GetFileName(complaintobj.resourcesUrls[i], "MediaGetUrl"));
            }
            
            //Guid g = new Guid();
            //bool b = g == complaint.Id;
            complaintService.AddComplaint(complaint);
            //b = g == complaint.Id;
            // if(complaint.Id==new Guid())
            //complaintobj.status = complaint.Status;
            //complaint = bllcomplaint.GetComplaintById(complaint.Id);
            //if (complaint == null)
            //{
            //    throw new Exception("没有获取到新增的数据，可能新增失败！");
            //}
            complaintobj = Mapper.Map<m.Complaint,complaintObj > (complaint);
            for (int i = 0; i < complaintobj.resourcesUrls.Count; i++)
            {
                complaintobj.resourcesUrls[i] = complaintobj.resourcesUrls[i] != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + complaintobj.resourcesUrls[i] : "";
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
            IList<m.Complaint> listcomplaint = null;
            TraitFilter filter1 = utils.CheckFilter(filter, "Complaint");
            listcomplaint = complaintService.GetComplaints(filter1, utils.CheckGuidID(complaint.orderID, "orderID"), utils.CheckGuidID(complaint.storeID, "storeID"), utils.CheckGuidID(complaint.customerServiceID, "customerServiceID"));
            if (listcomplaint == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<complaintObj>();
            }
            IList<complaintObj> complaintobj = Mapper.Map<IList<m.Complaint>, IList<complaintObj>>(listcomplaint);
            for (int i = 0; i < complaintobj.Count; i++)
            {
                for (int j = 0; j < complaintobj[i].resourcesUrls.Count; j++)
                {
                    complaintobj[i].resourcesUrls[j] = complaintobj[i].resourcesUrls[j] != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + complaintobj[i].resourcesUrls[j] : "";
                }
            }
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
            c.count = complaintService.GetComplaintsCount(complaint.orderID, complaint.storeID, complaint.customerServiceID).ToString();
            return c; 

        }

        /// <summary>
        /// 读取投诉信息
        /// </summary>
        /// <param name="complaintID"></param>
        /// <returns></returns>
        public complaintObj GetOneComplaint(string complaintID)
        {
            m.Complaint complaint = complaintService.GetComplaintById(utils.CheckGuidID(complaintID, "complaintID"));
            if (complaint == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                //return null;
                throw new Exception("没有找到资源！");
            }
            complaintObj complaintobj = Mapper.Map<m.Complaint, complaintObj>(complaint);
            for (int i = 0; i < complaint.ComplaitResourcesUrl.Count; i++)
            {
                complaintobj.resourcesUrls[i] = complaintobj.resourcesUrls[i] != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + complaintobj.resourcesUrls[i] : "";
            }
            return complaintobj;
        }
    }
}
