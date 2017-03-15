using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;
using Ydb.Common.Application;
using Ydb.Common.Domain;

public partial class area_MapBaidu : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       IAreaService  bllArea = Bootstrap.Container.Resolve<IAreaService>();
        string con_file_path = System.Web.HttpContext.Current.Server.MapPath("allcity.json");
        using (StreamReader sr = new StreamReader(con_file_path))
        {
            try
            {
                //构建Json.net的读取流  
                //JsonReader reader = new JsonTextReader(sr);
                
                String strData = sr.ReadToEnd();
                if (string.IsNullOrEmpty(strData))
                {
                    throw new Exception("没有请求参数!");
                }
                Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(strData);
                Newtonsoft.Json.Linq.JObject jo1 = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(jo["City"][0].ToString());
                Newtonsoft.Json.Linq.JToken jt= jo1.First;
                //for (int i = 1; i < jo1.Count; i++)
                //{
                //    IList<Dianzhu.Model.BaiduCity> baiduCity = JsonConvert.DeserializeObject<IList<Dianzhu.Model.BaiduCity>>(strCitys);
                //}
                while (jt!=null)
                {
                    jt = jt.Next;
                    IList< BaiduCity> baiduCitys = JsonConvert.DeserializeObject<IList< BaiduCity>>(jt.First.ToString());
                    foreach ( BaiduCity baiduCity in baiduCitys)
                    {
                        try
                        {
                             Area area = bllArea.GetAreaByBaiduName(baiduCity.name);
                            if (string.IsNullOrEmpty(area.BaiduCode))
                            {
                                area.BaiduCode = baiduCity.code;
                                area.BaiduName = baiduCity.name;
                            }
                            lblSuccess.Text = lblSuccess.Text + baiduCity.name + "," + baiduCity.code + "," + baiduCity.key + ";";
                        }
                        catch
                        {

                            lblFail.Text = lblFail.Text + baiduCity.name + "," + baiduCity.code + "," + baiduCity.key + ";";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}