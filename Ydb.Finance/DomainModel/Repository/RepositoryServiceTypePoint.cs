using System;
using System.Text;
using System.Collections;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;
using System.Collections.Generic;

namespace Ydb.Finance.DomainModel
{
    internal interface IRepositoryServiceTypePoint : IRepository< ServiceTypePoint,Guid>
    {

          ServiceTypePoint GetOneByServiceType(string serviceTypeId);
          IList<ServiceTypePoint> GetAll();
        void SaveList(IList<ServiceTypePoint> list);
    }
}
