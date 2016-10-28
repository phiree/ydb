using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class poolmonitor : System.Web.UI.Page
{
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.AdminBusiness.PoolMonitor");
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/bin/MySql.Data.dll");
        Assembly ms = Assembly.LoadFrom(path);
        Type type = ms.GetType("MySql.Data.MySqlClient.MySqlPoolManager");
        MethodInfo mi = type.GetMethod("GetPool", BindingFlags.Static | BindingFlags.Public);
        string connectionString =PHSuit.Security.Decrypt( System.Configuration.ConfigurationManager.ConnectionStrings["DianzhuConnectionString"].ConnectionString,false);
        var pool = mi.Invoke(null, new object[] { new MySqlConnectionStringBuilder(connectionString) });
        Type mip = ms.GetType("MySql.Data.MySqlClient.MySqlPool");
      //  MemberInfo[] mei1 = mip.GetMember("inUsePool", BindingFlags.NonPublic);
      var   totalAvailable = (int)pool.GetType().InvokeMember("available", BindingFlags.GetField | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, pool, new object[] { });
        var o = pool.GetType().InvokeMember("inUsePool", BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, pool, new object[] { });
        var o1 = pool.GetType().InvokeMember("idlePool", BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, pool, new object[] { });
     var   inUseCount = (int)o.GetType().InvokeMember("Count", BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public, null, o, null);
     var   idleCount = (int)o1.GetType().InvokeMember("Count", BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public, null, o1, null);

        string info= "totalAvailable   [" + totalAvailable+ "]   inUsePool    [" + inUseCount +"]   idlePool   [" + idleCount + "]   ";
       
        log.Info(info);
        Response.Write(info);

        
       
    }
}