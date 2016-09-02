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
    public interface IViewCustomer
    {
        /// <summary>
        /// 用户头像
        /// </summary>
        string AvatarImage { set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string CustomerName { set; }

        /// <summary>
        /// 当前接待状态
        /// </summary>
        enum_CustomerReceptionStatus CustomerReceptionStatus { set; }
        
        /// <summary>
        /// 点击事件
        /// </summary>
        event CustomerClick CustomerClick;

        /// <summary>
        /// 控件对应的订单
        /// </summary>
        ServiceOrder Order { get; set; }

        enum_CustomerReceptionStatus CustomerButtonStyle { set; }
    }

    public delegate void CustomerClick(ServiceOrder order);
}
