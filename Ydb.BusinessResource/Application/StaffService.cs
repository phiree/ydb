using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Web;
using System.IO;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
using Ydb.Common.Application;
using Ydb.BusinessResource.Infrastructure;
using Ydb.Common;

namespace Ydb.BusinessResource.Application
{
    public class StaffService : IStaffService
    {

         IRepositoryStaff  repositoryStaff;



        public StaffService(IRepositoryStaff repositoryStaff)
        {
            this.repositoryStaff = repositoryStaff;
        }
        
        
        [UnitOfWork]
        public void Delete(Guid staffId)
        {
          Staff  s= repositoryStaff.FindById(staffId);
            repositoryStaff.Delete(s);
        }
        public Staff GetOne(Guid id)
        {
            return repositoryStaff.FindById(id);
        }

        public void Save(Staff staff)
        {
            repositoryStaff.Add(staff);
        }

        public void Update(Staff staff)
        {
            repositoryStaff.Update(staff);
        }

        public string Save(Guid StaffId, System.Web.HttpPostedFile imageFile,  enum_ImageType imageType)
        {
            Staff s = GetOne(StaffId);
            string savedPath = string.Empty;
            string imageName = string.Empty;
            if (imageFile != null && imageFile.ContentLength != 0)
            {
                imageName = StaffId + imageType.ToString() + Guid.NewGuid().GetHashCode() + Path.GetExtension(imageFile.FileName);
                savedPath = HttpContext.Current.Server.MapPath(Dianzhu.Config.Config.GetAppSetting("business_image_root") + "/original/") + imageName;
                imageFile.SaveAs(savedPath);
                var avatarList = s.StaffAvatar.Where(x => x.IsCurrent == true).ToList();
                avatarList.ForEach(x => x.IsCurrent = false);
                BusinessImage biImage = new BusinessImage
                {
                    ImageType = imageType,
                    UploadTime = DateTime.Now,
                    ImageName = imageName,
                    Size = imageFile.ContentLength,
                    IsCurrent = true
                };
                s.StaffAvatar.Add(biImage);
                //s.BusinessImages.Add(biImage);
            }
            Update(s);
            return "/media/business/original/" + imageName;
        }

        public int GetEnableSum(Business business)
        {
            return repositoryStaff.GetEnableSum(business);
        }

        public IList<Staff> GetAllListByBusiness(Business business)
        {
            return repositoryStaff.GetAllListByBusiness(business);
        }

        public IList<Staff> GetListByBusiness(Guid businessId, int pageIndex, int pageSize, out int totalRecord)
        {
            long longTotalRecord;
            var list = repositoryStaff.Find(x => x.Belongto.Id == businessId, pageIndex, pageSize, out longTotalRecord);
            totalRecord = (int)longTotalRecord;
            return list;
        }

        /// <summary>
        /// 条件读取员工
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="alias"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="sex"></param>
        /// <param name="specialty"></param>
        /// <param name="realName"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public IList<Staff> GetStaffs(TraitFilter filter, string alias, string email, string phone, string sex, string specialty, string realName, Guid storeID)
        {
            var where = PredicateBuilder.True<Staff>();
            //where = where.And(x => x.Enable);
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Belongto.Id == storeID);
            }
            if (alias != null && alias != "")
            {
                where = where.And(x => x.NickName == alias);
            }
            if (email != null && email != "")
            {
                where = where.And(x => x.Email == email);
            }
            if (phone != null && phone != "")
            {
                where = where.And(x => x.Phone == phone);
            }
            if (sex != null && sex != "")
            {
                where = where.And(x => x.Gender == sex);
            }
            if (specialty != null && specialty != "")
            {
            }
            if (realName != null && realName != "")
            {
                where = where.And(x => x.Name == realName);
            }
            Staff baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repositoryStaff.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? repositoryStaff.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : repositoryStaff.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }

        /// <summary>
        /// 统计服务员工的数量
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="sex"></param>
        /// <param name="specialty"></param>
        /// <param name="realName"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public long GetStaffsCount(string alias, string email, string phone, string sex, string specialty, string realName, Guid storeID)
        {
            var where = PredicateBuilder.True<Staff>();
            //where = where.And(x => x.Enable);
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Belongto.Id == storeID);
            }
            if (alias != null && alias != "")
            {
                where = where.And(x => x.NickName == alias);
            }
            if (email != null && email != "")
            {
                where = where.And(x => x.Email == email);
            }
            if (phone != null && phone != "")
            {
                where = where.And(x => x.Phone == phone);
            }
            if (sex != null && sex != "")
            {
                where = where.And(x => x.Gender == sex);
            }
            if (specialty != null && specialty != "")
            {
            }
            if (realName != null && realName != "")
            {
                where = where.And(x => x.Name == realName);
            }
            long count = repositoryStaff.GetRowCount(where);
            return count;
        }

        /// <summary>
        /// 读取员工 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        public Staff GetStaff(Guid storeID, Guid staffID)
        {
            var where = PredicateBuilder.True<Staff>();
            //where = where.And(x => x.Enable);
            where = where.And(x => x.Id == staffID);
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Belongto.Id == storeID);
            }
            Staff staff = repositoryStaff.FindOne(where);
            return staff;
        }

        /// <summary>
        /// 根据用户ID获取员工信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Staff GetOneByUserID(Guid storeID, string userID)
        {
            var where = PredicateBuilder.True<Staff>();
            where = where.And(x => x.UserID == userID);
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Belongto.Id == storeID);
            }
            return repositoryStaff.FindOne(where);
        }
        [UnitOfWork]
        public void CanelAssign(string staffId)
        {
            Staff assigningStaff = repositoryStaff.FindById(new Guid(staffId));
            assigningStaff.IsAssigned = false;
        }
    }
}
