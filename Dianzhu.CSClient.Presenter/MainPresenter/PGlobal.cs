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
    public class PGlobal
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
        static IList<ServiceOrder> currentIdentityList = new List<ServiceOrder>();
        public static IList<ServiceOrder> CurrentIdentityList
        {
            get
            {
                return currentIdentityList;
            }

        }
        PIdentityList pIdentityList;
        PChatList pChatList;
        public PGlobal(InstantMessage iIM, PIdentityList pIdentityList, PChatList pChatList)
        {
            iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            this.pIdentityList = pIdentityList;
            this.pChatList = pChatList;
            //todo: 可以从历史记录
            InitLoadIdentityList();

        }
        /// <summary>
        ///todo： 加载保存过的历史列表
        /// </summary>
        private void InitLoadIdentityList()
        {
            currentIdentityList = new List<ServiceOrder>();
        }
        private void IIM_IMReceivedMessage(Model.ReceptionChat chat)
        {
            //判断信息类型
            if (chat.ChatType == Model.Enums.enum_ChatType.Notice
                || chat.ChatType == Model.Enums.enum_ChatType.ReAssign)
            {


            }
            else
            {

                pIdentityList.ReceivedMessage(chat);
            }



        }
    }
}
