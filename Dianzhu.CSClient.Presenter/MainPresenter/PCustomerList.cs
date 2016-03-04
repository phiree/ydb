using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 用户列表的控制.
    /// 功能:
    /// 1)根据接收的IM消息,增删用户列表,更改用户状态
    /// 2)点击用户时,修改自身状态,加载聊天列表,加载订单信息
    /// </summary>
    public class PCustomerList
    {
        IViewCustomerList iview;
        
        public PCustomerList(IViewCustomerList iview)
        {
            iview = this.iview;
        }
        DZMembership customer;
        public DZMembership CurrentCustomer
        {
            get { return customer; }
        }
        IList<DZMembership> customerList;
        public IList<DZMembership> CustomerList
        {
            get { return customerList; }
        }
        

    }
    
}
