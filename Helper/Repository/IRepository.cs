using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers.Domain;
using Helpers.Specification;

namespace Helpers.Repository
{
    public interface IRepository<TEntity> 
        where TEntity : IDomainEntity
    {
        TEntity FindById(Guid id);
        TEntity FindOne(ISpecification<TEntity> spec);
        IEnumerable<TEntity> Find(ISpecification<TEntity> spec);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
