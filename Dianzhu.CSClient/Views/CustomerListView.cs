using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dianzhu.CSClient.Views
{
    public partial class CustomerListView : UserControl
    {
        public CustomerListView()
        {
            InitializeComponent();
        }
        public event EventHandler<CustomerCameArgs> CustomerComing;
        
    }
    public class CustomerCameArgs : EventArgs
    {
        public string CustomerName { get; set; }
        
    }
}
