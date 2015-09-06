using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class DZService_Detail : System.Web.UI.Page
{
    public DZService CurrentService = new DZService();
    BLLDZService bllService = new BLLDZService();
    BLLDZTag bllTag = new BLLDZTag();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strId = Request["serviceId"];
        if (!string.IsNullOrEmpty(strId))
        {
            CurrentService = bllService.GetOne(new Guid(strId));
            BindTag(new Guid(strId));
        }
    }
    private void BindTag(Guid ServiceId)
    {
        IList<DZTag> tags = new List<DZTag>();
       
            tags = bllTag.GetTagForService(ServiceId);
            rptTags.DataSource = tags;
            rptTags.DataBind();
        
    }
}