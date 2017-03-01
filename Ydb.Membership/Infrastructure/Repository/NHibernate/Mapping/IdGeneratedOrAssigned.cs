using System;
using NHibernate.Engine;
using NHibernate.Id;
using Ydb.Common.Domain;
using NH = NHibernate;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping
{
    internal class IdGeneratedOrAssigned : IIdentifierGenerator
    {
        public object Generate(ISessionImplementor session, object obj)
        {
            var entity = (IEntity<Guid>)obj;
            if (Guid.Empty == entity.Id)
                return Guid.NewGuid();
            return entity.Id;
            // throw new NotImplementedException();
       }
    }
}