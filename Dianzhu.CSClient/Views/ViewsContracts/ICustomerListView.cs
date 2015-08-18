using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormsMvp;
 
namespace Dianzhu.CSClient.Views.ViewsContracts
{
    /// <summary>
    /// 主界面的时间.
    ///
    /// </summary>
   public interface ICustomerListView:IView<Models.ChatModel>
    {
       event EventHandler ClickCustomer;
    }
}
