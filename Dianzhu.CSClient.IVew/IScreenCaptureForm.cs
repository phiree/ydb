using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 截图界面的接口.
    /// </summary>
    public interface IScreenCaptureForm
    {
        /// <summary>
        /// 截图完成之后
        /// </summary>
        event MediaMessageSent Captured;
    }
}
