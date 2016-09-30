using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.ViewModel;

namespace Dianzhu.CSClient.LocalStorage
{    
    public interface LocalHistoryOrderManager
    {
        Dictionary<string,IList<VMOrderHistory>> LocalHistoryOrders { get; }

        void Add(string customerId, VMOrderHistory vmOrderHistory);
        void Remove(string customerId);
    }
    
    public class HistoryChatManagerInMemory : LocalHistoryOrderManager
    {
        public HistoryChatManagerInMemory()
        {
            LocalHistoryOrders = new Dictionary<string, IList<VMOrderHistory>>();
        }

        public Dictionary<string, IList<VMOrderHistory>> LocalHistoryOrders
        {
            get;
        }

        /// <summary>
        /// 增加一条新记录.如果不存在则获取初始化数据
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="vmOrderHistory"></param>
        public void Add(string customerId, VMOrderHistory vmOrderHistory)
        {
            if (LocalHistoryOrders.ContainsKey(customerId))
            {
                LocalHistoryOrders[customerId].Add(vmOrderHistory);
            }
            else
            {
                LocalHistoryOrders[customerId] = new List<VMOrderHistory>();
                LocalHistoryOrders[customerId].Add(vmOrderHistory);

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
    }

}
