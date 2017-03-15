using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

public partial class TagControl : System.Web.UI.UserControl
{


    IDZTagService tagService = Bootstrap.Container.Resolve<IDZTagService>();
    public string ServiceId { get; set; }
    public string BusinessId { get; set; }
    public string ServiceTypeId { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadTagList();
    }
    private IList<DZTag> GetTagList()
    {
        IList<DZTag> tags = new List<DZTag>();
        if (!string.IsNullOrEmpty(ServiceId))
        {
            tags = tagService.GetTagForService(new Guid(ServiceId));
        }
        return tags;
    }
    private void LoadTagList()
    {
        rptTags.DataSource = GetTagList();
        rptTags.DataBind();
    }
}