﻿using System;
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
    /// UC_ScrollView.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ScrollView : UserControl
    {
        public UC_ScrollView()
        {
            InitializeComponent();
        }

        private void sp_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ScrollViewer.ScrollableHeight == ScrollViewer.VerticalOffset)
            {
                ScrollViewer.ScrollToEnd();
            }
        }
    }
}
