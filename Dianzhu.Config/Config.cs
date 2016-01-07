using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using log4net;
namespace Dianzhu.Config
{
    
    public static class Config
    {
        //外网服务器地址
        static readonly string serverIpAddressV4 = "115.159.72.236";

        static ILog log = LogManager.GetLogger("Dianzhu.Config");
       
        static string[] envList = new string[] {"local","server" };
        static string currentEnv = string.Empty;
        //初始化, 确定当前环境
        static Config()
        {
            
            IPAddress[] iplist=  Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress ipV4 = null;
            foreach (IPAddress ip in iplist)
            {
                //IpV4 Address
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipV4 = ip;
                }
            }
            if (ipV4 == null)
            {
                string errMsg = "无法获得服务器IP地址";
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            //本地环境
            
            if (ipV4.ToString()== serverIpAddressV4)
            {
                currentEnv = envList[1];
            }
            else {

                currentEnv = envList[0];
               

            }
            //判断当前的环境
             
        }
        public static string ConnectionString {
            get {
                return ConfigurationManager.ConnectionStrings[currentEnv + ".DianzhuConnectionString"].ConnectionString;
                  
            }
        }

        public static string GetAppSetting(string key)
        {
            //所有环境下使用同一个值的key, 以 "all."开头
            
            string all = "all." + key;
            string hostName = Dns.GetHostName().ToLower();
            string specific =currentEnv + "." + key;
            string specific_hostName=hostName+"."+ currentEnv + "." + key;
            bool is_all = false;
            bool is_specific = false;
            bool is_specific_hostname = false;
            string value = string.Empty;
            if (ConfigurationManager.AppSettings.AllKeys.Contains(all))
            {
                is_all = true;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains(specific))
            {
                is_specific = true;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains(specific_hostName))
            {
                is_specific_hostname = true;
            }
            if (is_specific_hostname)
            {
                value = ConfigurationManager.AppSettings.Get(specific_hostName);

                if (is_specific)
                {
                    log.Warn(string.Format("AppSetting [{0}] has both specific and specifichostname values,please check", key));
                }
                if (is_all)
                {
                    log.Warn(string.Format("AppSetting [{0}] has both all and specifichostname values,please check", key));
                }
            }
            else if(is_specific)
            {
                value = ConfigurationManager.AppSettings.Get(specific);
                if (is_all)
                {
                    log.Warn(string.Format("AppSetting [{0}] has both all and specific values,please check", key));
                }
            }
            else
            {
                if (is_all)
                {
                    value = ConfigurationManager.AppSettings.Get(all);
                }
                else
                {
                    string errmsg = "did't find any match value for key:" + key;
                    log.Error(errmsg);
                    throw new Exception(errmsg);
                }
            }

            return value;
        }
       
    }
}
