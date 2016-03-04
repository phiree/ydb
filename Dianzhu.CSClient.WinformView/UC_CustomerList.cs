using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;

namespace Dianzhu.CSClient.WinformView
{
    /// <summary>
    /// 主界面:用户列表
    /// </summary>
    public partial class UC_CustomerList : UserControl,IViewCustomerList
    {
        public UC_CustomerList()
        {
            InitializeComponent();
        }

        public event CustomerClick CustomerClick;

        public void AddCustomer(DZMembership customer)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(DZMembership customer)
        {
            throw new NotImplementedException();
        }

        public void SetCustomerReaded(DZMembership customer)
        {
            throw new NotImplementedException();
        }

        public void SetCustomerUnread(DZMembership customer, int messageAmount)
        {
            throw new NotImplementedException();
        }
    }
}
