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
    public partial class UC_Order : UserControl,IView.IViewOrder
    {
        public UC_Order()
        {
            InitializeComponent();
        }

        public ServiceOrder Order
        {
             
            set
            {
                tbxDeposit.Text = value.DepositAmount.ToString("0.0");
                tbxMemo.Text = value.Memo;
                lblOrderStatus.Text = value.OrderStatus.ToString();
                pnlDetails.Controls.Clear();
                foreach (ServiceOrderDetail detail in value.Details)
                {
                    UC_OrderDetail ucDetail = new UC_OrderDetail();
                    ucDetail.LoadData(detail);
                    pnlDetails.Controls.Add(ucDetail);

                }
            }
        }

        public event CreateOrderClick CreateOrderClick;

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            CreateOrderClick();
        }
    }
}
