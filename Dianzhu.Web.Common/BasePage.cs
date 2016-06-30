using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.Web.Common
{
    public class BasePage:System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            NHibernateUnitOfWork.UnitOfWork.Start();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        }
    }
}
