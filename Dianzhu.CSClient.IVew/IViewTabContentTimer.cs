using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 各个content对应的时间控制器接口
    /// 用来管理客服回复用户后的时间
    /// 客服发送消息(文本，多媒体，订单)后开始计时
    /// 客服收到消息(文本，多媒体，订单)后台停止计时
    /// </summary>
    public interface IViewTabContentTimer
    {
        void InitTimer();
        void StartTimer();
        void StopTimer();

        string Identity { get; set; }

        event TimeOver TimeOver;
    }

    public delegate void TimeOver(string identity);
}
