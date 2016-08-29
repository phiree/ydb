using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;
using System.Diagnostics;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// order的控制类
    /// </summary>
  public  class PTabControl
    {
        IViewTabControl viewTabControl;
        IViewSearch viewSearch;
        public PTabControl(IViewTabControl viewTabControl, IViewSearch viewSearch)
        {
            this.viewTabControl = viewTabControl;
            this.viewSearch = viewSearch;

            viewTabControl.SetSearchAddress += ViewTabControl_SetSearchAddress;
        }

        private void ViewTabControl_SetSearchAddress(string address)
        {
            viewSearch.ServiceTargetAddress = address;
        }
    }
}
