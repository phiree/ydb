using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
using Ydb.Order.DomainModel;
using Ydb.Order.Infrastructure;

namespace Ydb.Order.Infrasturcture
{
    

        public class RefundFactory
        {
            public static IRefundApi CreateRefund(enum_PayAPI payApi)
            {
                switch (payApi)
                {
                    case enum_PayAPI.Alipay:
                    return new RefundAli();
                        break;
                    case enum_PayAPI.Wechat: break;
                }
            }

        }
    
