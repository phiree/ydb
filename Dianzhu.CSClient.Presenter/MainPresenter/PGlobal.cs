using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.Model;
using System.Diagnostics;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 处理全局变量 和 事件
    /// 接收im消息后,设置当前订单
    /// </summary>
    public class PGlobal
    {
        #region propertys
        /// <summary>
        /// 当前激活的聊天项.
        /// </summary>

        public static ServiceOrder CurrentIdentity
        {
            get
            {
                IEnumerable<KeyValuePair<ServiceOrder, bool>> identies = currentIdentityList.Where(x => x.Value == true);
                if (identies.Count() == 0)
                {
                    return null;
                }
                else if (identies.Count() == 1)
                {
                    return identies.First().Key;
                }
                else
                {
                    throw new Exception("当前标志项大于1:" + identies.Count());
                }
            }
            set
            {

                foreach (var key in currentIdentityList.Keys.ToList())
                {
                    currentIdentityList[key] = false;
                }

                currentIdentityList[value] = true;

            }
        }
        static Dictionary<ServiceOrder, bool> currentIdentityList = new Dictionary<ServiceOrder, bool>();
        
        /// <summary>
        /// 聊天列表
        /// </summary>
        public static Dictionary<ServiceOrder, bool> CurrentIdentityList
        {
            get
            {
                return currentIdentityList;
            }

        }

        public static DZMembership CurrentCustomerService { get; set; }
        #endregion

        #region  contructors
      
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

        }
        #endregion
        private void IIM_IMReceivedMessage(Model.ReceptionChat chat)
        {
            //判断信息类型
            if (chat.ChatType == Model.Enums.enum_ChatType.Notice
                || chat.ChatType == Model.Enums.enum_ChatType.ReAssign)
            {

                //todo ,显示非聊天消息
            }
            else
            {
                //1 更新当前聊天列表
                //2 判断消息 和 聊天列表,当前聊天项的关系(是当前聊天项 但是需要修改订单 非激活的列表, 新聊天.
                IdentityTypeOfOrder type;
                UpdateIdentityList(chat.ServiceOrder, out type);
                pIdentityList.ReceivedMessage(chat, type);
            }
        }

        /// <summary>
        /// 当前订单的类型.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public void UpdateIdentityList(ServiceOrder order, out IdentityTypeOfOrder type)
        {

            type = IdentityTypeOfOrder.None;
            var existedCustomer = currentIdentityList.Where(x => x.Key.Customer == order.Customer);

            if (existedCustomer.Count() == 1)
            {
                var existedOrder = existedCustomer.First();
                if (existedOrder.Key == order)
                {
                    if (existedOrder.Value == true)
                    {
                        type = IdentityTypeOfOrder.CurrentIdentity;
                    }
                    else
                    {
                        type = IdentityTypeOfOrder.InList;
                    }

                }
                else {
                    DictExtension.RenameKey(currentIdentityList, existedOrder.Key, order);
                    if (existedOrder.Value == true)
                    {
                        type = IdentityTypeOfOrder.CurrentCustomer;
                    }
                    else
                    {
                        type = IdentityTypeOfOrder.InList;
                    }
                }
            }
            else if (existedCustomer.Count() == 0)
            {
                type = IdentityTypeOfOrder.NewIdentity;
                currentIdentityList.Add(order, false);
            }
            else
            {
                throw new Exception("当前列表中存在重复项");
            }
        }
       
    }
    public static class DictExtension
    {
        public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic,
                                     TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }
    }
    public enum IdentityTypeOfOrder
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 当前订单
        /// </summary>
        CurrentIdentity,
        /// <summary>
        /// 当前客户
        /// </summary>
        CurrentCustomer,
        /// <summary>
        /// 客户列表中的客户
        /// </summary>
        InList,
        /// <summary>
        /// 新客户
        /// </summary>
        NewIdentity

    }
}
