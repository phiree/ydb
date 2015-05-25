using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    public class BLLServiceType
    {
        IDALServiceType iDALServiceType;

        public BLLServiceType(IDALServiceType iDALServiceType)
        {
            this.iDALServiceType = iDALServiceType;
        }
        public BLLServiceType()
            : this(new DALServiceType())
        { }
        public ServiceType GetOne(Guid id)
        {
            return iDALServiceType.DalBase.GetOne(id);
        }
        public IList<ServiceType> GetAll()
        {
            return iDALServiceType.DalBase.GetAll<ServiceType>();
        }
        public void SaveOrUpdate(ServiceType serviceType)
        {
            iDALServiceType.DalBase.SaveOrUpdate(serviceType);
        }
        /// <summary>
        /// 获取最顶层的类型
        /// </summary>
        /// <returns></returns>
        public IList<ServiceType> GetTopList()
        {
            return iDALServiceType.GetTopList();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="serviceName">属性名称</param>
        /// <param name="parentId">所属分类ID</param>
        /// <param name="values">属性值, 多个用逗号分隔</param>
        /// <returns></returns>
        public ServiceType Create(string propertyName, Guid serviceTypeId, string values)
        {
            
            
                 ServiceType currentServiceType = GetOne(serviceTypeId);
                ServiceProperty serviceProperty=new ServiceProperty{ Name=propertyName, ServiceType=currentServiceType};
            
            IList<ServicePropertyValue> propertyValues = new List<ServicePropertyValue>();
            string[] arrPropertyValues = values.Split(',');
            foreach (string value in arrPropertyValues)
            {
                ServicePropertyValue propertyValue = new ServicePropertyValue {  PropertyValue=value, ServiceProperty=serviceProperty };
                propertyValues.Add(propertyValue);
            }
            serviceProperty.Values = propertyValues;
            SaveOrUpdate(currentServiceType);
            return currentServiceType;
        }
    }
}
