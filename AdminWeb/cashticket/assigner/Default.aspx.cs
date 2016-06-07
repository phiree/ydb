using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class cashticket_assigner_Default : System.Web.UI.Page
{

    BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
    BLLArea bllArea = Bootstrap.Container.Resolve<BLLArea>();
   
    protected void Page_Load(object sender, EventArgs e)
    {
         
        if (!IsPostBack)
        {
            LoadForm();
            GetData();
            DisplaySummary();
        }
    }
   
    private void LoadForm()
    {
        ddlArea.DataSource = bllBusiness.GetAreasOfBusiness();
        ddlArea.DataTextField = "Name";
        ddlArea.DataValueField = "Id";
        ddlArea.DataBind();
    }

    IList<Business> Businesses;
    Area Area;
    private void GetData()
    {
        string areaId = ddlArea.SelectedValue;
        if (areaId != null)
        {
            Area = bllArea.GetOne(Convert.ToInt32(areaId));
            Businesses = bllBusiness.GetBusinessInSameCity(Area);
        }
    }
    protected void btnAssign_Click(object sender,EventArgs e)
    {
        GetData();
        //CashTicketAssignForArea cashticketAssignerArea = new CashTicketAssignForArea(Area, Businesses);
        //cashticketAssignerArea.Assign();
        CashTicketAssigner_Task task = Bootstrap.Container.Resolve<CashTicketAssigner_Task>();
        task.Assign();
    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
        DisplaySummary();
    }

    private void DisplaySummary(){

        lblBusinessAmount.Text = Businesses.Count.ToString();
      int amount = 0;
      foreach (Business b in Businesses)
      {
          amount += b.CashTicketsAvaiableToAssignAmount;
      }
      lblCashticketAmount.Text = amount.ToString();
    }
}