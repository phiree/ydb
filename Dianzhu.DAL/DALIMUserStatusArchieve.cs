using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALIMUserStatusArchieve : DALBase<IMUserStatusArchieve>
    {
         public DALIMUserStatusArchieve()
        {
             
        }
        //注入依赖,供测试使用;
         public DALIMUserStatusArchieve(string fortest):base(fortest)
        {
            
        }

    }
}
