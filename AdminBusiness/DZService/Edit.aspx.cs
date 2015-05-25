using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using PHSuit;
/// <summary>
/// 编辑/新增 服务信息.
/// </summary>
public partial class DZService_Edit : System.Web.UI.Page
{
    private Guid ServiceId = Guid.Empty;
    BLLDZService bllService = new BLLDZService();
    BLLServiceType bllServiceType = new BLLServiceType();
    BLLServiceProperty bllServiceProperty = new BLLServiceProperty();
    public IList<ServiceProperty>  TypeProperties = new List<ServiceProperty>();
    private bool IsNew { get { return ServiceId == Guid.Empty; } }
    
    DZService CurrentService = new DZService();//当前的服务 对象.
    ServiceType ServiceType = new ServiceType();
    protected void Page_Load(object sender, EventArgs e)
    {
        string paramId = Request.Params["id"];
        if (!string.IsNullOrEmpty(paramId))
        {
            ServiceId = new Guid(paramId);
            CurrentService = bllService.GetOne(ServiceId);
        }
        else //新建服务一定要传入typeid
        {
            string paramTypeId = Request.Params["typeId"];
            if (string.IsNullOrEmpty(paramTypeId))
            {
                Notification.Show(Page, "抱歉", "页面参数有误,请从正常入口访问本页面", "/");
            }
            Guid typeId = new Guid(paramTypeId);
            ServiceType = bllServiceType.GetOne(typeId);
            TypeProperties = bllServiceProperty.GetList(typeId);


        }
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