using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Finance.Application;
using Dianzhu.IDAL;
using Dianzhu.Model;
public partial class Finance_Default : BasePage
{
    //Dianzhu.BLL.IBLLServiceOrder bllOrder = Bootstrap.Container.Resolve<Dianzhu.BLL.IBLLServiceOrder>();

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
            BindTotal();
            BindAccount();
        }
    }

    /// <summary>
    /// 绑定流水列表
    /// </summary>
    private void BindList()
    {
        IBalanceFlowService dalBalance = Bootstrap.Container.Resolve<IBalanceFlowService>();
        Ydb.Common.Specification.TraitFilter traitFilter = new Ydb.Common.Specification.TraitFilter();
        traitFilter.sortby = "OccurTime";
        BalanceFlowFilter balanceFlowFilter = new BalanceFlowFilter();
        balanceFlowFilter.AccountId = CurrentBusiness.Id.ToString();
        IList<BalanceFlowDto> balanceList = dalBalance.GetBalanceFlowList(traitFilter, balanceFlowFilter);// dalBalance.GetAll().Where(x=>x.AccountId== CurrentBusiness.Id.ToString()).ToList();// dalBalance.Find(x =>x.MemberId== CurrentBusiness.OwnerId);
        // int totalAmount;
        // IList<ServiceOrder> orderList = bllOrder.GetListForBusiness(CurrentBusiness, 0, 99999, out totalAmount);
        //var filteredList= balanceList.Where(x => orderList.Select(y => y.Id.ToString()).ToList().Contains(x.RelatedObjectId));
        rpFinanceList.DataSource = balanceList;
        rpFinanceList.DataBind();
    }

    /// <summary>
    /// 绑定余额
    /// </summary>
    private void BindTotal()
    {
        IBalanceTotalService balanceTotal = Bootstrap.Container.Resolve<IBalanceTotalService>();
        BalanceTotalDto balanceTotalDto = balanceTotal.GetOneByUserId(CurrentBusiness.Id.ToString());
        if (balanceTotalDto == null)
        {
            lblBalanceTotal.Text = "0";
        }
        else
        {
            lblBalanceTotal.Text = balanceTotalDto.Total.ToString();
        }
    }

    /// <summary>
    /// 绑定提现收款账户
    /// </summary>
    private void BindAccount()
    {
        IBalanceAccountService balanceAccount = Bootstrap.Container.Resolve<IBalanceAccountService>();
        BalanceAccountDto balanceAccountDto = balanceAccount.GetAccount(CurrentBusiness.Id.ToString());
        if (balanceAccountDto == null)
        {
            lblBalanceAccount.Text = "还没有绑定提现账户！";
        }
        else
        {
            lblBalanceAccount.Text = balanceAccountDto.Account;
        }
    }



    protected void rpFinanceList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item|| e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //var item = (BalanceFlowDto)e.Item.DataItem;
            //var order= bllOrder.GetOne(new Guid(item.RelatedObjectId));
            //string serialOrderNo = item.SerialNo;//order.SerialNo;
            var li = (Literal) e.Item.FindControl("liNo");


            li.Text = (e.Item.ItemIndex + 1).ToString(); ;
           
        }
    }
}