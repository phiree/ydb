using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class DZService_Edit : System.Web.UI.Page
{
    private Guid ServiceId = Guid.Empty;
    BLLDZService bllService = new BLLDZService();
    private bool IsNew { get { return ServiceId == Guid.Empty; } }
    DZService CurrentService = new DZService();
    protected void Page_Load(object sender, EventArgs e)
    {
        string paramId = Request.Params["id"];
        if (!string.IsNullOrEmpty(paramId)) { CurrentService = bllService.GetOne(new Guid(paramId)); }
        if (!IsPostBack)
        {
            LoadInit();
            LoadForm();
        }
    }
    public void LoadInit()
    { }
    public void LoadForm()
    { }
    public void UpdateForm()
    { }

    public void Save()
    { 
        
    }
}