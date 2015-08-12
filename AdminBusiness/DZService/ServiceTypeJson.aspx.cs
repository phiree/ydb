using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
public partial class DZService_ServiceTypeJson : System.Web.UI.Page
{
    BLLServiceType bllType = new BLLServiceType();
    protected void Page_Load(object sender, EventArgs e)
    {
     var list=   bllType.GetAll();
     var jsonString= JsonConvert.SerializeObject(list, Formatting.Indented);
        lblMsg.Text=jsonString;
     //Response.Write(jsonString)
    }
}