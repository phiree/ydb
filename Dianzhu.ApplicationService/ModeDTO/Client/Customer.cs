using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dianzhu.ApplicationService
{
    public class Customer
    {
        public string loginName { get; set; }
        public string password { get; set; }
        public string UserType { get; set; }

        public string UserID { get; set; }

        public Customer getCustomer(string token,string apiKey,bool isVerify)
        {
            string strCustomerJson = "";
            try
            {
                strCustomerJson = JWT.JsonWebToken.Decode(token,apiKey,isVerify);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Customer customer = JsonConvert.DeserializeObject<Customer>(strCustomerJson);
            return customer;
        }

    }
}
