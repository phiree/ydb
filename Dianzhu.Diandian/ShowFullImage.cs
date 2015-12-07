using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dianzhu.Diandian
{
    public partial class ShowFullImage : Form
    {
        public ShowFullImage(Image image)
        {
            InitializeComponent();
            pb.Image = image;
            this.Size = image.Size;
        }
    }
}
