using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormsMvp;
namespace Dianzhu.CSClient.Presenters
{
   public  class ChatPresenter:Presenter<Views.ViewsContracts.IChatView>
    {
       public ChatPresenter(Views.ViewsContracts.IChatView view)
           : base(view)
       {
       
       }
    }
}
