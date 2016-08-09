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
        BLL.BLLStaff bllStaff;
        BLL.IBLLServiceOrder bllOrder;
        // BLL.DZMembershipProvider bllDZM;
        public ChatService(BLL.BLLReceptionChat bllChat, BLL.IBLLServiceOrder bllOrder, BLL.BLLStaff bllStaff)
        {
            this.bllChat = bllChat;
            this.bllOrder = bllOrder;
            this.bllStaff = bllStaff;
            //this.bllDZM = bllDZM;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="customer"></param>
        Guid checkRute(string orderID, Customer customer)
        {
            if (string.IsNullOrEmpty(orderID))
            {
                throw new FormatException("orderID不能为空");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Model.ServiceOrder order = bllOrder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (customer.UserType == "business")
            {
                if (order.Business == null || order.Business.Owner.Id.ToString() != customer.UserID)
                {
                    throw new Exception("这不是你的订单！");
                }
            }
            if (customer.UserType == "staff")
            {
                Model.Staff staff = bllStaff.GetOneByUserID(Guid.Empty, customer.UserID);
                if (staff == null || order.Business.Id != staff.Id)
                {
                    throw new Exception("这不是你的订单！");
                }
            }
            return guidOrder;
        }

        /// <summary>
        /// 条件读取聊天记录
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="chatfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<chatObj> GetChats(string orderID, common_Trait_Filtering filter, common_Trait_ChatFiltering chatfilter,Customer customer)
        {
            Guid guidOrder = checkRute(orderID, customer);
            Guid guidCustomer = utils.CheckGuidID(customer.UserID, "customer.UserID");
            IList<Model.ReceptionChat> chat = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "ReceptionChat");
            chat = bllChat.GetChats(filter1, chatfilter.type, chatfilter.fromTarget, guidOrder, guidCustomer,customer.UserType);
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
        /// <param name="customer"></param>
        /// <returns></returns>
        public countObj GetChatsCount(string orderID, common_Trait_ChatFiltering chatfilter, Customer customer)
        {
            Guid guidOrder = checkRute(orderID, customer);
            Guid guidCustomer = utils.CheckGuidID(customer.UserID, "customer.UserID");
            countObj c = new countObj();
            c.count = bllChat.GetChatsCount(chatfilter.type, chatfilter.fromTarget, guidOrder, guidCustomer,customer.UserType).ToString();
            return c;
        }
    }
}
