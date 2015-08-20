using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient
{
    public interface IView
    {
        List<string> ButtonList { get; }
        string ChatHistory { get; set; }
        string CurrentCustomerName { get; set; }
        /// <summary>
        /// 设置按钮的样式.
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="buttonStyle"></param>
        void SetButtonStyle(string buttonText, em_ButtonStyle buttonStyle);
        void AddButtonWithStyle(string buttonText, em_ButtonStyle buttonStyle);
        void SendMessage(string message);
        

    }
}
