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

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_OrderDetail.xaml 的交互逻辑
    /// </summary>
    public partial class UC_OrderDetail : UserControl
    {
        public UC_OrderDetail()
        {
            InitializeComponent();
        }
        public int UnitAmount
        {
            get
            {
                int amount;
                bool isInt = int.TryParse(tbxUnitAmount.Text, out amount);
                return isInt ? Convert.ToInt32(tbxUnitAmount.Text) : 1;
            }
        }
        public string TargetTime
        {
            get
            {
                return tbxTargetTime.Text;
            }
        }
        public string TargetAddress
        {
            get
            {
                return tbxTargetAddress.Text;
            }
        }
        public void LoadData(Dianzhu.Model.ServiceOrderDetail detail)
        {
            lblBusinessName.Content = detail.OriginalService.Business.Name;
            lblServiceDescription.Content = detail.Description;
            lblPrice.Content = detail.UnitPrice + "/" + detail.ChargeUnit;
            lblServiceName.Content = detail.ServiceName;

        }

    }
}
