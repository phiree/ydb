using System;
using System.Data;
using log4net;
namespace NHibernateUnitOfWork
{
    public static partial class With
    {
        static ILog  log = log4net.LogManager.GetLogger("Dianzhu.NhibernateUnitofWork.With");
        public static void Transaction(IsolationLevel level, Action transactional)
        {
            using (UnitOfWork.Start())
            {
                // If we are already in a transaction, don't start a new one
                if (UnitOfWork.Current.IsInActiveTransaction)
                {
                    transactional();
                }
                else
                {
                    IGenericTransaction tx = UnitOfWork.Current.BeginTransaction(level);
                    try
                    {
                        transactional();
                        tx.Commit();
                    }
                    catch(Exception ex)
                    {
                        tx.Rollback();
                        PHSuit.ExceptionLoger.ExceptionLog(log, ex);
                        throw;
                    }
                    finally
                    {
                        tx.Dispose();
                    }
                }
            }
        }

        public static void Transaction(Action transactional)
        {
            Transaction(IsolationLevel.ReadCommitted, transactional);
        }
    }
}