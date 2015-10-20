using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
   public class DALServiceProperty:DALBase<ServiceProperty>
    {
     
        
       
        public DALServiceProperty()
        {
            
        }
        //注入依赖,供测试使用;
        public DALServiceProperty(string fortest):base(fortest)
        {
            
        }
 

       public IList<ServiceProperty> GetList(Guid serviceTypeId)
       {
         return   GetList("select p from ServiceProperty p where p.ServiceType.id='"+serviceTypeId+"'");
       }
       public void   Save(ServiceProperty property)
       {
            Save(property);
       }
     
    }
}
