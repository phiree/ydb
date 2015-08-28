using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Dianzhu.Model;
namespace Dianzhu.CSClient.IVew
{
    public delegate void ActiveCustomerHandler(string customername);
    public delegate void SendMessageHandler();
    public interface MainFormView
    {
        #region Chat
        IList<ReceptionChat> ChatLog { set; get; }
        void LoadOneChat( ReceptionChat chat);
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
        event SendMessageHandler SendMessageHandler;
        event ActiveCustomerHandler ActiveCustomerHandler;
        #endregion

        string SerachKeyword { get; set; }
        string MessageTextBox { get; set; }


        IList<DZService> SearchedService { get; set; }
    }
}
