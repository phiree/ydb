using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate.Criterion;

namespace Dianzhu.DAL
{
    public class DALRefund : DALBase<Refund>
    {
         public DALRefund()
        {
             
        }
        //注入依赖,供测试使用;
         public DALRefund(string fortest):base(fortest)
        {
            
        }


    }
}
