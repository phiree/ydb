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
    public partial class UC_Search : UserControl,IView.IViewSearch
    {
        public UC_Search()
        {
            InitializeComponent();
        }

        public string SearchKeyword
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

        public decimal SearchKeywordPriceMax
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

        public decimal SearchKeywordPriceMin
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

        public string SearchKeywordTime
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

        public string SearchKeywordType
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

        public IList<ServiceType> ServiceTypeFirst
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<ServiceType> ServiceTypeSecond
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<ServiceType> ServiceTypeThird
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        DateTime IViewSearch.SearchKeywordTime
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

        public event SearchService Search;
        public event ServiceTypeFirst_Select ServiceTypeFirst_Select;
        public event ServiceTypeSecond_Select ServiceTypeSecond_Select;
        public event ServiceTypeThird_Select ServiceTypeThird_Select;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}
