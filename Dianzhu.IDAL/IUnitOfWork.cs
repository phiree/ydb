using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.IDAL
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
 
}
