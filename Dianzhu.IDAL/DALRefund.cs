﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.IDAL
{
    public interface IDALRefund : IRepository<Refund,Guid>
    {
        Refund GetRefundByPlatformTradeNo(string platformTradeNo);
    }
}
