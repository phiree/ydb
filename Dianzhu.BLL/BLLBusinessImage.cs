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
       
       public DALBusinessImage DALBusinessImage=DALFactory.DALBusinessImage;
       
       public void Delete(Guid biId)
       {
           BusinessImage bi = DALBusinessImage.GetOne(biId);

           DALBusinessImage.Delete(bi);
       }
    }
}
