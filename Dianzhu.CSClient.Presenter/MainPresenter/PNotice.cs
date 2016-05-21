using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter
{
   public  class PNotice
    {
        IView.IViewNotice viewNotice;
        public PNotice(IView.IViewNotice viewNotice)
        {
            this.viewNotice = viewNotice;
        }
        public void ShowNotice(string noticeBody)
        {
            viewNotice.NoticeBody = noticeBody;
        }
    }
}
