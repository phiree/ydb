using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dianzhu.DemoClient
{
    public partial class AdvList : Form
    {
        public AdvList()
        {
            InitializeComponent();
        }
        private void LoadAdvList()
        {
            BLL.BLLAdvertisement bll;
            long totalRecords;
            dataGridView1.DataSource = bll.GetADList(0, 10, out totalRecords);
        }
    }
}
