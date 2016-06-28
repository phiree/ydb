using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.DAL
{
   public interface IUnitOfWork
    {
        
        void BeginTransaction();
        void FlushTransaction();
        void End();
         
        
    }
}
