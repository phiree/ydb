using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 搜索结果
    /// </summary>
    public interface IViewShelfService
    {
        event PushShelfService PushShelfService;
    }

    public delegate void PushShelfService(DZService pushedService);

}
