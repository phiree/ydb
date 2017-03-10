using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;

public partial class advertisement_Default :BasePage
{
    BLLAdvertisement bllAd = Bootstrap.Container.Resolve<BLLAdvertisement>();

    protected override void OnPreInit(EventArgs e)
    {
        NHibernateUnitOfWork.UnitOfWork.Start();//.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        base.OnPreInit(e);
    }
    protected override void OnUnload(EventArgs e)
    {
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush(System.Data.IsolationLevel.ReadCommitted);

        base.OnUnload(e);
    }
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

            Literal litPT = e.Row.FindControl("litPT") as Literal;
            switch (adv.PushType)
            {
                case "customer":
                    litPT.Text = "用户";
                    break;
                case "business":
                    litPT.Text = "商家";
                    break;
                default:
                    break;
            }

            var img = e.Row.FindControl("imgAdv") as System.Web.UI.HtmlControls.HtmlImage;
            img.Src = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl")  +adv.ImgUrl;

            
        }
    }
    
    
}