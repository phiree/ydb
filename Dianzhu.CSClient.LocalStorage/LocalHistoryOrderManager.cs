using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;

namespace Dianzhu.CSClient.LocalStorage
{    
    public interface LocalHistoryOrderManager
    {
        Dictionary<string,IList<ServiceOrder>> LocalHistoryOrders { get; }

        void Add(string customerId, ServiceOrder order);
        void Remove(string customerId);

        IList<ServiceOrder> GetOrInitHistoryOrderList(Guid customerId);
    }
    
    public class HistoryChatManagerInMemory : LocalHistoryOrderManager
    {
        IBLLServiceOrder bllServiceOrder;
        public HistoryChatManagerInMemory(IBLLServiceOrder bllServiceOrder)
        {
            this.bllServiceOrder = bllServiceOrder;

            LocalHistoryOrders = new Dictionary<string, IList<ServiceOrder>>();
        }

        public Dictionary<string, IList<ServiceOrder>> LocalHistoryOrders
        {
            get;
        }

        /// <summary>
        /// 增加一条新记录.如果不存在则获取初始化数据
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="order"></param>
        public void Add(string customerId, ServiceOrder order)
        {
            if (LocalHistoryOrders.ContainsKey(customerId))
            {
                LocalHistoryOrders[customerId].Add(order);
            }
            else
            {
                //initdata 
                IList<ServiceOrder> initChatList = GetOrInitHistoryOrderList(Guid.Parse(customerId));
                //是否包含当前记录.  插件保存记录 和 xmpp接收 的顺序无法控制, 加此判断以保证列表中包含该消息.
                //if (initChatList.Where(x => x.Id == order.Id).Count() == 0)
                //{
                //    LocalHistoryOrders[customerId].Add(order);
                //}
            }
        }

        /// <summary>
        /// 清除用户相应的历史订单数据
        /// </summary>
        /// <param name="customerId"></param>
        public void Remove(string customerId)
        {
            if (LocalHistoryOrders.ContainsKey(customerId))
            {
                LocalHistoryOrders.Remove(customerId);
            }
        }

        /// <summary>
        /// 获取一个聊天列表,如果不存在则初始化.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetOrInitHistoryOrderList(Guid customerId)
        {
            string key = customerId.ToString();
            if (LocalHistoryOrders.ContainsKey(key))
            {
                return LocalHistoryOrders[key];
            }
            else
            {
                int rowCount;
                //initdata 
                IList<ServiceOrder> orderList = bllServiceOrder.GetListForCustomer(customerId, 1, 5, out rowCount);
                LocalHistoryOrders.Add(key, orderList);
            }
            return LocalHistoryOrders[key];
        }
    }

}
