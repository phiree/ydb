using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
///BrowserCheck 的摘要说明
/// </summary>
public class BrowserCheck
{
	public BrowserCheck()
	{
         
	}

    //判断IE浏览器版本是否小于或等于7.0
    public static bool CheckVersion()
    {
        bool cvalue = false;
        if (Convert.ToInt32(HttpContext.Current.Request.Browser.MajorVersion) > 7)
        {
            cvalue = true;
        }

        return cvalue;
    
    }
}