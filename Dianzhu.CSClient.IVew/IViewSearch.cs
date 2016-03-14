using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 搜索界面
    /// </summary>
    public interface IViewSearch
    {
        string SearchKeyword { get; set; }
        event SearchService Search;
    }
    public delegate void SearchService();
    /// <summary>
    /// 搜索结果
    /// </summary>
    public interface IViewSearchResult
    {
        IList<DZService> SearchedService { get; set; }
        event SelectService SelectService;
    }
   
    public delegate void SelectService(DZService selectedService);

}
