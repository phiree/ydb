using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using System.Diagnostics;
using System.Collections.Concurrent;
using Ydb.Order.DomainModel;

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
        static object lockObj = new object();

        /// <summary>
        /// 当前接待用户的Id
        /// </summary>
        public static string CurrentCustomerId
        {
            get; private set;
        }

        public static string CurrentOrderId
        {
            get
            {
                if (customerList.Keys.Contains(CurrentCustomerId))
                    return customerList[CurrentCustomerId];
                else
                    return string.Empty;
            }
        }

        static ConcurrentDictionary<string, string> customerList = new ConcurrentDictionary<string, string>();
        /// <summary>
        /// 用户列表，key为用户的id，value为该用户当前的订单id
        /// </summary>
        public static ConcurrentDictionary<string,string> CustomerList
        {
            get
            {
                return customerList;
            }
        }

        /// <summary>
        /// 更新当前用户列表，有用户更新订单，没有用户新增用户
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static IdentityTypeOfOrder UpdateCustomerList(string customerId,string orderId)
        {
            lock (lockObj)
            {
                log.Debug("开始更新聊天标志的状态.用户id:" + customerId + ",订单id:" + orderId);
                IdentityTypeOfOrder orderType = IdentityTypeOfOrder.None;

                log.Debug("存在用户");
                if (customerList.Keys.Contains(customerId))
                {
                    if (customerId == CurrentCustomerId)
                    {
                        log.Debug("当前接待的用户");
                        orderType = IdentityTypeOfOrder.CurrentCustomer;
                    }
                    else
                    {
                        log.Debug("列表中存在的用户");
                        orderType = IdentityTypeOfOrder.InList;
                    }

                    if (customerList[customerId] != orderId)
                    {
                        log.Debug("订单不同，更新用户的当前订单id");
                        customerList[customerId] = orderId;
                    }
                }
                else
                {
                    log.Debug("新用户");
                    if (!customerList.TryAdd(customerId, orderId))
                    {
                        log.Warn("新增用户失败");
                    }
                    else
                    {
                        orderType = IdentityTypeOfOrder.NewIdentity;
                    }
                }


                return orderType;
            }
        }

        public static bool DeleteCustomer(string customerId)
        {
            if (customerList.Keys.Contains(customerId))
            {
                string orderId;
                log.Debug("删除用户");
                if(customerList.TryRemove(customerId,out orderId))
                {
                    log.Debug("用户删除成功");
                    return true;
                }
                else
                {
                    log.Warn("用户删除失败");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置当前接待用户的Id
        /// </summary>
        /// <param name="cusomterId"></param>
        public static void SetCurrentCustomerId (string cusomterId)
        {
            CurrentCustomerId = cusomterId;
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                lock (lockObj)
                {
                    if (currentIdentityList.Keys.Select(x => x.Id).Contains(value.Id))
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
            lock (lockObj)
            {
                log.Debug("1开始更新聊天标志的状态.订单id:" + order.Id + ",用户id:" + order.CustomerId);
                type = IdentityTypeOfOrder.None;
                var existedCustomer = currentIdentityList.Where(x => x.Key.CustomerId == order.CustomerId);

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
                    else
                    {
                        log.Debug("1.1.2订单不一样");
                        //DictExtension.RenameKey(currentIdentityList, existedOrder.Key, order);

                        foreach (var key in currentIdentityList.Keys.ToList())
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

                        log.Debug("用户名id：" + order.CustomerId);
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

                    bool newIdentityAdded = currentIdentityList.TryAdd(order, false);
                    if (!newIdentityAdded)
                    {
                        log.Warn("新用户插入失败");
                    }
                }
                else
                {
                    log.Error("当前列表中存在重复项");
                    foreach(var item in existedCustomer)
                    {
                        log.Debug("orderId:" + item.Key);
                        log.Debug("isTrueOrFalse:" + item.Value);
                    }
                }
                log.Debug("更新完成");
            }
            

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
