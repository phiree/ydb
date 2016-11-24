using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 

//using Dianzhu.ApplicationService;
//using Dianzhu.ApplicationService.Staff;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
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
        var filter = new TraitFilter() { pageNum = index , pageSize = pager.PageSize };
      
        IList<Staff> staffList = staffService.GetStaffs(filter,string.Empty,string.Empty,String.Empty, String.Empty, String.Empty,String.Empty,b.Id ); // GetListByBusiness(b.Id, index, pager.PageSize, out total);
        pager.RecordCount =(int) staffService.GetStaffsCount(string.Empty, string.Empty, String.Empty, String.Empty, String.Empty, String.Empty, b.Id);
        rptStaff.DataSource = staffList;
        rptStaff.DataBind();

        
    }
    protected void rptStaff_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            Guid id = new Guid(e.CommandArgument.ToString());
             staffService.Delete( id );
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            BindList();
        }
    }
}