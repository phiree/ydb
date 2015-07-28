using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.DAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System.Web;
using System.IO;

namespace Dianzhu.BLL
{
   public class BLLBusinessImage
    {
       
       public DALBusinessImage DALBusinessImage=DALFactory.DALBusinessImage;
       public BLLBusiness BLLBusiness = new BLLBusiness();

       
       public void Delete(Guid biId)
       {
           BusinessImage bi = DALBusinessImage.GetOne(biId);

           DALBusinessImage.Delete(bi);
       }
       public string  Save(Guid businessId,System.Web.HttpPostedFile imageFile, Dianzhu.Model.Enums.enum_ImageType imageType)
       {
           Business b = BLLBusiness.GetOne(businessId);
           string savedPath = string.Empty;
           if (imageFile != null && imageFile.ContentLength != 0)
           {
               string imageName = businessId + imageType.ToString() + Guid.NewGuid().GetHashCode() + Path.GetExtension(imageFile.FileName);
               savedPath = HttpContext.Current.Server.MapPath(SiteConfig.BusinessImagePath + "/original/") + imageName;
               imageFile.SaveAs(savedPath);
               BusinessImage biImage = new BusinessImage
               {
                   
                   ImageType= imageType,
                   UploadTime = DateTime.Now,
                   ImageName = imageName,
                   Size = imageFile.ContentLength
               };
               switch (imageType)
               {
                   case enum_ImageType.Business_Avatar:
                       b.BusinessAvatar = biImage;
                       break;
                   case enum_ImageType.Business_ChargePersonIdCard:
                       b.ChargePersonIdCard = biImage;
                       break;
                   case enum_ImageType.Business_Licence:
                       b.BusinessLicence = biImage;
                       break;
                   case enum_ImageType.Business_Show:
                       
                       b.BusinessImages.Add(biImage);
                       break;
                   default: break;
               }
                

           }
           BLLBusiness.Updte(b);
           return savedPath;
       }
    }
}
