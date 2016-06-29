using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Dianzhu.Web.Common
{
    public class IocModule : System.Web.IHttpModule, IDisposable
    {
        private HttpApplication httpApplication;
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            if (context == null)
                throw new ArgumentException("context");

            this.httpApplication = context;

            this.httpApplication.BeginRequest += new EventHandler(BeginRequest);
            this.httpApplication.EndRequest += new EventHandler(EndRequest);
            this.httpApplication.Error += new EventHandler(Error);

             
        }

        private void Error(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EndRequest(object sender, EventArgs e)
        {
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        }

        private void BeginRequest(object sender, EventArgs e)
        {
            //NHibernateUnitOfWork.UnitOfWork.Start();
        }
    }
}
