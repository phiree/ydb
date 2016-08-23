using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.IView
{
    public interface IViewMainForm
    {
        void ShowMessage(string message);
        void CloseApplication();
        bool? ShowDialog();
        string FormTitle { set; }
    }
}
