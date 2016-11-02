using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL.Finance;
using Dianzhu.IDAL;
using Dianzhu.Model;
public partial class Finance_Default : BasePage
{
    Dianzhu.BLL.IBLLServiceOrder bllOrder = Bootstrap.Container.Resolve<Dianzhu.BLL.IBLLServiceOrder>();

   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            BindList();
        }
    }

    private void BindList()
    {

        Dianzhu.IDAL.Finance.IDALBalanceFlow dalBalance = Bootstrap.Container.Resolve<Dianzhu.IDAL.Finance.IDALBalanceFlow>();
        IList<Dianzhu.Model.Finance.BalanceFlow> balanceList = dalBalance.Find(x =>x.MemberId== CurrentBusiness.OwnerId);

        int totalAmount;
        IList<ServiceOrder> orderList = bllOrder.GetListForBusiness(CurrentBusiness, 0, 99999, out totalAmount);

       var filteredList= balanceList.Where(x => orderList.Select(y => y.Id.ToString()).ToList().Contains(x.RelatedObjectId));

        rpFinanceList.DataSource = filteredList;

        rpFinanceList.DataBind();
    }

    protected void rpFinanceList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item|| e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var item = (Dianzhu.Model.Finance.BalanceFlow)e.Item.DataItem;
           var order= bllOrder.GetOne(new Guid(item.RelatedObjectId));
            string serialOrderNo = order.SerialNo;
            var li = (Literal) e.Item.FindControl("liSerialNo");
         
             
                li.Text = serialOrderNo;
           
        }
    }
}