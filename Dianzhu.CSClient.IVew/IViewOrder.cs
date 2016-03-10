using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 订单界面
    /// </summary>
    public interface IViewOrder
    {


        ServiceOrder Order {set; }
        decimal DepositAmount { get; set; }
        string Memo { get; set; }
        event CreateOrderClick CreateOrderClick;
        
        
    }
    public delegate void CreateOrderClick();
    
}
