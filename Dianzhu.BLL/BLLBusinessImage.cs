using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
   public class BLLBusinessImage
    {
       IDALBusinessImage iDalBusinessImage;
       public BLLBusinessImage(IDALBusinessImage iDalBusinessImage)
       {
           this.iDalBusinessImage = iDalBusinessImage;
       }
       public BLLBusinessImage():this(new DALBusinessImage())
       {
       }
       public void Delete(Guid biId)
       { 
           BusinessImage bi=iDalBusinessImage.DalBase.GetOne(biId);

           iDalBusinessImage.DalBase.Delete(bi);
       }
    }
}
