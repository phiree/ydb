using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL.Finance;
/// <summary>
/// 流水帐列表
/// </summary>
public partial class finance_Balance :BasePage
{
    IBalanceFlowService balanceFlowService = Bootstrap.Container.Resolve<IBalanceFlowService>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBalance();
        }
    }
    private void BindBalance()
    {
        var list = balanceFlowService.GetList();
        gvBalance.DataSource = list;
        gvBalance.DataBind();
    }
}