using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALServiceType :DALBase<ServiceType>
    {
         public DALServiceType()
        {
            Session = new HybridSessionBuilder().GetSession();
        }
        //注入依赖,供测试使用;
         public DALServiceType(string fortest)
        {
            
        }

        public IList<ServiceType> GetTopList()
        {
            string query = "select s from ServiceType s where s.Parent is null order by OrderNumber";
            return GetList(query);
        }
        public ServiceType GetOneByCode(string code)
        {
            ServiceType s = Session.QueryOver<ServiceType>().Where(x => x.Code == code).SingleOrDefault();
            return s;
        }
        
         
        

         
    }
}
