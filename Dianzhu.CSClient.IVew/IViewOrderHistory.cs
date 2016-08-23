using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 历史订单界面
    /// </summary>
    public interface IViewOrderHistory
    {
        IList<ServiceOrder> OrderList { get; set; }
        string SearchStr { get; set; }
        int OrderPage { get; set; }

        event SearchOrderHistoryClick SearchOrderHistoryClick;
        void ShowNullListLable();
        void ShowListLoadingMsg();
        void ShowMoreOrderList();
        void ShowNoMoreOrderList();

        void AddOneOrder(ServiceOrder order);
        void InsertOneOrder(ServiceOrder order);

        void ClearUCData();

        event BtnMoreChat BtnMoreOrder;
    }

    /// <summary>
    /// 点击搜索按钮的委托
    /// </summary>
    /// <param name="searchStr"></param>
    public delegate void SearchOrderHistoryClick();

    public delegate void BtnMoreOrder();

}
