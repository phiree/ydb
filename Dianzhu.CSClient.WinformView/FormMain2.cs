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
        public FormMain2(UC_ChatList uc_ChatList,UC_IdentityList uc_CustomerList)
        {
            InitializeComponent();
            this.uc_ChatList = uc_ChatList;
            this.uc_CustomerList = uc_CustomerList;


            this.uc_CustomerList.Dock = DockStyle.Fill;
            this.splitContainer1.Panel1.Controls.Add(uc_CustomerList);


            this.uc_ChatList.Dock = DockStyle.Fill;
            this.splitContainer3.Panel1.Controls.Add(uc_ChatList);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            uc_ChatList.AddOneChat(DateTime.Now.ToString());
        }
    }
}
