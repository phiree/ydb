using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALDZTag : NHRepositoryBase<DZTag,Guid>,IDAL.IDALDZTag
    {       
        public IList<DZTag> GetTagsForService(Guid serviceId)
        {
            return Find(x => x.ForPK == serviceId.ToString());
        }
        public IList<DZTag> GetTagsForBusiness(Guid businessId)
        {
            return Find(x => x.ForPK3 == businessId.ToString());            
        }
        public IList<DZTag> GetTagsForBusinessAndTypeId(Guid businessId, Guid typeId)
        {
            return Find(x => x.ForPK3 == businessId.ToString() && x.ForPK2 == typeId.ToString());
        }
    }
}
