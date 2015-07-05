using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using System.Web;
namespace Dianzhu.BLL.FormModel
{
    public class FormBusiness
    {
        public Business business { get; set; }
        public string AddressInfo { get; set; }
        public HttpPostedFile BusinessAvatar { get; set; }
        public void AdaptToBusiness(AddressParser addressParser)
        { 
            Area area;
            double latitude,longtitude;
            addressParser.ParseAddress(out area, out latitude, out longtitude);
            business.AreaBelongTo = area;
            business.Latitude = latitude;
            business.Longitude = longtitude;
            business.Address=string.Empty;

            BusinessImage image = new BusinessImage();
             
           
        }

    }
}
