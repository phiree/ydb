using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.ApplicationService;
using Dianzhu.ApplicationService.Staff;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.BusinessResource.DomainModel;

public partial class Staff_Default : BasePage
{
    
    IStaffService staffService =Bootstrap.Container.Resolve<IStaffService>();
    public string merchantID {
        get {
            return System.Web.Security.Membership.GetUser().ProviderUserKey.ToString();
        }
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
        int total=30;
        Business b = CurrentBusiness;
        string strIndex = Request["page"];
        int index = 0;
        if (!string.IsNullOrEmpty(strIndex))
        {
            index = int.Parse(strIndex);
        }
        var filter = new common_Trait_Filtering {pageNum = index.ToString(), pageSize = pager.PageSize.ToString()};
        var filterStaff = new common_Trait_StaffFiltering();
        IList<staffObj> staffList = staffService.GetStaffs(b.Id.ToString(), filter, filterStaff, null); // GetListByBusiness(b.Id, index, pager.PageSize, out total);
        pager.RecordCount =Convert.ToInt32( staffService.GetStaffsCount(b.Id.ToString(), filterStaff, null).count);
        rptStaff.DataSource = staffList;
        rptStaff.DataBind();

        
    }
    protected void rptStaff_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            Guid id = new Guid(e.CommandArgument.ToString());
             staffService.DeleteStaff(CurrentBusiness.Id.ToString(),id.ToString(),null);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            BindList();
        }
    }
}