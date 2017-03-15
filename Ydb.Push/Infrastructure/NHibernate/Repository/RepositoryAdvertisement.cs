using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
 
using NHibernate;
using Ydb.Push.DomainModel;
using Ydb.Push.DomainModel.IRepository;
namespace Ydb.Push.Infrastructure.NHibernate.Repository
{
    /// <summary>
    /// nhibernate implenmenting
    /// </summary>
    public class RepositoryAdvertisement:NHRepositoryBase<Advertisement,Guid>,IRepositoryAdvertisement
    {
        
    }
}
