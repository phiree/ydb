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

namespace Dianzhu.CSClient.WinformView
{
    public partial class UC_Search : UserControl,IView.IViewSearch
    {
        public UC_Search()
        {
            InitializeComponent();
        }

        public string SerachKeyword
        {
            get
            {
                return tbxKeywords.Text;
            }

            set
            {
                tbxKeywords.Text = value;
            }
        }

        public event SearchService Search;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}
