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
        DateTime SearchKeywordTime { get; set; }
        decimal SearchKeywordPriceMin { get; set; }
        decimal SearchKeywordPriceMax { get; set; }
        string ServiceAddress { get; set; }
        int UnitAmount { get; set; }

        void ClearData();

        event SearchService Search;
        #region 服务类型相关属性及委托
        IList<ServiceType> ServiceTypeFirst { set; }
        IList<ServiceType> ServiceTypeSecond { set; }
        IList<ServiceType> ServiceTypeThird { set; }
        event ServiceTypeFirst_Select ServiceTypeFirst_Select;
        event ServiceTypeSecond_Select ServiceTypeSecond_Select;
        event ServiceTypeThird_Select ServiceTypeThird_Select;
        #endregion
    }
    public delegate void SearchService(DateTime targetTime,decimal minPrice,decimal maxPrice,Guid servieTypeId,string name);
    public delegate void ServiceTypeFirst_Select(ServiceType type);
    public delegate void ServiceTypeSecond_Select(ServiceType type);
    public delegate void ServiceTypeThird_Select(ServiceType type);
    /// <summary>
    /// 搜索结果
    /// </summary>
    public interface IViewSearchResult
    {
        IList<DZService> SearchedService { get; set; }
        event SelectService SelectService;
        event PushServices PushServices;
        string LoadingText { set; }
        void AddSearchItem(IViewShelfService service);
        //bool BtnPush { get; set; }
        event PushServiceTimerSend PushServiceTimerSend;
        event FilterByBusinessName FilterByBusinessName;
    }
   
    public delegate void SelectService(DZService selectedService);
    public delegate ReceptionChat PushServices(IList<DZService> pushedServices);
    public delegate void PushServiceTimerSend();
    public delegate void FilterByBusinessName(string businessName);

}
