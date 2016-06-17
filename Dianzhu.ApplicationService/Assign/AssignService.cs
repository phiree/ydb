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
        /// <returns>area实体list</returns>
        public assignObj PostAssign(assignObj assignobj)
        {
            Model.OrderAssignment oa= Mapper.Map<assignObj, Model.OrderAssignment>(assignobj);
            Model.ServiceOrder order=ibllorder.GetOne(utils.CheckGuidID(assignobj.orderID, "orderID"));
            Model.Staff staff = bllstaff.GetOne(utils.CheckGuidID(assignobj.staffID, "staffID"));
            if (order == null)
            {
                throw new Exception("指派的订单不存在！");
            }
            if (staff == null)
            {
                throw new Exception("指派的员工不存在！");
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
            DateTime dt = oa.CreateTime;
            oa.AssignedTime = dt;
            oa.Order = order;
            oa.AssignedStaff = staff;

            bllassign.Save(oa);
            bllstaff.SaveOrUpdate(staff);
            oa = bllassign.GetAssignById(oa.Id);
            if (oa != null && oa.CreateTime == dt)
            {
                assignobj = Mapper.Map<Model.OrderAssignment,assignObj>(oa);
                return assignobj;
            }
            else
            {
                throw new Exception("新建失败");
            }
        }

        /// <summary>
        /// 条件读取指派
        /// </summary>
        /// <returns>area实体list</returns>
        public IList<assignObj> GetAssigns(common_Trait_Filtering filter, common_Trait_AssignFiltering assign)
        {
            IList<Model.OrderAssignment> listassign = null;
            int[] page = utils.CheckFilter(filter);
            listassign = bllassign.GetAssigns(page[0], page[1], utils.CheckGuidID(assign.staffID, "customerServiceID"), utils.CheckGuidID(assign.orderID, "orderID"), utils.CheckGuidID(assign.storeID, "storeID"));
            if (listassign == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<assignObj> complaintobj = Mapper.Map<IList<Model.OrderAssignment>, IList<assignObj>>(listassign);
            return complaintobj;
        }

        /// <summary>
        /// 统计指派的数量
        /// </summary>
        /// <returns>area实体list</returns>
        public countObj GetAssignsCount(common_Trait_AssignFiltering assign)
        {
            countObj c = new countObj();
            c.count = bllassign.GetAssignsCount(utils.CheckGuidID(assign.staffID, "customerServiceID"), utils.CheckGuidID(assign.orderID, "orderID"), utils.CheckGuidID(assign.storeID, "storeID")).ToString();
            return c;
        }
    }
}
