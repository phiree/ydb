using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ydb.InstantMessage.DomainModel.Chat;
using NHibernate;
using Ydb.Common.Repository;
namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate
{
    public class RepositoryChat : IRepositoryChat
    {

        NHRepositoryBase<ReceptionChat,Guid> baseRepository;
        ISession session;
        public RepositoryChat(ISession session)
        {
            this.session = session;
            baseRepository = new NHRepositoryBase<ReceptionChat, Guid>(session);
        }
        
       
        public void Add(ReceptionChat t)
        {
            session.Save(t);
        }

        public void Add(ReceptionChat t, Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(ReceptionChat t)
        {
            session.Delete(t);
        }

        public IList<ReceptionChat> Find(Expression<Func<ReceptionChat, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IList<ReceptionChat> Find(Expression<Func<ReceptionChat, bool>> where, int pageIndex, int pageSize, out long totalRecords)
        {
            throw new NotImplementedException();
        }

        public IList<ReceptionChat> Find(Expression<Func<ReceptionChat, bool>> where, string sortBy, bool ascending, int offset, ReceptionChat baseone)
        {
            throw new NotImplementedException();
        }

        public IList<ReceptionChat> Find(Expression<Func<ReceptionChat, bool>> where, int pageIndex, int pageSize, out long totalRecords, string sortBy, bool ascending, int offset, ReceptionChat baseone)
        {
            throw new NotImplementedException();
        }

        public ReceptionChat FindByBaseId(Guid strBaseID)
        {
            throw new NotImplementedException();
        }

        public ReceptionChat FindById(Guid identityId)
        {
            throw new NotImplementedException();
        }

        public ReceptionChat FindOne(Expression<Func<ReceptionChat, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IList<ReceptionChat> GetListByCustomerId(string customerId)
        {
            return baseRepository.Find(x => x.FromId == customerId || x.ToId == customerId);
        }

        public long GetRowCount(Expression<Func<ReceptionChat, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(ReceptionChat t)
        {
            throw new NotImplementedException();
        }

        public void Update(ReceptionChat t)
        {
           
            session.Update(t);
        }
    }
}
