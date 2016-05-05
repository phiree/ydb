using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class advertisement_Default : System.Web.UI.Page
{
    BLLAdvertisement bllAd = Installer.Container.Resolve<BLLAdvertisement>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    { 
        long totalRecord;
        int currentPageIndex=1;
        string paramPage=Request.Params["page"];
        if(!string.IsNullOrEmpty(paramPage))
        {
         currentPageIndex=int.Parse(paramPage);
        }
        gvMember.DataSource = bllAd.GetADList(currentPageIndex, pager.PageSize, out totalRecord);
        pager.RecordCount = Convert.ToInt32(totalRecord);
        gvMember.RowDataBound += new GridViewRowEventHandler(gvMember_RowDataBound);
        gvMember.DataBind();
    }

    void gvMember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Literal litType = e.Row.FindControl("litType") as Literal;
            Advertisement adv = e.Row.DataItem as Advertisement;
            if (adv.IsUseful)
            {
                litType.Text = "是";
            }
            else
            {
                litType.Text = "否";
            }

            var img = e.Row.FindControl("imgAdv") as System.Web.UI.HtmlControls.HtmlImage;
            img.Src = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl")  +adv.ImgUrl;

            
        }
    }
    
    
}