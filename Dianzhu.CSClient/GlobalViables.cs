using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using System.Configuration;
using Dianzhu.Model;
namespace Dianzhu.CSClient
{
    
    public class GlobalViables
    {
    
        public static readonly string WebServerRoot = ConfigurationManager.AppSettings["WebServerRoot"];
        public static readonly string ButtonNamePrefix = "btn";
        public static BLLReception BLLReception = new BLLReception();
        public static BLLDZService BLLDZService = new BLLDZService();
        public static DZMembershipProvider BLLMembership = new DZMembershipProvider();
        public static BLLServiceOrder BLLServiceOrder = new BLLServiceOrder();
        
            
 
       
       
    }
}
