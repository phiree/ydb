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


        static string Server = string.Empty;
        static string Server_Media = string.Empty;
        static string Server_Pay = string.Empty;
        static string Server_HttpAPI = string.Empty;

        static string Port_Media = string.Empty;
        static string Port_Pay = string.Empty;
        static string Port_HttpAPI = string.Empty;

       

         
        static Config()
        {
            //读取server  不存在的 server 默认读取 "Server"
             

        }
        public static string ConnectionString {
            get {
                return ConfigurationManager.ConnectionStrings["DianzhuConnectionString"].ConnectionString;
                  
            }
        }
 
        public static string GetAppSetting(string key)
        {
 

            string value = ConfigurationManager.AppSettings[key];
            return value;
        }

        
       
        
       
    }
}
