using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter
{
   public  class PMain
    {
        IView.IViewMainForm viewMainForm;
        public PMain(IView.IViewMainForm viewMainForm)
        {
            this.viewMainForm = viewMainForm;
        }

        public bool?  ShowDialog()
        {
          return  viewMainForm.ShowDialog();
        }
        public void ShowMessage(string message)
        {
            viewMainForm.ShowMessage(message);
        }
        public void CloseApplication()
        {
            viewMainForm.CloseApplication();
        }
    }
}
