using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.Presenters.Raw
{
    public  class CustomerListPresenter
    {
        Views.Raw.ICustomerList iview;
        public CustomerListPresenter(Views.Raw.ICustomerList iview)
        {
            this.iview = iview;
        }
        /// <summary>
        /// 增加一个客户
        /// </summary>
        public void AddCustomer(string customerName)
        {
            Models.Raw.ChatModel m = new Models.Raw.ChatModel();
            iview.CustomerNames.Add(customerName);
        }
    }
}
