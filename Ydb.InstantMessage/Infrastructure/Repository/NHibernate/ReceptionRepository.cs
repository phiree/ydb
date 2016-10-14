using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using NHibernate;
using Ydb.InstantMessage.DomainModel;
using Ydb.InstantMessage.Infrastructure.Repository.NHibernate;
namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate
{
    public class ReceptionRepository : IRepositoryReception
    {

        NHRepositoryBase<ReceptionStatus,Guid> baseRepository;
        ISession session;
        public ReceptionRepository(ISession session)
        {
            this.session = session;
            baseRepository = new NHRepositoryBase<ReceptionStatus, Guid>(session);
        }
        public IList<ReceptionStatus> FindByCustomerId(string customerServiceId)
        {
            return baseRepository.Find(x => x.CustomerId == customerServiceId);
        }
        public IList<ReceptionStatus> FindByCustomerServiceId(string csId)
        {
            return baseRepository.Find(x => x.CustomerServiceId == csId);
        }
        public IList<ReceptionStatus> FindByDiandian(string diandianId,int amount)
        {
            return baseRepository.Find(x => x.CustomerServiceId == diandianId).Take(amount).ToList();
        }
        public ReceptionRepository(ISessionFactory sessionFactory)
        {

        }
        public void Add(ReceptionStatus t)
        {
            session.Save(t);
        }

        public void Add(ReceptionStatus t, Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(ReceptionStatus t)
        {
            session.Delete(t);
        }

        public IList<ReceptionStatus> Find(Expression<Func<ReceptionStatus, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IList<ReceptionStatus> Find(Expression<Func<ReceptionStatus, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            throw new NotImplementedException();
        }

        public IList<ReceptionStatus> Find(Expression<Func<ReceptionStatus, bool>> where, string sortBy, bool ascending, int offset, ReceptionStatus baseone)
        {
            throw new NotImplementedException();
        }

        public IList<ReceptionStatus> Find(Expression<Func<ReceptionStatus, bool>> where, int pageIndex, int pageSize, out long totalRecords, string sortBy, bool ascending, int offset, ReceptionStatus baseone)
        {
            throw new NotImplementedException();
        }

        public ReceptionStatus FindByBaseId(Guid strBaseID)
        {
            throw new NotImplementedException();
        }

        public ReceptionStatus FindById(Guid identityId)
        {
            throw new NotImplementedException();
        }

        public ReceptionStatus FindOne(Expression<Func<ReceptionStatus, bool>> where)
        {
            throw new NotImplementedException();
        }

        public long GetRowCount(Expression<Func<ReceptionStatus, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(ReceptionStatus t)
        {
            throw new NotImplementedException();
        }

        public void Update(ReceptionStatus t)
        {
            t.LastUpdateTime = DateTime.Now;
            session.Update(t);
        }
    }
}
