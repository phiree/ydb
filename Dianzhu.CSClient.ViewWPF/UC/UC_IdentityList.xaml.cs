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
    /// IdentityList.xaml 的交互逻辑
    /// </summary>
    public partial class UC_IdentityList : UserControl, IViewIdentityList
    {
        public UC_IdentityList()
        {
            InitializeComponent();
        }

        public event IdentityClick IdentityClick;

        public void AddIdentity(ServiceOrder serviceOrder)
        {
            Action lambda = () =>
            {
                Button btnIdentity = new Button { Content = serviceOrder.Customer.DisplayName };
                btnIdentity.Tag = serviceOrder;

                btnIdentity.Name =PHSuit.StringHelper.SafeNameForWpfControl(serviceOrder.Id.ToString());
                btnIdentity.Click += BtnIdentity_Click;
                pnlIdentityList.Children.Add(btnIdentity);
                pnlIdentityList.RegisterName(btnIdentity.Name, btnIdentity);
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else { lambda(); }
        }

        private void BtnIdentity_Click(object sender, RoutedEventArgs e)
        {
            if (IdentityClick != null)
            {
                ServiceOrder order = (ServiceOrder)((Button)sender).Tag;
                IdentityClick(order);
                SetIdentityReaded(order);
            }
        }

        public void RemoveIdentity(ServiceOrder serviceOrder)
        {
            throw new NotImplementedException();
        }

        public void SetIdentityReaded(ServiceOrder serviceOrder)
        {
            SetIdentityButtonStyle(serviceOrder, em_ButtonStyle.Readed);
        }

        public void SetIdentityUnread(ServiceOrder serviceOrder, int messageAmount)
        {
            SetIdentityButtonStyle(serviceOrder, em_ButtonStyle.Unread);
        }
        private void SetIdentityButtonStyle(ServiceOrder order, em_ButtonStyle buttonStyle)
        {
            Action lambda = () => {
                var objBtn = pnlIdentityList.FindName(PHSuit.StringHelper.SafeNameForWpfControl(order.Id.ToString()));
                if (objBtn != null)
                {
                    Button btn = (Button)objBtn;
                    Color foreColor = Colors.White;
                    switch (buttonStyle)
                    {
                        case em_ButtonStyle.Login:
                            foreColor = Colors.Green;
                            break;
                        case em_ButtonStyle.LogOff:
                            foreColor = Colors.Gray;
                            break;
                        case em_ButtonStyle.Readed: foreColor = Colors.Black; break;
                        case em_ButtonStyle.Unread: foreColor = Colors.Red; break;
                        case em_ButtonStyle.Actived: foreColor = Colors.Yellow; break;
                        default: break;
                    }
                    btn.Foreground = new SolidColorBrush(foreColor);
                }
            };
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(lambda);
            }
            else
            {
                lambda();
            }

        }

    }
}
