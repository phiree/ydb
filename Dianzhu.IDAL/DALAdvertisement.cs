using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.DAL
{
    public interface IDALAdvertisement  
    {

          IList<Advertisement> GetADList(int pageIndex, int pageSize, out int totalRecord);

          IList<Advertisement> GetADListForUseful();

          Advertisement GetByUid(Guid uid);
    }
}
