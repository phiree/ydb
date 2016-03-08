using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.BLL;
using Dianzhu.Model.Enums;
using Dianzhu.DAL;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 用户列表的控制.
    /// 功能:
    /// 1)根据接收的IM消息,增删用户列表,更改用户状态
    /// 2)点击用户时,修改自身状态,加载聊天列表,加载订单信息
    /// </summary>
    public class PIdentityList
    {
        IViewIdentityList iView;
        IViewChatList iViewChatList;



        public PIdentityList(IViewIdentityList iView, IViewChatList iViewChatList)
        {
            this.iView = iView;

            this.iViewChatList = iViewChatList;

            iView.IdentityClick += IView_IdentityClick;
            
        }

        private void IView_IdentityClick(ServiceOrder serviceOrder)
        {
            PGlobal.CurrentIdentity = serviceOrder;
            iView.SetIdentityReaded(serviceOrder);
            
        }



        //负责接收消息
        public void ReceivedMessage(ReceptionChat chat)
        {
            //
            if (PGlobal.CurrentIdentity == null)
            {
                PGlobal.CurrentIdentity = chat.ServiceOrder;
                PGlobal.CurrentIdentityList.Add(chat.ServiceOrder);
            }
            //和当前订单对比
            else if (PGlobal.CurrentIdentity == chat.ServiceOrder)
            {
                iViewChatList.AddOneChat(chat);
                
            }
            //当前订单的用户等于来源订单


            else if (PGlobal.CurrentIdentity.Customer == chat.From)
            {
                PGlobal.CurrentIdentityList.Remove(PGlobal.CurrentIdentity);
                PGlobal.CurrentIdentity = chat.ServiceOrder;
                PGlobal.CurrentIdentityList.Add(chat.ServiceOrder);
                iViewChatList.AddOneChat(chat);

            }
            else {
            bool hasIdentity = PGlobal.CurrentIdentityList.Contains(chat.ServiceOrder);
                if (hasIdentity)
                {
                    iView.SetIdentityUnread(chat.ServiceOrder, 1);

                }
                else { 
            //和列表里的用户相比
            bool hasCustomer = PGlobal.CurrentIdentityList.Select(x => x.Customer).Contains(chat.From);
                    if (hasCustomer)
                    {
                        ServiceOrder oldIdentity = PGlobal.CurrentIdentityList.Single(x => x.Customer == chat.From);
                        PGlobal.CurrentIdentityList.Remove(oldIdentity);
                        PGlobal.CurrentIdentityList.Add(chat.ServiceOrder);
                        iView.SetIdentityUnread(chat.ServiceOrder, 1);

                    }
                    else
                    {
                        PGlobal.CurrentIdentityList.Add(chat.ServiceOrder);
                        AddIdentity(chat.ServiceOrder);
                    }
                }
                //新订单，新用户
               

            }
        }
 
        public void AddIdentity(ServiceOrder order)
        {
 
             iView.AddIdentity(order);
            iView.SetIdentityUnread(order, 1);
        }


    }

}
