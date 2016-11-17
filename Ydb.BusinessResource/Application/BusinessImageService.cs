using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.IO;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
using Ydb.Common;

namespace Ydb.BusinessResource.Application
{
    public class  BusinessImageService : IBusinessImageService
    {

        IRepositoryBusinessImage repositoryBusinessImage;
       IRepositoryBusiness repositoryBusiness;
        public BusinessImageService(IRepositoryBusinessImage repositoryBusinessImage,
        IRepositoryBusiness iRepositoryBusiness)
        {
            this.repositoryBusinessImage = repositoryBusinessImage;
            this.repositoryBusiness = iRepositoryBusiness;
        }
        public void Delete(Guid biId)
        {
            BusinessImage bi = repositoryBusinessImage.FindById(biId);
            if (bi == null) { return; }
            
            string filePath = HttpContext.Current.Server.MapPath(Dianzhu.Config.Config.GetAppSetting("business_image_root") + "/original/") + bi.ImageName;

            if (filePath != null)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                repositoryBusinessImage.Delete(bi);
            }

        }
        
        public string Save(Guid businessId, System.Web.HttpPostedFileBase imageFile,  enum_ImageType imageType)
        {
            Business b = repositoryBusiness.FindById(businessId);
            string savedPath = string.Empty;
            string imageName = string.Empty;
            if (imageFile != null && imageFile.ContentLength != 0)
            {
                imageName = businessId + imageType.ToString() + Guid.NewGuid().GetHashCode() + Path.GetExtension(imageFile.FileName);
                savedPath = HttpContext.Current.Server.MapPath(Dianzhu.Config.Config.GetAppSetting("business_image_root") + "/original/") + imageName;
                imageFile.SaveAs(savedPath);
                BusinessImage biImage = new BusinessImage
                {
                    ImageType = imageType,
                    UploadTime = DateTime.Now,
                    ImageName = imageName,
                    Size = imageFile.ContentLength
                };
                b.BusinessImages.Add(biImage);
            }
            repositoryBusiness.SaveOrUpdate(b);
            return "/media/business/original/" + imageName;
        }

        public BusinessImage FindBusImageByName(string imgName)
        {
            return repositoryBusinessImage.FindBusImageByName(imgName);
        }

        public bool DeleteBusImageByName(string imgName)
        {
            BusinessImage bi = repositoryBusinessImage.FindBusImageByName(imgName);
            if (bi != null)
            {
                string filePath = HttpContext.Current.Server.MapPath(bi.GetRelativePathByType()) + bi.ImageName;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                repositoryBusinessImage.Delete(bi);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
