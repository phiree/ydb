using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dianzhu.Diandian
{
    public partial class FmOnlineUsers : Form
    {
        public FmOnlineUsers(object[] users)
        {
            InitializeComponent();
            listBox1.Items.AddRange(users);
        }
    }
}
