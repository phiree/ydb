using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Ydb.BusinessResource.DomainModel;

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

        Dictionary<string, SearchObj> LocalSearchTempObj { get; }

        void InitUIData(string customerId);

        void SaveSearchObj(string customerId, SearchObj obj);
        void RemoveSearchObj(string customerId);
    }

    /// <summary>
    /// 聊天消息内存中实现
    /// </summary>
    public class UIDataManagerInMemory : LocalUIDataManager
    {
        public UIDataManagerInMemory()
        {
            LocalUIDatas = new Dictionary<string, CustomerUIData>();
            LocalSearchTempObj = new Dictionary<string, SearchObj>();
        }

        public void SaveSearchObj(string customerId, SearchObj obj)
        {
            //if (!LocalSearchTempObj.ContainsKey(customerId))
            //{
            //    LocalSearchTempObj.Add(customerId, obj);
            //}
            //else
            //{
            //    LocalSearchTempObj[customerId] = obj;
            //}
            LocalSearchTempObj[customerId] = obj;
        }

        public void Save(string customerId, string key, object value)
        {
            InitUIData(customerId);

            switch (key)
            {
                case "Name":
                    LocalUIDatas[customerId].Name = value.ToString().ToString();
                    break;
                case "Date":
                    LocalUIDatas[customerId].Date = (DateTime)value;
                    break;
                case "ServiceType":
                    LocalUIDatas[customerId].ServiceType = (ServiceType)value;
                    break;
                case "ServiceName":
                    LocalUIDatas[customerId].ServiceName = value.ToString();
                    break;
                case "PriceMin":
                    LocalUIDatas[customerId].PriceMin = (decimal)value;
                    break;
                case "PriceMax":
                    LocalUIDatas[customerId].PriceMax = (decimal)value;
                    break;
                case "Phone":
                    LocalUIDatas[customerId].Phone = value.ToString();
                    break;
                case "Amount":
                    LocalUIDatas[customerId].Amount = (int)value;
                    break;
                case "Address":
                    LocalUIDatas[customerId].Address = value.ToString();
                    break;
                case "Memo":
                    LocalUIDatas[customerId].Memo = value.ToString();
                    break;
                case "TargetAddressObj":
                    LocalUIDatas[customerId].TargetAddressObj = (TargetAddressObj)value;
                    break;
                case "MessageText":
                    LocalUIDatas[customerId].MessageText = value.ToString();
                    break;
                default: break;
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
                LocalUIDatas[customerId].MessageText = string.Empty;
            }
        }

        public void RemoveSearchObj(string customerId)
        {
            if (LocalSearchTempObj.ContainsKey(customerId))
            {
                LocalSearchTempObj.Remove(customerId);
            }
        }

        public Dictionary<string, CustomerUIData> LocalUIDatas
        {
            get;
        }

        public Dictionary<string, SearchObj> LocalSearchTempObj
        {
            get;
        }
    }

    public class CustomerUIData
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
      //todo:refactor 直接使用领域对象? 
        public ServiceType ServiceType { get; set; }
        public string ServiceName { get; set; }
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }
        public string Phone { get; set; }
        public int Amount { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public TargetAddressObj TargetAddressObj { get; set; }
        public string MessageText { get; set; }
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

    public class SearchObj
    {
        public string ServiceName { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public Guid ServiceTypeId { get; set; }
        public DateTime TargetTime { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
        public string Address { get; set; }
        public SearchObj(string serviceName, decimal minPrice, decimal maxPrice, Guid serviceTypeId, DateTime targetTime, double lng, double lat, string address)
        {
            ServiceName = serviceName;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            ServiceTypeId = serviceTypeId;
            TargetTime = targetTime;
            Lng = lng;
            Lat = lat;
            Address = address;
        }
    }
}
