using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.Model;
using System.Diagnostics;
using System.Collections.Concurrent;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    ///  
    ///  管理通讯列表
    ///  
    /// </summary>
    public class IdentityManager
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Presenter.IdentityManager");
        #region propertys
        /// <summary>
        /// 当前激活的通讯
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
                   log.Error("当前标志项大于1:" + identies.Count());
                    return identies.First().Key;
                }
            }
            set
            {
                if (currentIdentityList.Keys.Select(x=>x.Id).Contains(value.Id))
                {
                    foreach (var key in currentIdentityList.Keys.ToList())
                    {
                        if (key.Id == value.Id)
                        {
                            currentIdentityList[key] = true;
                        }
                        else
                        {
                            currentIdentityList[key] = false;
                        }
                    }
                }
                else
                {
                    foreach (var key in currentIdentityList.Keys.ToList())
                    {
                        currentIdentityList[key] = false;
                    }

                    currentIdentityList[value] = true;
                }
            }
        }
        static ConcurrentDictionary<ServiceOrder, bool> currentIdentityList = new ConcurrentDictionary<ServiceOrder, bool>();
        /// <summary>
        /// 通讯列表, bool 表示 是否是激活.
        /// </summary>
        public static ConcurrentDictionary<ServiceOrder, bool> CurrentIdentityList
        {
            get
            {
                return currentIdentityList;// currentIdentityList.ToDictionary()
            }

        }

        #endregion

        #region  contructors
      
        public IdentityManager()
        { 
            InitLoadIdentityList();

        }
        /// <summary>
        ///todo： 加载保存过的历史列表
        /// </summary>
        private void InitLoadIdentityList()
        {

        }
        #endregion

        public static bool DeleteIdentity(ServiceOrder order)
        {
            bool isExactive = false;
            foreach(var key in currentIdentityList.Keys)
            {
                if (key.Id == order.Id)
                {
                    bool value;
                    isExactive = currentIdentityList.TryRemove(key, out value);
                    break;
                }
            }
            return isExactive;
        }

        /// <summary>
        /// 当前订单的类型.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>        
        public static void UpdateIdentityList(ServiceOrder order, out IdentityTypeOfOrder type)
        {
            log.Debug("1开始更新聊天标志的状态.订单:"+order.Id+",用户:"+order.Customer.DisplayName);
            type = IdentityTypeOfOrder.None;
            var existedCustomer = currentIdentityList.Where(x => x.Key.Customer.Id == order.Customer.Id);
           
            if (existedCustomer.Count() == 1)
            {
                log.Debug("1.1存在用户");
                var existedOrder = existedCustomer.First();
                if (existedOrder.Key.Id == order.Id)
                {
                    log.Debug("1.1.1订单一样:" + order.Id);
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
                    log.Debug("1.1.2订单不一样");
                    //DictExtension.RenameKey(currentIdentityList, existedOrder.Key, order);

                    foreach(var key in currentIdentityList.Keys.ToList())
                    {
                        bool isRemove = false;
                        if (key.Id == existedOrder.Key.Id)
                        {
                            currentIdentityList.TryRemove(key, out isRemove);
                        }
                        if (isRemove)
                        {
                            break;
                        }
                    }
                    currentIdentityList[order] = true; 

                    log.Debug("用户名：" + order.Customer.DisplayName);
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
                log.Debug("1.2 新用户");
                type = IdentityTypeOfOrder.NewIdentity;
                
                bool newIdentityAdded= currentIdentityList.TryAdd(order,false);
                if (!newIdentityAdded)
                {
                    log.Warn("新用户插入失败");
                }
            }
            else
            {
                log.Error("当前列表中存在重复项");
                 
            }
            log.Debug("更新完成");

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
