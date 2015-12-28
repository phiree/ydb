using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.CSClient.IVew;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.CSClient.ViewModel;
using System.Collections;

namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 当前界面需要维护的状态值.
    /// </summary>
    public partial class MainPresenter
    {
        //当前k客服
        DZMembership customerService = GlobalViables.CurrentCustomerService;
        //当前激活客户
       // DZMembership customer = null;
        ServiceOrder CurrentServiceOrder;
        //当前接待的 客户列表
        IList<DZMembership> customerList = new List<DZMembership>();

        //当前界面内 的订单
        IList<ServiceOrder> OrderList = new List<ServiceOrder>();
        //每个订单 的接待记录
       // Dictionary<string, ReceptionBase> ReceptionList = new Dictionary<string, ReceptionBase>();
        //搜索列表
        Dictionary<string, IList> SearchResultForCustomer = new Dictionary<string, IList>();
         //当前客户的历史订单
       // Dictionary<string, IList<ServiceOrder>> CustomerOrderList = new Dictionary<string, IList<ServiceOrder>>();
       
       
    }

    

}

