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

            using (var tr = Session.BeginTransaction())
            {

                var result = Session.QueryOver<DZTag>().Where(x => x.ForPK == serviceId.ToString()).List();
                tr.Commit();
                return result; 
               
            }

        }
        public IList<DZTag> GetTagsForBusiness(Guid businessId)
        {
            using (var tr = Session.BeginTransaction())
            {

                var result = Session.QueryOver<DZTag>().Where(x => x.ForPK3 == businessId.ToString()).List();
                tr.Commit();
                return result;

            }
            
        }
        public IList<DZTag> GetTagsForBusinessAndTypeId(Guid businessId, Guid typeId)
        {
            using (var tr = Session.BeginTransaction())
            {

                var result =    Session.QueryOver<DZTag>().Where(x => x.ForPK3 == businessId.ToString()).And(x => x.ForPK2 == typeId.ToString()).List();
                tr.Commit();
                return result;

            }
            
        }
    }
}
