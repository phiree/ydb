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
        

        public IList<ServiceType> GetTopList()
        {
            string query = "select s from ServiceType s where s.Parent is null order by OrderNumber";
            return GetList(query);
        }
         
        

         
    }
}
