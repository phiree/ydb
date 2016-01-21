using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;
using NHibernate.Transform;

namespace Dianzhu.DAL
{
    public class DALDZService : DALBase<DZService>
    {
        public DALDZService() { }
        [Obsolete("just for test ,don't use it outside testing code.")]
        public DALDZService(string fortest):base(fortest)
        {
        }
        public IList<DZService> GetList(Guid businessId,  int pageindex, int pagesize, out int totalRecord)
        {
            string where = "s.Business.Id='" + businessId + "'";
            return  GetList("select s from DZService s where "
                +where +" order by s.LastModifiedTime desc",
                pageindex, pagesize, out totalRecord);
        }
        public IList<DZService> GetOtherList(Guid businessId,Guid serviceId, int pageindex, int pagesize, out int totalRecord)
        {
            string where = "s.Business.Id='" + businessId + "' and s.Id!='"+serviceId+"'";
            return GetList("select s from DZService s where "
                + where + " order by s.LastModifiedTime desc",
                pageindex, pagesize, out totalRecord);
        }
        public IList<DZService> SearchService(string keywords, int pageindex, int pagesize, out int totalRecord)
        {
            var totalquery = Session.QueryOver<DZService>()
             // .Where(x => x.Name.Contains(keywords) || x.Description.Contains(keywords));
             .Where(Restrictions.On<DZService>(x => x.Name).IsLike(string.Format("%{0}%", keywords))
             || Restrictions.On<DZService>(x => x.Description).IsLike(string.Format("%{0}%", keywords))
             ); 
      
            totalRecord = totalquery.RowCount();

            var result = totalquery
                .Skip(pageindex * pagesize).Take(pagesize);

            return result.List();
        }
        
    }
}
