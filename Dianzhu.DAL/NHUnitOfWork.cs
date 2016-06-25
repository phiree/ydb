using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
namespace Dianzhu.DAL
{
   public  class NHUnitOfWork : IUnitOfWork
    {
       
        public NHUnitOfWork()
        {
            // = new DAL_Hyber.HybridSessionBuilder().GetSession();
        }
        public  void BeginTransaction()
        {
            new DAL_Hyber.HybridSessionBuilder().GetSession().BeginTransaction(System.Data.IsolationLevel.Unspecified);
        }

        public void End()
        {
            DAL_Hyber.HybridSessionBuilder.ResetSession();
        }

        public void FlushTransaction()
        {
            var tr = new DAL_Hyber.HybridSessionBuilder().GetSession().Transaction;
            
            if (tr.IsActive)
            {
                try
                {
                    tr.Commit();
                }
                catch
                {
                    tr.Rollback();
                    throw;
                }
                finally {
                    tr.Dispose();
                }
            }
        }

        
    }
}
