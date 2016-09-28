using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 显示一些系统通知
    /// </summary>
    public interface IViewTabControl
    {
        event SetSearchAddress SetSearchAddress;
    }

    public delegate void SetSearchAddress(string address);

}
