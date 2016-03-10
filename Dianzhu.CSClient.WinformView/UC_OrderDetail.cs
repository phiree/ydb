using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dianzhu.Model;
namespace Dianzhu.CSClient.WinformView
{
    public partial class UC_OrderDetail : UserControl
    {
        
        public int UnitAmount {
            get {
                int amount;
               bool isInt= int.TryParse(tbxUnitAmount.Text, out amount);
                return isInt ? Convert.ToInt32(tbxUnitAmount.Text) : 1;
              }
        }
        public string TargetTime {
            get {
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
        public UC_OrderDetail()
        {
            InitializeComponent();
        }
        public void LoadData(ServiceOrderDetail detail)
        {
            lblBusinessName.Text = detail.OriginalService.Business.Name;
            lblDescription.Text = detail.Description;
            lblPrice.Text = detail.UnitPrice + "/" + detail.ChargeUnit;
            lblServiceName.Text = detail.ServiceName;
            
        }
    }
}
