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

            set
            {
                selectedTypeId = value;
            }
        }

        IList<ServiceType> typeList;
        private object selectedF;

        public void Init(IList<ServiceType> typeList)
        {
            this.typeList = typeList;
            var source = typeList.Where(x => x.Parent == null);
            cbxSearchTypeF.ItemsSource = source.ToList();
            cbxSearchTypeF.SelectedIndex = 0;
        }

        private void cbxSearchTypeF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selected = cbxSearchTypeF.SelectedItem;
            if (selected == null)
            {   return;}

            ServiceType type = (ServiceType)selected;
            cbxSearchTypeS.ItemsSource = type.Children;
 
        }
        private void cbxSearchTypeS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selected = cbxSearchTypeF.SelectedItem;
            if (selected == null)
            { return; }

            ServiceType type = (ServiceType)selectedF;
            cbxSearchTypeT.ItemsSource = type.Children;
        }
        private void cbxSearchTypeT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selected = cbxSearchTypeF.SelectedItem;
            if (selected == null)
            { return; }
            selectedTypeId = ((ServiceType)selected).Id;
        }
    }
}
