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
    public partial class UC_CustomerList : UserControl, IViewCustomerList
    {
        
        public UC_CustomerList()
        {
            InitializeComponent();
        }

        public event CustomerClick CustomerClick;

        public void AddCustomer(DZMembership customer)
        {

            Action lambda = () =>
            {
                Button btnCustomer = new Button { Text = customer.DisplayName };
                btnCustomer.Tag = customer;
                btnCustomer.AutoSize = true;
                btnCustomer.Name = "btn" + customer.Id;
                btnCustomer.Click += BtnCustomer_Click;
                pnlCustomerList.Controls.Add(btnCustomer);
            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else { lambda(); }

        }

        private void BtnCustomer_Click(object sender, EventArgs e)
        {
            if (CustomerClick != null)
            {
                DZMembership customer = (DZMembership)((Button)sender).Tag;
                CustomerClick(customer);
                SetCustomerReaded(customer);
            }
        }

        public void RemoveCustomer(DZMembership customer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置按钮的 样式.
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="buttonStyle"></param>
        private void SetCustomerButtonStyle(DZMembership dm, em_ButtonStyle buttonStyle)
        {
            Action lambda = () => {
                if (pnlCustomerList.Controls.Find(   dm.Id.ToString(), true).Count() > 0)
                {
                    Button btn = (Button)pnlCustomerList.Controls.Find(dm.Id.ToString(), true)[0];
                    Color foreColor = Color.White;
                    switch (buttonStyle)
                    {
                        case em_ButtonStyle.Login:
                            foreColor = Color.Green;
                            break;
                        case em_ButtonStyle.LogOff:
                            foreColor = Color.Gray;
                            break;
                        case em_ButtonStyle.Readed: foreColor = Color.Black; break;
                        case em_ButtonStyle.Unread: foreColor = Color.Red; break;
                        case em_ButtonStyle.Actived: foreColor = Color.Yellow; break;
                        default: break;
                    }
                    btn.ForeColor = foreColor;
                }
            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else
            {
                lambda();
            }

        }

      
        public void SetCustomerUnread(DZMembership customer, int messageAmount)
        {
            SetCustomerButtonStyle(customer, em_ButtonStyle.Unread);
        }

        public void SetCustomerReaded(DZMembership customer)
        {
            SetCustomerButtonStyle(customer, em_ButtonStyle.Readed);
        }
    }
}
