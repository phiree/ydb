﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
using NHibernate;
using System.Linq.Expressions;
using Ydb.Common.Repository;
namespace Ydb.Order.Infrastructure.Repository.NHibernate
{
   public class RepositoryServiceOrderPushedService : NHRepositoryBase<ServiceOrderPushedService, Guid>, IRepositoryServiceOrderPushedService
    {
        public virtual IList<ServiceOrderPushedService> FindByOrder(ServiceOrder order)
        {
            return Find(x => x.ServiceOrder.Id == order.Id);
        }
    }
}
