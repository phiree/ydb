using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.Model;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 处理全局变量 和 事件
    /// 接收im消息后,设置当前订单
    /// </summary>
   public   class PGlobal
    {
        /// <summary>
        /// 当前激活的客户 和 订单
        /// </summary>
        static ServiceOrder currentIdentity;
        public static ServiceOrder CurrentIdentity
        {
            get { return currentIdentity; }
            set { currentIdentity = value; }
        }
   
        PIdentityList pCustomerList;
        PChatList pChatList;
        public PGlobal(InstantMessage iIM,PIdentityList pCustomerList,PChatList pChatList)
        {
            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            this.pCustomerList = pCustomerList;
            this.pChatList = pChatList;
             
        }
        private void IIM_IMReceivedMessage(Model.ReceptionChat chat)
        {
            if (currentIdentity.Customer == chat.From)
            { }

            pCustomerList.ReceivedMessage(chat);

        }
    }
}
