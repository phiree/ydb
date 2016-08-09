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
    /// 显示加载中、没有数据或显示更多按钮的控件
    /// </summary>
    public partial class UC_Hint : UserControl
    {
        public UC_Hint(RoutedEventHandler btnClick)
        {
            InitializeComponent();

            btnMore.Click += btnClick;
        }
    }
}
