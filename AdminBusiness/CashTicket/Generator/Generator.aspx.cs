using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class CashTicket_Generator : BasePage
{
    BLLCashTicketTemplate bllCashTicketTemplate = new BLLCashTicketTemplate();
    protected void Page_Load(object sender, EventArgs e)
    {
        InitForm();
        if (!IsPostBack)
        {
            LoadForm();
        }
    }
    private void InitForm()
    {
        BindTemplateList();
    }
    private void LoadForm()
    {
        string strTemplateId = Request.Params["templateid"];
        if (string.IsNullOrEmpty(strTemplateId))
        {
            return;
        }
        Guid templateId = new Guid(strTemplateId);
        ddlTemplate.SelectedValue = strTemplateId;
    }
    private void BindTemplateList()
    {
        ddlTemplate.DataSource = bllCashTicketTemplate.GetTemplateList(CurrentBusiness);
        ddlTemplate.DataTextField = "Name";
        ddlTemplate.DataValueField = "Id";
        ddlTemplate.DataBind();
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        BLLCashTicket bllcashticket = new BLLCashTicket();
        BusinessUser businessuser = (BusinessUser)CurrentUser;
        Guid templateId=new Guid(ddlTemplate.SelectedValue);
        CashTicketTemplate cashTicketTemplate=bllCashTicketTemplate.GetOne(templateId);
        string result=  bllcashticket.CreateBatch(businessuser.BelongTo, Convert.ToInt32(tbxTotal.Text.Trim()), cashTicketTemplate);
        Response.Write(result);

    }
}