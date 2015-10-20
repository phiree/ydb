using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
   public class DALServicePropertyValue:DALBase<ServicePropertyValue>
    {
     
        
        public DALServicePropertyValue()
        {
             
        }
        //注入依赖,供测试使用;
        public DALServicePropertyValue(string fortest):base(fortest)
        {
            
        }
      
     
    }
}
