using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.DomainModel;
using Ydb.Order.DomainModel;

namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 当前界面需要维护的状态值.
    /// </summary>
    public partial class ClientState
    {
        //当前客服
        public static MemberDto customerService = GlobalViables.CurrentCustomerService;

        //当前激活客户
        public static MemberDto CurrentCustomer = null;        
        //当前接待的 客户列表
        public static IList<MemberDto> customerList = new List<MemberDto>();
        //在线的客户列表
        public static IList<MemberDto> customerOnlineList = new List<MemberDto>();

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

