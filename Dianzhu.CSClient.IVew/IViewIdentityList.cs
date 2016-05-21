using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 接待列表
    /// </summary>
    public interface IViewIdentityList
    {
        event IdentityClick IdentityClick;
        //增加一个标志
        void AddIdentity(ServiceOrder  serviceOrder);
        //删除一个用户
        void RemoveIdentity(ServiceOrder serviceOrder);
        void UpdateIdentityBtnName(Guid oldOrder, Guid newOrder);
        //设置为未读
        void SetIdentityUnread(ServiceOrder serviceOrder, int messageAmount);
        //设置为已读
        void SetIdentityReaded(ServiceOrder serviceOrder);
        //
        void SetIdentityLoading(ServiceOrder serviceOrder);
        
    }
    /// <summary>
    /// 点击用户按钮的委托.
    /// </summary>
    /// <param name="customer"></param>
    public delegate void IdentityClick(ServiceOrder serviceOrder);
}
