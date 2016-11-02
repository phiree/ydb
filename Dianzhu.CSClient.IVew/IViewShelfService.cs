using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 搜索结果
    /// </summary>
    public interface IViewShelfService
    {
        event PushShelfService PushShelfService;
    }

    public delegate void PushShelfService(Guid pushedServiceId);

}
