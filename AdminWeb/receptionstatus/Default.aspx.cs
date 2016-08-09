using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;

public partial class advertisement_Default : BasePage
{
    BLLReceptionStatus bllRS = Bootstrap.Container.Resolve<BLLReceptionStatus>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    { 
        //long totalRecord;
        int currentPageIndex=1;
        string paramPage=Request.Params["page"];
        if(!string.IsNullOrEmpty(paramPage))
        {
         currentPageIndex=int.Parse(paramPage);
        }
        var list = bllRS.GetAllList().OrderByDescending(x=>x.LastUpdateTime).ToList();
        gvMember.DataSource = list;
        pager.RecordCount = list.Count;
        gvMember.RowDataBound += new GridViewRowEventHandler(gvMember_RowDataBound);
        gvMember.DataBind();
    }

    void gvMember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Literal litType = e.Row.FindControl("litType") as Literal;
            ReceptionStatus rs = e.Row.DataItem as ReceptionStatus;
            rs.CSId = rs.CustomerService.Id.ToString();
            rs.CSName = rs.CustomerService.DisplayName;
            rs.CustomerId = rs.Customer.Id.ToString();
            rs.CustomerName = rs.Customer.DisplayName;

            //var img = e.Row.FindControl("imgAdv") as System.Web.UI.HtmlControls.HtmlImage;
            //img.Src = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + adv.ImgUrl;


        }
    }
    
    
}