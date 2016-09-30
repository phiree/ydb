using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Dianzhu.Pay
{
    public class GetAliRequestGet
    {
        public static SortedDictionary<string, string> GetRequestGet(NameValueCollection coll)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            //Load Form variables into NameValueCollection variable.
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], coll[requestItem[i]]);
            }

            return sArray;
        }
    }
}
