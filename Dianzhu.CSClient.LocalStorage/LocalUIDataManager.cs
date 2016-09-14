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
    /// <summary>
    /// 聊天消息本地化存储
    /// </summary>
    public interface LocalUIDataManager
    {
        void Save(string customerId, string key, object value);
        void Remove(string customerId);

        Dictionary<string, CustomerUIData> LocalUIDatas { get; }

        void InitUIData(string customerId);
    }

    /// <summary>
    /// 聊天消息内存中实现
    /// </summary>
    public class UIDataManagerInMemory : LocalUIDataManager
    {
        public UIDataManagerInMemory()
        {
            LocalUIDatas = new Dictionary<string, CustomerUIData>();
        }

        public void Save(string customerId, string key, object value)
        {
            if (!LocalUIDatas.ContainsKey(customerId))
            {
                InitUIData(customerId);
            }

            switch (key)
            {
                case "Name":
                    LocalUIDatas[customerId].Name = (string)value;
                    break;
                case "Date":
                    LocalUIDatas[customerId].Date = (DateTime)value;
                    break;
                case "ServiceType":
                    LocalUIDatas[customerId].ServiceType = (ServiceType)value;
                    break;
                case "ServiceName":
                    LocalUIDatas[customerId].ServiceName = (string)value;
                    break;
                case "PriceMin":
                    LocalUIDatas[customerId].PriceMin = (decimal)value;
                    break;
                case "PriceMax":
                    LocalUIDatas[customerId].PriceMax = (decimal)value;
                    break;
                case "Phone":
                    LocalUIDatas[customerId].Phone = (string)value;
                    break;
                case "Amount":
                    LocalUIDatas[customerId].Amount = (int)value;
                    break;
                case "Address":
                    LocalUIDatas[customerId].Address = (string)value;
                    break;
                case "Memo":
                    LocalUIDatas[customerId].Memo = (string)value;
                    break;
                case "TargetAddressObj":
                    LocalUIDatas[customerId].TargetAddressObj = (TargetAddressObj)value;
                    break;
                default:break;
            }
        }

        public void Remove(string customerId)
        {
            if (LocalUIDatas.ContainsKey(customerId))
            {
                LocalUIDatas.Remove(customerId);
            }
        }

        public void InitUIData(string customerId)
        {
            if (!LocalUIDatas.ContainsKey(customerId))
            {
                CustomerUIData ui = new CustomerUIData();
                LocalUIDatas.Add(customerId, ui);
                LocalUIDatas[customerId].Name = string.Empty;
                LocalUIDatas[customerId].Date = DateTime.Now;
                LocalUIDatas[customerId].ServiceType = null;
                LocalUIDatas[customerId].ServiceName = string.Empty;
                LocalUIDatas[customerId].PriceMin = 1;
                LocalUIDatas[customerId].PriceMax = 200;
                LocalUIDatas[customerId].Phone = string.Empty;
                LocalUIDatas[customerId].Amount = 1;
                LocalUIDatas[customerId].Address = string.Empty;
                LocalUIDatas[customerId].Memo = string.Empty;
                LocalUIDatas[customerId].TargetAddressObj = null;
            }
        }

        public Dictionary<string, CustomerUIData> LocalUIDatas
        {
            get;
        }
    }

    public class CustomerUIData
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ServiceType ServiceType { get; set; }
        public string ServiceName { get; set; }
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }
        public string Phone { get; set; }
        public int Amount { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public TargetAddressObj TargetAddressObj { get; set; }
    }

    public class TargetAddressObj
    {
        public TargetAddressObjPoint point { get; set; }
        public TargetAddressObjAddress address { get; set; }
    }

    public class TargetAddressObjPoint
    {
        public string lng { get; set; }
        public string lat { get; set; }
    }

    public class TargetAddressObjAddress
    {
        public string streetNumber { get; set; }
        public string street { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string province { get; set; }
    }
}
