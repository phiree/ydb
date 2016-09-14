using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL.Finance;
using Dianzhu.IDAL;

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
        IList<Dianzhu.Model.Finance.BalanceFlow> balanceList = dalBalance.Find(x => x.Member.Id == CurrentBusiness.Owner.Id);

        rpFinanceList.DataSource = balanceList;

        rpFinanceList.DataBind();
    }
}