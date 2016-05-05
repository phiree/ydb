using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using PHSuit;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using log4net;
using NHibernate.Context;

namespace Dianzhu.DAL
{
    public class NHUnitOfWork : IDAL.IUnitOfWork
    {

        public static NHUnitOfWork Current
        {
            get { return _current; }
            set { _current = value; }
        }
        private static NHUnitOfWork _current;
        private readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        public ISession Session { get; private set; }


     



        public NHUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _current = this;


        }

        public void BeginTransaction()
        {

            Session = _sessionFactory.OpenSession();

            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                Session.Close();

            }
        }
        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            finally
            {
                Session.Close();
            }
        }
    }

}
