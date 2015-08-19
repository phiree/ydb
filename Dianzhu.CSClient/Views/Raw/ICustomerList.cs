using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.Views.Raw
{
    public  interface ICustomerList
    {
        IList<string> CustomerNames{get;set;}
    }
}
