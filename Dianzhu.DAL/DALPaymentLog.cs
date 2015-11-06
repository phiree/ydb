using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

namespace Dianzhu.DAL
{
    public class DALPaymentLog : DALBase<Model.PaymentLog>
    {
        
        public DALPaymentLog()
        {
           
        }
        //注入依赖,供测试使用;
        public DALPaymentLog(string fortest):base(fortest)
        {

        }

    }
}
