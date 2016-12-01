using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.BusinessResource.DomainModel;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
     IList< Business> businessList=   NHibernateUnitOfWork.UnitOfWork.CurrentSession.QueryOver< Business>().List();

        rptBusinessList.DataSource = businessList;
        rptBusinessList.DataBind();
         

    }
}