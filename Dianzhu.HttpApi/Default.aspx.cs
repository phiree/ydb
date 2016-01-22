using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;
using System.Drawing;
using log4net;
public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty( Request["refresh"]))
            { 
        ILog log = LogManager.GetLogger("Dianzhu.HttpApi");
        log.Debug("被调用");
        }

    }
    
}