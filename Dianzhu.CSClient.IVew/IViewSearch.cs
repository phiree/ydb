﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Dianzhu.CSClient.ViewModel;
using Ydb.BusinessResource.DomainModel;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 搜索界面
    /// </summary>
    public interface IViewSearch
    {
        DateTime SearchKeywordTime { get; set; }
        decimal ServiceTargetPriceMin { get; set; }
        decimal ServiceTargetPriceMax { get; set; }
        string ServiceCustomerName { get; set; }
      
        string ServiceName { get; set; }
        string ServiceCustomerPhone { get; set; }
        string ServiceTargetAddressStr { get; set; }
        string ServiceTargetAddress { get; set; }
        string ServiceMemo { get; set; }
        /// <summary>
        /// 搜索时的服务时间
        /// </summary>
        DateTime TargetTime { get; set; }
        int UnitAmount { get; set; }

        void ClearData();

        event SearchService Search;

        event SaveUIData SaveUIData;

      
        void InitType(IList<ServiceType> typeList);
        event ReloadServiceType ReloadServiecType;
    }
    public delegate void SearchService(DateTime targetTime,decimal minPrice,decimal maxPrice,Guid servieTypeId,string name,string lng,string lat);
 
    public delegate void SaveUIData( string key, object value);
    /// <summary>
    /// 搜索结果
    /// </summary>
    public interface IViewSearchResult
    {
        IList<VMShelfService> SearchedService { get; set; }
        event PushServices PushServices;
        string LoadingText { set; }
        void AddSearchItem(IViewShelfService service);
        //bool BtnPush { get; set; }
        event PushServiceTimerSend PushServiceTimerSend;
        event FilterByBusinessName FilterByBusinessName;
     
    }
    
    public delegate ServiceOrder PushServices(IList<Guid> pushedServices,out string errorMsg);
    public delegate void PushServiceTimerSend();
    public delegate void FilterByBusinessName(string businessName);
    public delegate void ReloadServiceType();

}
