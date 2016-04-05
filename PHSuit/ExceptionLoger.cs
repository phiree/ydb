using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ExceptionLoger 的摘要说明
/// </summary>
public class ExceptionLoger
{
    public ExceptionLoger()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    static int a = 0;
    public static void ExceptionLog(log4net.ILog log, Exception e)
    {
        string str = string.Empty;
        if (a > 0)
        {
            str = "InnerException:";
        }
        log.Error(str+e.Message);
        if (e.InnerException != null)
        {
            a++;
            ExceptionLog(log, e.InnerException);
        }
        else
        {
            a = 0;
        }
    }
}