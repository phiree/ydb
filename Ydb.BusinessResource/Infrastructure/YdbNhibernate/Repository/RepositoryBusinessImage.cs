using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Repository;
namespace Ydb.BusinessResource.Infrastructure.YdbNHibernate.Repository
{
    public class RepositoryBusinessImage : NHRepositoryBase<BusinessImage, Guid>, IRepositoryBusinessImage
    {
        public BusinessImage FindBusImageByName(string imgName)
        {
            return FindOne(x => x.ImageName == imgName);
        }
    }
}
