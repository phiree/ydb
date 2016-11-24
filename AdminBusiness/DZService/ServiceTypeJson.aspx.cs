using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using System.IO;
using Ydb.BusinessResource.Application;

public partial class DZService_ServiceTypeJson : BasePage
{
    IServiceTypeService bllType = Bootstrap.Container.Resolve<IServiceTypeService>();
    protected void Page_Load(object sender, EventArgs e)
    {
     var list=   bllType.GetAll();
     var jsonString= JsonConvert.SerializeObject(list, Formatting.Indented);
        lblMsg.Text=jsonString;
        CreatJsFile(jsonString);
     //Response.Write(jsonString)
    }

    protected void CreatJsFile(string json)
    {
        string filepath = "/js"+"/ServiceType.js";

        //if (!File.Exists(Server.MapPath(filepath)))
        //{

        FileStream fs1 = new FileStream(Server.MapPath(filepath), FileMode.Create, FileAccess.Write);//创建写入文件 
        StreamWriter sw = new StreamWriter(fs1);
        sw.WriteLine("var typeList=" + json);//开始写入值
        sw.Close();
        fs1.Close();

       // }

    
    }
}