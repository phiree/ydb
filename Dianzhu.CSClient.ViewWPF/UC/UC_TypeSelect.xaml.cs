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
using Ydb.BusinessResource.DomainModel;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// UC_TypeSelect.xaml 的交互逻辑
    /// 
    /// todo:提炼成独立的级联控件.
    /// </summary>
    public partial class UC_TypeSelect : UserControl,IView.IViewTypeSelect
    {
        public UC_TypeSelect()
        {
            InitializeComponent();
        }
        Guid selectedTypeId;
        public Guid SelectedTypeId
        {
            get
            {
                return selectedTypeId;
            }
 
        }

        IList<ServiceType> typeList;
      
        public void Init(IList<ServiceType> typeList)
        {
            this.typeList = typeList;
            var source = typeList.Where(x => x.Parent == null).ToList();
            cbxSearchTypeF.ItemsSource = source;
            cbxSearchTypeF.SelectedItem = source[0];
        }

        private void cbxSearchTypeF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selected = cbxSearchTypeF.SelectedItem;
          
            ServiceType type = (ServiceType)selected;
            var children = type.Children;
            if (children == null || children.Count == 0)
            {
                selectedTypeId = type.Id;
                cbxSearchTypeT.Visibility = Visibility.Collapsed;
                return;
            }
            
            cbxSearchTypeS.ItemsSource = children;
            cbxSearchTypeS.SelectedItem = children[0];

        }
        private void cbxSearchTypeS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selected = cbxSearchTypeS.SelectedItem;

            if (selected == null)
            { return; }
            ServiceType type = (ServiceType)selected;
            var children = type.Children;
            if (children == null || children.Count == 0)
            {
                selectedTypeId = type.Id;
                cbxSearchTypeT.Visibility = Visibility.Collapsed;
                return;
            }
            cbxSearchTypeT.Visibility = Visibility.Visible;
            cbxSearchTypeT.ItemsSource = children;
            cbxSearchTypeT.SelectedItem = children[0];
        }
        private void cbxSearchTypeT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selected = cbxSearchTypeT.SelectedItem;
            if (selected == null)
            { return; }
            selectedTypeId = ((ServiceType)selected).Id;
        }
    }
}
