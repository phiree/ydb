using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Dianzhu.BLL;
using Dianzhu.Model;
/// <summary>
/// config 的摘要说明
/// </summary>
public class config
{

    public static   bool EnableCache = true;

      static config()
    {
#if DEBUG
    EnableCache=false;
#endif
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static PagedDataSource pds(IList<ServiceOrder> il, int page, int pagecount) //分页功能
    {
        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = il;
        pds.AllowPaging = true;//允许分页
        pds.PageSize = pagecount;//单页显示项数
        pds.CurrentPageIndex = page;
        return pds;


    }
}