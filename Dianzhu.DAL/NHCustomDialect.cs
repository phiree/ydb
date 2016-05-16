using NHibernate;
using NHibernate.Dialect.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.DAL
{
  public   class NHCustomDialect:NHibernate.Dialect.MySQLDialect
    {
        public NHCustomDialect()
        {
            RegisterFunction("datepart", new SQLFunctionTemplate(NHibernateUtil.Date, "DATE(?1)"));
        }
    }
}
