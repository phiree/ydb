using System;
using System.Data;

namespace NHibernateUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Flush();
        bool IsInActiveTransaction { get; }

        void Refresh(object obj);
        IGenericTransaction BeginTransaction();
        IGenericTransaction BeginTransaction(IsolationLevel isolationLevel);
        void TransactionalFlush();
        void TransactionalFlush(IsolationLevel isolationLevel);
    }
}