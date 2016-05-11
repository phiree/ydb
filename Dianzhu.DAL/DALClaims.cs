using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALClaims : DALBase<Claims>
    {
         public DALClaims()
        {
             
        }
        //注入依赖,供测试使用;
         public DALClaims(string fortest):base(fortest)
        {
            
        }
        
        
    }
}
