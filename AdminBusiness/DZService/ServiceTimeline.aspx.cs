using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using Newtonsoft.Json;
public partial class DZService_ServiceTimeline : System.Web.UI.Page
{
    BLLDZService bllService = new BLLDZService();
    public string jsonData = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Guid serviceId = new Guid(Request["serviceId"]);
        DZService service = bllService.GetOne(serviceId);
        lblServiceName.Text= service.Name;
        jsonData = JsonConvert.SerializeObject(service.OpenTimes);
    }
    
}