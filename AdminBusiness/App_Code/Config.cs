using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
/// <summary>
///Config 的摘要说明
/// </summary>
public static class Config
{
    public static string BusinessImagePath
    {
        get
        {
            return Dianzhu.Config.Config.GetAppSetting("business_image_root");
        }
    }
    public static readonly string  pwssword_recovery_spliter = "1z0m-2mkirowxi2-wrk2";
    public static log4net.ILog log = log4net.LogManager.GetLogger(Dianzhu.Config.Config.GetAppSetting("LoggerName"));
}