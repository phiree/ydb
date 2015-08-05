using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALDZTag : DALBase<Model.DZTag>
    {
         
        public IList<DZTag> GetTagsForService(Guid serviceId)
        {
            return Session.QueryOver<DZTag>().Where(x => x.ForPK == serviceId.ToString()).List();
        }
        public IList<DZTag> GetTagsForBusiness(Guid businessId)
        {
            return Session.QueryOver<DZTag>().Where(x => x.ForPK3 == businessId.ToString()).List();
        }
        public IList<DZTag> GetTagsForBusinessAndTypeId(Guid businessId, Guid typeId)
        {
            return Session.QueryOver<DZTag>().Where(x => x.ForPK3 == businessId.ToString()).And(x=>x.ForPK2==typeId.ToString()).List();
        }
    }
}
