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
  public  class BLLStaff
    {
      public DALStaff DALStaff=DALFactory.DALStaff;

      
      public IList<Staff> GetList(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecords)
      {

          return DALStaff.GetList(businessId, serviceTypeId, pageindex, pagesize, out totalRecords);
      }
      
      public void SaveOrUpdate(Staff staff)
      {
          DALStaff.SaveOrUpdate(staff);
      }
      public void Delete(Staff staff)
      {
          DALStaff.Delete(staff);
      }
      public Staff GetOne(Guid id)
      {
          return DALStaff.GetOne(id);
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
                  IsCurrent=true
              };
              s.StaffAvatar.Add(biImage);
              //s.BusinessImages.Add(biImage);
          }
          SaveOrUpdate(s);
          return "/media/business/original/" + imageName;
      }
       
    }
}
