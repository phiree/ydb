using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate.Criterion;

namespace Dianzhu.DAL
{
    public class DALPayment : DALBase<Model.Payment>
    {
         public DALPayment()
        {
             
        }
        //注入依赖,供测试使用;
         public DALPayment(string fortest):base(fortest)
        {
            
        }

        /// <summary>
        /// 获取一个订单已经
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public IList<Payment> GetPaymentsForOrder(ServiceOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
