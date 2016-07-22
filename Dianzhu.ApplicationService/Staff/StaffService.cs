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
        BLL.DZMembershipProvider DZM;
        public StaffService(BLL.BLLBusiness bllBusiness, BLL.BLLStaff bllStaff, BLL.DZMembershipProvider DZM)
        {
            this.bllBusiness = bllBusiness;
            this.bllStaff = bllStaff;
            this.DZM = DZM;
        }

        /// <summary>
        /// 新建员工 店铺的员工
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffobj"></param>
        /// <returns></returns>
        public staffObj PostStaff(string storeID, staffObj staffobj)
        {
            //Guid guidUser = new Guid();
            //Model.Business business = bllBusiness.GetBusinessByIdAndOwner(utils.CheckGuidID(storeID, "storeID"), guidUser);
            if (staffobj.loginName == "" || staffobj.pWord == "")
            {
                throw new Exception("登录的用户名或密码不能为空！");
            }
            Model.Business business = bllBusiness.GetOne(utils.CheckGuidID(storeID, "storeID"));
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }

            Model.Staff staff = Mapper.Map<staffObj, Model.Staff>(staffobj);
            System.Web.Security.MembershipCreateStatus mc = new System.Web.Security.MembershipCreateStatus();
            Model.DZMembership dzms= DZM.CreateUser(staffobj.loginName, null,  null, staffobj.pWord, out mc, Model.Enums.enum_UserType.staff);
            //staffobj.phone == "" ? null : staffobj.phone, staffobj.email == "" ? null : staffobj.email--防止出现重复异常
            staff.Enable = true;
            staff.UserID = dzms.Id.ToString();
            staff.Belongto = business;
            staff.IsAssigned = false;
            //staff.Photo = utils.DownloadToMediaserver(staff.Photo, string.Empty, "StaffAvatar", "image");
            //上传图片都是先调用上传接口，然后将其结果传给该接口就好了，该接口不用上传。
            bllStaff.Save(staff);
            //staff = bllStaff.GetOne(staff.Id);
            //if (staff != null)
            //{
            staffobj = Mapper.Map<Model.Staff, staffObj>(staff);
            //}
            //else
            //{
            //    throw new Exception("新建失败");
            //}
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
            Model.Staff staff = bllStaff.GetStaff(utils.CheckGuidID(storeID, "storeID"), utils.CheckGuidID(staffID, "staffID"));
            if (staff == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
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
            //Guid guidUser = new Guid();
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Guid guidStaff = utils.CheckGuidID(staffID, "staffID");
            //Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, guidUser);
            Model.Business business = bllBusiness.GetOne(guidStore);
            if (business == null)
            {
                throw new Exception("该店铺不存在！");
            }
            Model.Staff staff = bllStaff.GetStaff(guidStore, guidStaff);
            if (staff == null)
            {
                throw new Exception("该员工不在职！");
            }

            //Model.Staff staff1 = new Model.Staff();
            //staff.CopyTo(staff1);
            //Model.Staff staff2 = Mapper.Map<staffObj, Model.Staff>(staffobj);
            if (string.IsNullOrEmpty(staffobj.alias)==false && staffobj.alias != staff.NickName)
            {
                staff.NickName = staffobj.alias;
            }
            if (string.IsNullOrEmpty(staffobj.email) == false && staffobj.email != staff.Email)
            {
                staff.Email = staffobj.email;
            }
            if (string.IsNullOrEmpty(staffobj.phone) == false && staffobj.phone != staff.Phone)
            {
                staff.Phone = staffobj.phone;
            }
            if (string.IsNullOrEmpty(staffobj.imgUrl) == false && staffobj.imgUrl != staff.Photo)
            {
                staff.Photo = staffobj.imgUrl;
                //staff1.Photo = utils.DownloadToMediaserver(staff2.Photo, string.Empty, "StaffAvatar", "image");
            }
            if (string.IsNullOrEmpty(staffobj.realName) == false && staffobj.realName != staff.Name)
            {
                staff.Name = staffobj.realName;
            }
            if (string.IsNullOrEmpty(staffobj.number) == false && staffobj.number != staff.Code)
            {
                staff.Code = staffobj.number;
            }
            //bllStaff.Update(staff1);
            //staff2 = bllStaff.GetOne(staff1.Id);

            
            //if (staff2 != null )
            //{
            staffobj = Mapper.Map<Model.Staff, staffObj>(staff);
            //}
            //else
            //{
            //    throw new Exception("更新失败");
            //}
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
            //staff.Enable = false;
            bllStaff.Delete(staff);
            //staff = bllStaff.GetStaff(guidStore, guidStaff);
            //if (staff == null)
            //{
            try
            {
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            }
            catch
            {
                throw new Exception("该员工已经被指派过服务，无法再删除！");
            }
            return "删除成功！";
            //}
            //else
            //{
            //    throw new Exception("删除失败！");
            //}
        }
    }
}
