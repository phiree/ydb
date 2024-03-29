﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
 
namespace Dianzhu.IDAL
{
    public interface IDALServiceOrder:IDAL.IRepository<ServiceOrder,Guid>
    {
        IList<ServiceOrder> GetOrderListOfServiceByDateRange(Guid serviceId, DateTime timeBegin, DateTime dateEnd);
        IList<ServiceOrder> GetAll(int pageIndex, int pageSize, out long totalRecords);


    }
}
