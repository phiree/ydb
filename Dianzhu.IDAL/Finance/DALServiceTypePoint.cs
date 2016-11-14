﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
using Dianzhu.Model;
namespace Dianzhu.IDAL.Finance
{
    public interface IDALServiceTypePoint:IRepository<Dianzhu.Model.Finance.ServiceTypePoint,Guid>
    {

          ServiceTypePoint GetOneByServiceType(string serviceTypeId);


          IList<Dianzhu.Model.Finance.ServiceTypePoint> GetAll();
        void SaveList(IList<ServiceTypePoint> list);
    }
}
