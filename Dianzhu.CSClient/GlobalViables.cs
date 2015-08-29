using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using System.Configuration;
using Dianzhu.Model;
namespace Dianzhu.CSClient
{
    
    public class GlobalViables
    {
        public static readonly string ServerName = ConfigurationManager.AppSettings["server"];
        public static readonly string Domain = ConfigurationManager.AppSettings["domain"];
        public static readonly string WebServerRoot = ConfigurationManager.AppSettings["WebServerRoot"];
        public static readonly string ButtonNamePrefix = "btn";
      
             
 
       
       
    }
}
