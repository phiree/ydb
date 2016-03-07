using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 客户列表的界面接口
    /// </summary>
    public interface IViewCustomerList
    {
        event CustomerClick CustomerClick;
        //增加一个用户
        void AddCustomer(DZMembership customer);
        //删除一个用户
        void RemoveCustomer(DZMembership customer);
        //设置为未读
        void SetCustomerUnread(DZMembership customer,int messageAmount);
        //设置为已读
        void SetCustomerReaded(DZMembership customer);
        
    }
    /// <summary>
    /// 点击用户按钮的委托.
    /// </summary>
    /// <param name="customer"></param>
    public delegate void CustomerClick(DZMembership customer);
}
