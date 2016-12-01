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
  public  class PToolsControl
    {
        IViewToolsControl viewToolsControl;
        IViewSearch viewSearch;

        string identity;
        public IViewToolsControl ViewToolsControl
        {
            get
            {
                return viewToolsControl;
            }
        }

        public PToolsControl(IViewToolsControl viewToolsControl, IViewSearch viewSearch,string identity)
        {
            this.viewToolsControl = viewToolsControl;
            this.viewSearch = viewSearch;
            this.identity = identity;

            viewToolsControl.SetSearchAddress += ViewTabControl_SetSearchAddress;
        }

        private void ViewTabControl_SetSearchAddress(string address)
        {
            viewSearch.ServiceTargetAddressStr = address;
        }
    }
}
