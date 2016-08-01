using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.IView
{
    public interface IViewFormShowMessage
    {
        string Title { set; }
        string Message { set; }
        void ShowDialog();
    }
}
