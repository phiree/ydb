using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.DomainModel;
using Helpers.Specification;
using LinqSpecs;
namespace Finance.Infrastructure
{
    public class SalePointNHRepository : Helpers.Repository.IRepository<DomainModel.SalePoint>
    {
        NHibernate.ISession session;
        public SalePointNHRepository()
        {
            this.session = new Finance.Infrastructure.Nhibernate.HybridSessionBuilder().GetSession();
        }
        public void Add(SalePoint entity)
        {
            session.SaveOrUpdate(entity);
        }

        public IEnumerable<SalePoint> Find(ISpecification<SalePoint> spec)
        {
            return session.QueryOver<SalePoint>().Where(spec.SpecExpression).List();
        }

        public SalePoint FindById(Guid id)
        {
            return session.Get<SalePoint>(id);
        }

        public SalePoint FindOne(ISpecification<SalePoint> spec)
        {
         return   session.QueryOver<SalePoint>().Where(spec.SpecExpression).SingleOrDefault();
        }

        public void Remove(SalePoint entity)
        {
            session.Delete(entity);
        }
    }
}
