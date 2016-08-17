using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Assign
{
    public class AssignService:IAssignService
    {
        BLL.BLLOrderAssignment bllassign;
        BLL.BLLStaff bllstaff;
        BLL.IBLLServiceOrder ibllorder;
        public AssignService(BLL.BLLOrderAssignment bllassign, BLL.BLLStaff bllstaff, BLL.IBLLServiceOrder ibllorder)
        {
            this.bllassign = bllassign;
            this.bllstaff = bllstaff;
            this.ibllorder = ibllorder;
        }

        /// <summary>
        /// 新建指派
        /// </summary>
        /// <param name="assignobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public assignObj PostAssign(assignObj assignobj,Customer customer)
        {
            if (string.IsNullOrEmpty(assignobj.orderID))
            {
                throw new FormatException("指派的订单号不能为空！");
            }
            if (string.IsNullOrEmpty(assignobj.staffID))
            {
                throw new FormatException("指派的员工ID不能为空！");
            }
            Model.OrderAssignment oa= Mapper.Map<assignObj, Model.OrderAssignment>(assignobj);
            Model.ServiceOrder order=ibllorder.GetOneOrder(utils.CheckGuidID(assignobj.orderID, "assignobj.orderID"),utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (order == null)
            {
                throw new Exception("该商户指派的订单不存在！");
            }
            if (order.Staff != null)
            {
                throw new Exception("该订单已经指派！");
            }
            Model.Staff staff = bllstaff.GetStaff(order.Business.Id,utils.CheckGuidID(assignobj.staffID, "assignobj.staffID"));
            if (staff == null)
            {
                throw new Exception("在指派订单所属的店铺中不存在该指派的员工！");
            }
            if (!staff.Enable)
            {
                throw new Exception("指派的员工不在职！");
            }
            if (staff.IsAssigned)
            {
                throw new Exception("指派的员工已经被指派过！");
            }
            staff.IsAssigned = true;
            oa.Enabled = true;
            DateTime dt = DateTime.Now ;
            oa.CreateTime = dt;
            oa.AssignedTime = dt;
            oa.Order = order;
            oa.AssignedStaff = staff;
            order.Staff = staff;
            //oa.Order.Details[0].Staff.Clear();
            //oa.Order.Details[0].Staff.Add(staff);
            bllassign.Save(oa);
            //bllstaff.Update(staff);
            //oa = bllassign.GetAssignById(oa.Id);
            //if (oa != null && oa.CreateTime == dt)
            //{
            assignobj = Mapper.Map<Model.OrderAssignment, assignObj>(oa);
            return assignobj;
            //}
            //else
            //{
            //    throw new Exception("新建失败");
            //}
        }

        /// <summary>
        /// 条件读取指派
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="assign"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<assignObj> GetAssigns(common_Trait_Filtering filter, common_Trait_AssignFiltering assign,Customer customer)
        {
            IList<Model.OrderAssignment> listassign = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "OrderAssignment");
            listassign = bllassign.GetAssigns(filter1, utils.CheckGuidID(assign.staffID, "assign.staffID"), utils.CheckGuidID(assign.orderID, "assign.orderID"), utils.CheckGuidID(assign.storeID, "assign.storeID"), utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (listassign == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<assignObj>();
            }
            IList<assignObj> complaintobj = Mapper.Map<IList<Model.OrderAssignment>, IList<assignObj>>(listassign);
            return complaintobj;
        }

        /// <summary>
        /// 统计指派的数量
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public countObj GetAssignsCount(common_Trait_AssignFiltering assign, Customer customer)
        {
            countObj c = new countObj();
            c.count = bllassign.GetAssignsCount(utils.CheckGuidID(assign.staffID, "assign.staffID"), utils.CheckGuidID(assign.orderID, "assign.orderID"), utils.CheckGuidID(assign.storeID, "assign.storeID"), utils.CheckGuidID(customer.UserID, "customer.UserID")).ToString();
            return c;
        }

        /// <summary>
        /// 取消指派
        /// </summary>
        /// <param name="assignobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public object DeleteAssign(assignObj assignobj, Customer customer)
        {
            if (string.IsNullOrEmpty(assignobj.orderID))
            {
                throw new FormatException("取消指派的订单号不能为空！");
            }
            if (string.IsNullOrEmpty(assignobj.staffID))
            {
                throw new FormatException("取消指派的员工ID不能为空！");
            }
            Model.ServiceOrder order = ibllorder.GetOneOrder(utils.CheckGuidID(assignobj.orderID, "assignobj.orderID"), utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (order == null)
            {
                throw new Exception("该商户不存在该订单！");
            }
            if (order.Staff == null)
            {
                throw new Exception("该订单还没有被指派过！");
            }
            if (order.OrderStatus == Model.Enums.enum_OrderStatus.Finished || order.OrderStatus == Model.Enums.enum_OrderStatus.Appraised)
            {
                throw new Exception("该订单的服务已经完成，无法再取消指派！");
            }
            //Model.Staff staff = bllstaff.GetStaff(order.Business.Id, utils.CheckGuidID(assignobj.staffID, "assignobj.staffID"));
            //if (staff == null)
            //{
            //    throw new Exception("在指派订单所属的店铺中不存在该指派的员工！");
            //}
            //if (!staff.Enable)
            //{
            //    throw new Exception("指派的员工不在职！");
            //}
            if (order.Staff.Id.ToString () != assignobj.staffID)
            {
                throw new Exception("该订单指派的不是该员工！");
            }
            Model.OrderAssignment oa = bllassign.FindByOrderAndStaff(order, order.Staff);
            if (oa == null || oa.Enabled == false)
            {
                throw new Exception("该指派不存在或已取消！");
            }
            order.Staff.IsAssigned = false;
            oa.Enabled = false;
            DateTime dt = DateTime.Now;
            oa.DeAssignedTime = dt;
            order.Staff = null;
            //oa.Order.Details[0].Staff.Clear();
            //oa.Order.Details[0].Staff.Add(staff);
            //bllassign.Save(oa);
            //bllstaff.Update(staff);
            //oa = bllassign.GetAssignById(oa.Id);
            //if (oa != null && oa.CreateTime == dt)
            //{
            //assignobj = Mapper.Map<Model.OrderAssignment, assignObj>(oa);
            return "取消成功！";
            //}
            //else
            //{
            //    throw new Exception("新建失败");
            //}
        }
    }
}
