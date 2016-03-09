using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IInstantMessage;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 处理和通讯相关的事情
    /// </summary>
    public class InstantMessageHandler
    {
        InstantMessage iIM;
        IdentityManager identityManager;
        PIdentityList pIdentityList;
        public InstantMessageHandler(InstantMessage iIM,IdentityManager identityManager,PIdentityList pIdentityList)
        {
            this.iIM = iIM;
            this.iIM.IMReceivedMessage += IIM_IMReceivedMessage;
            this.identityManager = identityManager;
            this.pIdentityList = pIdentityList;

        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="chat"></param>
        private void IIM_IMReceivedMessage(Model.ReceptionChat chat)
        {
            //判断信息类型
            if (chat.ChatType == Model.Enums.enum_ChatType.Notice
                || chat.ChatType == Model.Enums.enum_ChatType.ReAssign
                  || chat.ChatType == Model.Enums.enum_ChatType.UserStatus)
            {
                //todo ,显示非聊天消息
            }
            else
            {

                //1 更新当前聊天列表
                //2 判断消息 和 聊天列表,当前聊天项的关系(是当前聊天项 但是需要修改订单 非激活的列表, 新聊天.
                IdentityTypeOfOrder type;
                identityManager.UpdateIdentityList(chat.ServiceOrder, out type);
                pIdentityList.ReceivedMessage(chat, type);
            }
        }
       
    }
}
