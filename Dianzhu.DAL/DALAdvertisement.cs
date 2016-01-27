using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALAdvertisement : DALBase<Advertisement>
    {
         public DALAdvertisement()
        {
             
        }
        //注入依赖,供测试使用;
         public DALAdvertisement(string fortest):base(fortest)
        {
            
        }
        
        public IList <Advertisement> GetADList(int pageIndex, int pageSize, out int totalRecord)
        {
            IQueryOver<Advertisement, Advertisement> iquery = Session.QueryOver<Advertisement>();
            totalRecord = iquery.ToRowCountQuery().FutureValue<int>().Value;
            return iquery.OrderBy(x => x.Num).Asc.Skip((pageIndex-1) * pageSize).Take(pageSize).List();
        }

        public Advertisement GetByUid(Guid uid)
        {
            return Session.QueryOver<Advertisement>().Where(x => x.Id == uid).SingleOrDefault();
        }
    }
}
