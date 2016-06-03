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
using System.Configuration;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using System.Data;

namespace Dianzhu.DAL
{
    public class NHUnitOfWork : IDAL.IUnitOfWork
    {


        ISession Session
        {
            get { return new HybridSessionBuilder().GetSession();  }
        }
        private ITransaction _transaction;
        public NHUnitOfWork( )
        {

           // Session =
            
           // _transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
        }
       
        

        

        public void BeginTransaction()
        {

          

            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch(Exception ex)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                Session.Dispose();

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
                Session.Dispose();
            }
        }

        
    }

}
