using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
public partial class YTask_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        YdbJobManager jobFactory = Bootstrap.Container.Resolve<YdbJobManager>();
        if (!IsPostBack)
        {          
            BindData(jobFactory.GetAllJobs());
        }
    }
    
    private void BindData(IList<JobDto> jobs)
    {
        gvJobs.DataSource = jobs.OrderByDescending(x=>x.StartTime);
        gvJobs.DataBind();
    }
  
}