using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Staff
{
    public class StaffService : IStaffService
    {
        BLL.BLLBusiness bllBusiness;
        BLL.BLLStaff bllStaff;
        BLL.DZMembershipProvider DZM;
       public static BLL.BLLOrderAssignment bllAssignment;
        public StaffService(BLL.BLLBusiness bllBusiness, BLL.BLLStaff bllStaff, BLL.DZMembershipProvider DZM, BLL.BLLOrderAssignment bllAssignment)
        {
            this.bllBusiness = bllBusiness;
            this.bllStaff = bllStaff;
            this.DZM = DZM;
            StaffService.bllAssignment = bllAssignment;
        }

        /// <summary>
        /// 对象处理
        /// </summary>
        /// <param name="servicesobj"></param>
        /// <param name="dzservice"></param>
        public static void changeObj(staffObj staffobj, Model.Staff staff)
        {
            staffobj.storeData.storeID = staff.Belongto.Id.ToString();
            IList<Model.OrderAssignment> listAssignment = bllAssignment.GetOAListByStaff(staff);
            staffobj.storeData.allCount = listAssignment.Count;
            for (int i = 0; i < listAssignment.Count; i++)
            {
                staffobj.storeData.assignOrderIDs.Add(listAssignment[i].Order.Id.ToString());
                if (listAssignment[i].Order.OrderStatus != Model.Enums.enum_OrderStatus.Finished && listAssignment[i].Order.OrderStatus != Model.Enums.enum_OrderStatus.Appraised)
                {
                    staffobj.storeData.handleCount++;
                }
                else
                {
                    staffobj.storeData.finishCount++;
                }
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="customer"></param>
        Model.Business checkRute(string storeID, Customer customer)
        {
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("storeID不能为空！");
            }
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Guid guidCustomer = utils.CheckGuidID(customer.UserID, "customer.UserID");
            if (customer.UserType == "business")
            {
                Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, guidCustomer);
                if (business == null)
                {
                    throw new Exception("该店铺不存在！");
                }
                return business;
            }
            else
            {
                Model.Staff staff = bllStaff.GetOneByUserID(guidStore, customer.UserID);
                if (staff == null)
                {
                    throw new Exception("你不是该店铺的员工！");
                }
                return staff.Belongto;
            }
        }

        /// <summary>
        /// 新建员工 店铺的员工
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public staffObj PostStaff(string storeID, staffObj staffobj, Customer customer)
        {
            if (staffobj.loginName == "" || staffobj.pWord == "")
            {
                throw new Exception("登录的用户名或密码不能为空！");
            }
            Model.Business business = checkRute(storeID, customer);
            Model.Staff staff = Mapper.Map<staffObj, Model.Staff>(staffobj);
            System.Web.Security.MembershipCreateStatus mc = new System.Web.Security.MembershipCreateStatus();
            Model.DZMembership dzms = DZM.CreateUser(staffobj.loginName, null, null, staffobj.pWord, out mc, Model.Enums.enum_UserType.staff);
            //staffobj.phone == "" ? null : staffobj.phone, staffobj.email == "" ? null : staffobj.email--防止出现重复异常
            staff.Enable = true;
            staff.UserID = dzms.Id.ToString();
            staff.Belongto = business;
            staff.IsAssigned = false;
            //staff.Photo = utils.DownloadToMediaserver(staff.Photo, string.Empty, "StaffAvatar", "image");
            //上传图片都是先调用上传接口，然后将其结果传给该接口就好了，该接口不用上传。
            bllStaff.Save(staff);
            var avatarList = staff.StaffAvatar.Where(x => x.IsCurrent == true).ToList();
            avatarList.ForEach(x => x.IsCurrent = false);
            Model.BusinessImage biImage = new Model.BusinessImage
            {
                ImageType = Model.Enums.enum_ImageType.Staff_Avatar,
                UploadTime = DateTime.Now,
                ImageName = staff.Phone,
                Size = 0,
                IsCurrent = true
            };
            staff.StaffAvatar.Add(biImage);
            //staff = bllStaff.GetOne(staff.Id);
            //if (staff != null)
            //{
            staffobj = Mapper.Map<Model.Staff, staffObj>(staff);
            changeObj(staffobj, staff);
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
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<staffObj> GetStaffs(string storeID, common_Trait_Filtering filter, common_Trait_StaffFiltering stafffilter, Customer customer)
        {
            Model.Business business = checkRute(storeID, customer);
            IList<Model.Staff> staff = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "Staff");
            staff = bllStaff.GetStaffs(filter1, stafffilter.alias, stafffilter.email, stafffilter.phone, stafffilter.sex, stafffilter.specialty, stafffilter.realName, business.Id);
            if (staff == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<staffObj>();
            }
            IList<staffObj> staffobj = Mapper.Map<IList<Model.Staff>, IList<staffObj>>(staff);
            for (int i = 0; i < staffobj.Count; i++)
            {
                changeObj(staffobj[i], staff[i]);
            }
            return staffobj;
        }

        /// <summary>
        /// 统计服务员工的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="stafffilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public countObj GetStaffsCount(string storeID, common_Trait_StaffFiltering stafffilter, Customer customer)
        {
            Model.Business business = checkRute(storeID, customer);
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
                //throw new Exception(Dicts.StateCode[4]);
                return null;
            }
            staffObj staffobj = Mapper.Map<Model.Staff, staffObj>(staff);
            changeObj(staffobj, staff);
            return staffobj;
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <param name="staffobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public staffObj PatchStaff(string storeID, string staffID, staffObj staffobj, Customer customer)
        {
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("storeID不能为空！");
            }
            if (string.IsNullOrEmpty(staffID))
            {
                throw new FormatException("staffID不能为空！");
            }
            Guid guidStore = utils.CheckGuidID(storeID, "storeID");
            Guid guidCustomer = utils.CheckGuidID(customer.UserID, "customer.UserID");
            Model.Staff staff = null;

            staff = bllStaff.GetStaff(guidStore, utils.CheckGuidID(staffID, "staffID"));
            if (staff == null)
            {
                throw new Exception("该店铺不存在该员工！");
            }
            if (customer.UserType == "business")
            {
                Model.Business business = bllBusiness.GetBusinessByIdAndOwner(guidStore, guidCustomer);
                if (business == null)
                {
                    throw new Exception("该店铺不存在！");
                }
            }
            else
            {
                if (staff.UserID.ToString() != customer.UserID)
                {
                    throw new Exception("员工只能修改自己的信息！");
                }
            }
            if (string.IsNullOrEmpty(staffobj.alias) == false && staffobj.alias != staff.NickName)
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
                staff.Photo = utils.GetFileName(staffobj.imgUrl);
                var avatarList = staff.StaffAvatar.Where(x => x.IsCurrent == true).ToList();
                avatarList.ForEach(x => x.IsCurrent = false);
                Model.BusinessImage biImage = new Model.BusinessImage
                {
                    ImageType = Model.Enums.enum_ImageType.Staff_Avatar,
                    UploadTime = DateTime.Now,
                    ImageName = staff.Phone,
                    Size = 0,
                    IsCurrent = true
                };
                staff.StaffAvatar.Add(biImage);
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
            changeObj(staffobj, staff);
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
        /// <param name="customer"></param>
        /// <returns></returns>
        public object DeleteStaff(string storeID, string staffID, Customer customer)
        {
            if (string.IsNullOrEmpty(staffID))
            {
                throw new FormatException("staffID不能为空！");
            }
            Model.Business business = checkRute(storeID, customer);
            Model.Staff staff = null;
            Guid guidStaff = utils.CheckGuidID(staffID, "staffID");
            staff = bllStaff.GetStaff(business.Id, guidStaff);
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
            return new string[] { "删除成功！" };
            //}
            //else
            //{
            //    throw new Exception("删除失败！");
            //}
        }
    }
}
