using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dianzhu.CSClient.WinformView
{
    public partial class FormMain2 : Form
    {
        private UC_ChatList uc_ChatList;
        private UC_IdentityList uc_CustomerList;
        public FormMain2(UC_ChatList uc_ChatList,UC_IdentityList uc_CustomerList,UC_Order uc_Order,UC_Search uc_Search,UC_SearchResult uc_SearchResult)
        {
            InitializeComponent();





            this.uc_ChatList = uc_ChatList;
            this.uc_CustomerList = uc_CustomerList;


            this.uc_CustomerList.Dock = DockStyle.Fill;
            this.splitContainer1.Panel1.Controls.Add(uc_CustomerList);


            this.uc_ChatList.Dock = DockStyle.Fill;
            this.splitContainer3.Panel1.Controls.Add(uc_ChatList);

            uc_Order.Dock = DockStyle.Fill;
            this.splitContainer2.Panel1.Controls.Add(uc_Order);

            uc_Search.Dock = DockStyle.Top;
            this.splitContainer3.Panel2.Controls.Add(uc_Search);

            uc_SearchResult.Dock = DockStyle.Top;
            this.splitContainer3.Panel2.Controls.Add(uc_SearchResult);

        }

        
    }
}
