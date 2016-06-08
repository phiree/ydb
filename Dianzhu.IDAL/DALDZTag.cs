using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.IDAL
{
    public interface IDALDZTag :IRepository<DZTag,Guid>
    {

          IList<DZTag> GetTagsForService(Guid serviceId);
          IList<DZTag> GetTagsForBusiness(Guid businessId);
          IList<DZTag> GetTagsForBusinessAndTypeId(Guid businessId, Guid typeId);
    }
}
