using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.CSClient
{
    public interface IView
    {
        
        #region Chat
        string ChatHistory { get; set; }
        string CurrentCustomerName { get; set; }

        /// <summary>
        /// 搜索关键字.
        /// </summary>
       
         
        /// <summary>
        /// 设置按钮的样式.
        /// </summary>
        /// <param name="buttonText">按钮文本(等同于客户登录名)</param>
        /// <param name="buttonStyle">按钮样式</param>
        void SetCustomerButtonStyle(string buttonText, em_ButtonStyle buttonStyle);
        /// <summary>
        /// 增加一个客户按钮,并设置样式
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="buttonStyle"></param>
        void AddCustomerButtonWithStyle(string buttonText, em_ButtonStyle buttonStyle);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        void SendMessage(string message);
        #endregion

         string SerachKeyword { get; set; }
         void LoadSearchHistory(IList<DZService> services);
        

    }
}
