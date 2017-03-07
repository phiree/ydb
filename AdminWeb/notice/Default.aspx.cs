using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Notice.Application;
using Ydb.ApplicationService.Application;
using N = Ydb.Notice.DomainModel;
public partial class notice_Default : System.Web.UI.Page
{
    INoticeService noticeService = Bootstrap.Container.Resolve<INoticeService>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList(noticeService.GetAll());
        }
    }
    private void BindList(IList<N.Notice> list)
    {
        gvNotice.DataSource = list;
        gvNotice.DataBind();
    }

    protected void gvNotice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "refuse")
        {

        }
    }
}