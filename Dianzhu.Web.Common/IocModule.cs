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
            
        }

        private void EndRequest(object sender, EventArgs e)
        {
            if (!httpApplication.Request.CurrentExecutionFilePath.EndsWith(".aspx")
                && !httpApplication.Request.CurrentExecutionFilePath.EndsWith(".ashx")
                )
            {
                return;
            }
             
        }

        private void BeginRequest(object sender, EventArgs e)
        {
            if (!httpApplication.Request.CurrentExecutionFilePath.EndsWith(".aspx")
               && !httpApplication.Request.CurrentExecutionFilePath.EndsWith(".ashx")
               )
            {
                return;
            }
        }

        
    }
}
