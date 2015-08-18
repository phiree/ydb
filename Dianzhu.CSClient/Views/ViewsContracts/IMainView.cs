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
   public interface IMainView:IView<Models.MainModel>
    {
       event EventHandler OnMessage;
    }
}
