using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Chat
{
    public class ChatService: IChatService
    {
        BLL.BLLReceptionChat bllChat;
        //BLL.IBLLServiceOrder bllOrder;
       // BLL.DZMembershipProvider bllDZM;
        public ChatService(BLL.BLLReceptionChat bllChat)
        {
            this.bllChat = bllChat;
            //this.bllOrder = bllOrder;
            //this.bllDZM = bllDZM;
        }

        /// <summary>
        /// 条件读取聊天记录
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="chatfilter"></param>
        /// <returns></returns>
        public IList<chatObj> GetChats(string orderID, common_Trait_Filtering filter, common_Trait_ChatFiltering chatfilter)
        {
            IList<Model.ReceptionChat> chat = null;
            int[] page = utils.CheckFilter(filter);
            chat = bllChat.GetChats(page[0], page[1], chatfilter.type, chatfilter.fromTarget, utils.CheckGuidID(orderID, "orderID"));
            if (chat == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<chatObj> staffobj = Mapper.Map<IList<Model.ReceptionChat>, IList<chatObj>>(chat);
            return staffobj;
        }

        /// <summary>
        /// 统计聊天信息的数量
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="chatfilter"></param>
        /// <returns></returns>
        public countObj GetChatsCount(string orderID, common_Trait_ChatFiltering chatfilter)
        {
            countObj c = new countObj();
            c.count = bllChat.GetChatsCount(chatfilter.type, chatfilter.fromTarget, utils.CheckGuidID(orderID, "orderID")).ToString();
            return c;
        }
    }
}
