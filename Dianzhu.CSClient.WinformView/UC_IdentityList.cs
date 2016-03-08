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
    public partial class UC_IdentityList : UserControl, IViewIdentityList
    {
        
        public UC_IdentityList()
        {
            InitializeComponent();
        }

        public event IdentityClick IdentityClick;

        public void AddIdentity(ServiceOrder order)
        {

            Action lambda = () =>
            {
                Button btnIdentity = new Button { Text =order.Customer.DisplayName };
                btnIdentity.Tag = order;
                btnIdentity.AutoSize = true;
                btnIdentity.Name = "btn" + order.Id;
                btnIdentity.Click += BtnIdentity_Click;
                pnlIdentityList.Controls.Add(btnIdentity);
            };
            if (InvokeRequired)
            {
                Invoke(lambda);
            }
            else { lambda(); }

        }

        private void BtnIdentity_Click(object sender, EventArgs e)
        {
            if (IdentityClick != null)
            {
                ServiceOrder  order = (ServiceOrder)((Button)sender).Tag;
                IdentityClick(order);
                SetIdentityReaded(order);
            }
        }

        public void RemoveIdentity(ServiceOrder order)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置按钮的 样式.
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="buttonStyle"></param>
        private void SetIdentityButtonStyle(ServiceOrder order, em_ButtonStyle buttonStyle)
        {
            Action lambda = () => {
                if (pnlIdentityList.Controls.Find(   order.Id.ToString(), true).Count() > 0)
                {
                    Button btn = (Button)pnlIdentityList.Controls.Find(order.Id.ToString(), true)[0];
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

      
        public void SetIdentityUnread(ServiceOrder order, int messageAmount)
        {
            SetIdentityButtonStyle(order, em_ButtonStyle.Unread);
        }

        public void SetIdentityReaded(ServiceOrder order)
        {
            SetIdentityButtonStyle(order, em_ButtonStyle.Readed);
        }
    }
}
