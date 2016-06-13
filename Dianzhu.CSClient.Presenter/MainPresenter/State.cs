using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.IInstantMessage;
 
using System.Collections;

namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 当前界面需要维护的状态值.
    /// </summary>
    public partial class ClientState
    {
        //当前客服
        public static DZMembership customerService = GlobalViables.CurrentCustomerService;

        //当前激活客户
        public static DZMembership CurrentCustomer = null;        
        //当前接待的 客户列表
        public static IList<DZMembership> customerList = new List<DZMembership>();
        //在线的客户列表
        public static IList<DZMembership> customerOnlineList = new List<DZMembership>();

        //当前界面内的订单
        public static ServiceOrder CurrentServiceOrder;
        //当前界面内的订单列表
        public static IList<ServiceOrder> OrderList = new List<ServiceOrder>();

        //每个订单 的接待记录
        // Dictionary<string, ReceptionBase> ReceptionList = new Dictionary<string, ReceptionBase>();
        //搜索列表
        public static Dictionary<string, IList<DZService>> SearchResultForCustomer = new Dictionary<string, IList<DZService>>();
         //当前客户的历史订单
       // Dictionary<string, IList<ServiceOrder>> CustomerOrderList = new Dictionary<string, IList<ServiceOrder>>();
       
       
    }

    

}

