using NHibernate;
using NHibernate.Cfg;
using System;

namespace NHibernateUnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        Configuration Configuration { get; }
        ISessionFactory SessionFactory { get; }
     
        ISession CurrentSession { get; set; }

        IUnitOfWork Create();
        void DisposeUnitOfWork(UnitOfWorkImplementor adapter);
    }
}