using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace Dianzhu.BLL
{
    public class SiteConfig
    {
        public static string BusinessImagePath
        {
            get
            {
                return ConfigurationManager.AppSettings["business_image_root"];
            }
        }
    }
}
