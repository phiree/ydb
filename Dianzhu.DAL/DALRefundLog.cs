using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

namespace Dianzhu.DAL
{
    public class DALRefundLog : DALBase<Model.RefundLog>
    {
        
        public DALRefundLog()
        {
           
        }
        //注入依赖,供测试使用;
        public DALRefundLog(string fortest):base(fortest)
        {

        }

    }
}
