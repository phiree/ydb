using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.IView
{
    public interface IViewMainForm
    {
        void CloseApplication();
        bool? ShowDialog();

        string CSName { set; }

        /// <summary>
        /// 播放收到消息的提示音
        /// </summary>
        void PlayVoice();
        /// <summary>
        /// 任务栏闪烁
        /// </summary>
        void FlashTaskBar();

        void AddIdentityTab(string identityTabFriendly, IViewTabContent viewTabContent);

        void ShowIdentityTab(string identityTabFriendly);

        void RemoveIdentityTab(string identityTabFriendly);

        string Version { set; }
        event AddCustomerTest AddCustomerTest;
        
    }
    public delegate void AddCustomerTest();
}
