using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Staff
{
    public class StaffService: IStaffService
    {
        BLL.BLLBusiness bllBusiness ;
        BLL.BLLStaff bllStaff;
        public StaffService(BLL.BLLBusiness bllBusiness, BLL.BLLStaff bllStaff)
        {
            this.bllBusiness = bllBusiness;
            this.bllStaff = bllStaff;
        }

        /// <summary>
        /// 新建员工 店铺的员工
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffobj"></param>
        /// <returns></returns>
        public staffObj PostStaff(string storeID, staffObj staffobj)
        {
            Guid guidUser = new Guid();
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(utils.CheckGuidID(storeID, "storeID"), guidUser);
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            Model.Staff staff = Mapper.Map<staffObj, Model.Staff>(staffobj);
            staff.Enable = true;
            staff.IsAssigned = false;
            staff.Photo = utils.DownloadToMediaserver(staff.Photo, string.Empty, "StaffAvatar", "image");
            bllStaff.Save(staff);
            staff = bllStaff.GetOne(staff.Id);
            if (staff != null)
            {
                staffobj = Mapper.Map<Model.Staff, staffObj>(staff);
            }
            else
            {
                throw new Exception("新建失败");
            }
            return staffobj;
        }

        /// <summary>
        /// 条件读取员工
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="filter"></param>
        /// <param name="stafffilter"></param>
        /// <returns></returns>
        public IList<staffObj> GetStaffs(string storeID, common_Trait_Filtering filter, common_Trait_StaffFiltering stafffilter)
        {
            IList<Model.Staff> staff = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "Staff");
            staff = bllStaff.GetStaffs(filter1, stafffilter.alias, stafffilter.email, stafffilter.phone, stafffilter.sex, stafffilter.specialty, stafffilter.realName, utils.CheckGuidID(storeID, "storeID"));
            if (staff == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<staffObj> staffobj = Mapper.Map<IList<Model.Staff>, IList<staffObj>>(staff);
            return staffobj;
        }

        /// <summary>
        /// 统计服务员工的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="stafffilter"></param>
        /// <returns></returns>
        public countObj GetStaffsCount(string storeID, common_Trait_StaffFiltering stafffilter)
        {
            countObj c = new countObj();
            c.count = bllStaff.GetStaffsCount(stafffilter.alias, stafffilter.email, stafffilter.phone, stafffilter.sex, stafffilter.specialty, stafffilter.realName, utils.CheckGuidID(storeID, "storeID")).ToString();
            return c;
        }

        /// <summary>
        /// 读取员工 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        public staffObj GetStaff(string storeID, string staffID)
        {
            Model.Staff staff = null;
            staff = bllStaff.GetStaff(utils.CheckGuidID(storeID, "storeID"), utils.CheckGuidID(staffID, "staffID"));
            staffObj staffobj = Mapper.Map<Model.Staff, staffObj>(staff);
            return staffobj;
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <param name="staffobj"></param>
        /// <returns></returns>
        public staffObj PatchStaff(string storeID, string staffID, staffObj staffobj)
        {
            Guid guidUser = new Guid();
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Guid guidStaff = utils.CheckGuidID(staffID, "staffID");
            Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, guidUser);
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            Model.Staff staff = bllStaff.GetStaff(guidStore, guidStaff);
            if (staff == null)
            {
                throw new Exception("该员工不在职！");
            }

            Model.Staff staff1 = new Model.Staff();
            staff.CopyTo(staff1);
            Model.Staff staff2 = Mapper.Map<staffObj, Model.Staff>(staffobj);
            if (staff2.NickName != null && staff2.NickName != staff1.NickName)
            {
                staff1.NickName = staff2.NickName;
            }
            if (staff2.Email != null && staff2.Email != staff1.Email)
            {
                staff1.Email = staff2.Email;
            }
            if (staff2.Phone != null && staff2.Phone != staff1.Phone)
            {
                staff1.Phone = staff2.Phone;
            }
            if (staff2.Photo != null && staff2.Photo != staff1.Photo)
            {
                //staff1.Photo = staff2.Photo;
                staff1.Photo = utils.DownloadToMediaserver(staff2.Photo, string.Empty, "StaffAvatar", "image");
            }
            if (staff2.Name != null && staff2.Name != staff1.Name)
            {
                staff1.Name = staff2.Name;
            }
            if (staff2.Code != null && staff2.Code != staff1.Code)
            {
                staff1.Code = staff2.Code;
            }
            bllStaff.Update(staff1);
            staff2 = bllStaff.GetOne(staff1.Id);

            
            if (staff2 != null )
            {
                staffobj = Mapper.Map<Model.Staff, staffObj>(staff2);
            }
            else
            {
                throw new Exception("更新失败");
            }
            return staffobj;
        }

        /// <summary>
        /// 删除员工 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        public object DeleteStaff(string storeID, string staffID)
        {
            Model.Staff staff = null;
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Guid guidStaff = utils.CheckGuidID(staffID, "staffID");
            staff = bllStaff.GetStaff(guidStore, guidStaff);
            if (staff == null)
            {
                throw new Exception("该员工不在职！");
            }
            bllStaff.Delete(staff);
            staff = bllStaff.GetStaff(guidStore, guidStaff);
            if (staff == null)
            {
                return "删除成功！";
            }
            else
            {
                throw new Exception("删除失败！");
            }
        }
    }
}
