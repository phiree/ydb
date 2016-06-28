using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.Model;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.Model.Enums;
using System.Web;
using System.IO;
using DDDCommon;
namespace Dianzhu.BLL
{
    public class BLLStaff
    {

        public IDAL.IDALStaff dalStaff;



        public BLLStaff(IDAL.IDALStaff dalStaff)
        {
            this.dalStaff = dalStaff;
        }
        
        public void Delete(Staff staff)
        {
            dalStaff.Delete(staff);
        }
        public Staff GetOne(Guid id)
        {
            return dalStaff.FindById(id);
        }

        public void Save(Staff staff)
        {
            dalStaff.Add(staff);
        }

        public void Update(Staff staff)
        {
            dalStaff.Update(staff);
        }

        public string Save(Guid StaffId, System.Web.HttpPostedFile imageFile, Dianzhu.Model.Enums.enum_ImageType imageType)
        {
            Staff s = GetOne(StaffId);
            string savedPath = string.Empty;
            string imageName = string.Empty;
            if (imageFile != null && imageFile.ContentLength != 0)
            {
                imageName = StaffId + imageType.ToString() + Guid.NewGuid().GetHashCode() + Path.GetExtension(imageFile.FileName);
                savedPath = HttpContext.Current.Server.MapPath(SiteConfig.BusinessImagePath + "/original/") + imageName;
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
            return dalStaff.GetEnableSum(business);
        }

        public IList<Staff> GetAllListByBusiness(Business business)
        {
            return dalStaff.GetAllListByBusiness(business);
        }

        public IList<Staff> GetListByBusiness(Guid businessId, int pageIndex, int pageSize, out int totalRecord)
        {
            long longTotalRecord;
            var list = dalStaff.Find(x => x.Belongto.Id == businessId, pageIndex, pageSize, out longTotalRecord);
            totalRecord = (int)longTotalRecord;
            return list;
        }

        /// <summary>
        /// 条件读取员工
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pagenum"></param>
        /// <param name="alias"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="sex"></param>
        /// <param name="specialty"></param>
        /// <param name="realName"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public IList<Staff> GetStaffs(int pagesize, int pagenum, string alias, string email, string phone, string sex, string specialty, string realName, Guid storeID)
        {
            var where = PredicateBuilder.True<Staff>();
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
            long t = 0;
            var list = pagesize == 0 ? dalStaff.Find(where).ToList() : dalStaff.Find(where, pagenum, pagesize, out t).ToList();
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
            long count = dalStaff.GetRowCount(where);
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
            if (staffID != Guid.Empty)
            {
                where = where.And(x => x.Id == staffID);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Belongto.Id == storeID);
            }
            Staff staff = dalStaff.FindOne(where);
            return staff;
        }
    }
}
