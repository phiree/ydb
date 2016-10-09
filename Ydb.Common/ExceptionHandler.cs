using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common
{
  public  class ExceptionHandler
    {
        public static void Throw(string errMessage,log4net.ILog log)
        {
            log.Error(errMessage);
            throw new Exception(errMessage);
        }
    }
}
