using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.Model;
using Dianzhu.DAL;
using Dianzhu.Model.Enums;
using System.Web;
using System.IO;
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
    }
}
