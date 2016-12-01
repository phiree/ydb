using Dianzhu.CSClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 接待列表
    /// </summary>
    public interface IViewIdentityList
    {
        event IdentityClick IdentityClick;
        /// <summary>
        /// 增加一个标志
        /// </summary>
        /// <param name="vmIdentity"></param>
        void AddIdentity(VMIdentity vmIdentity);
        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="customerId"></param>
        void RemoveIdentity(string customerId);
        void UpdateIdentityBtnName(string customerId, VMIdentity vmIdentity);
        /// <summary>
        /// 设置为未读
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="messageAmount"></param>
        void SetIdentityUnread(string customerId, int messageAmount);
        /// <summary>
        /// 设置为已读
        /// </summary>
        /// <param name="customerId"></param>
        void SetIdentityReaded(string customerId);
    }
    /// <summary>
    /// 点击用户按钮的委托.
    /// </summary>
    /// <param name="customer"></param>
    public delegate void IdentityClick(string identityId);
}
