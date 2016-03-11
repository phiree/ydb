using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 聊天信息列表
    /// </summary>
    public interface IViewSearch
    {
        string SearchKeyword { get; set; }
        event SearchService Search;
    }
    public interface IViewSearchResult
    {
        IList<DZService> SearchedService { get; set; }
        event SelectService SelectService;
    }
    public delegate void SearchService();
    public delegate void SelectService(DZService selectedService);

}
