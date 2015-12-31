using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALIMUserStatus : DALBase<IMUserStatus>
    {
         public DALIMUserStatus()
        {
             
        }
        //注入依赖,供测试使用;
         public DALIMUserStatus(string fortest):base(fortest)
        {
            
        }

    }
}
