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

        string CSName { set; }

        void FlashTaskBar();//任务栏闪烁

        void AddIdentityTab(string identity, 
            IViewSearch viewSearch, IViewSearchResult viewSearchResult,
            IViewChatList viewChatList, IViewChatSend viewChatSend,
            IViewOrderHistory viewOrderHistory, IViewToolsControl viewTabControl);

        void RemoveIdentityTab(string identity);
    }
}
