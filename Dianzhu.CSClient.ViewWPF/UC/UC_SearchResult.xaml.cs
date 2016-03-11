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
    /// UC_SearchResult.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SearchResult : UserControl,IView.IViewSearchResult
    {
        public UC_SearchResult()
        {
            InitializeComponent();
        }

        public IList<DZService> SearchedService
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

        public event SelectService SelectService;
    }
}
