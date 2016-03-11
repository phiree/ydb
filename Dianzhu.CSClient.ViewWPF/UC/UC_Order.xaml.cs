using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_Order.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Order : UserControl,IView.IViewOrder
    {
        public UC_Order()
        {
            InitializeComponent();
        }

        public decimal DepositAmount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Memo
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ServiceOrder Order
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public event CreateOrderClick CreateOrderClick;
    }
}
