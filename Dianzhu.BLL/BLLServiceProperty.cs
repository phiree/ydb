using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
  public  class BLLServiceProperty
    {

      BLLServiceType bllServiceType = new BLLServiceType();
      IDALServiceProperty iDALServiceProperty;
      
      public BLLServiceProperty(IDALServiceProperty iDALServiceProperty)
      {
          this.iDALServiceProperty = iDALServiceProperty;
      }
      public BLLServiceProperty()
          : this(new DALServiceProperty ())
      { }
      public ServiceProperty GetOne(Guid id)
      {
          return iDALServiceProperty.DalBase.GetOne(id);
      }
      public IList<ServiceProperty> GetList(Guid serviceTypeId)
      {
          return iDALServiceProperty.GetList(serviceTypeId);
      }
      public void Delete(ServiceProperty p)
      {
          iDALServiceProperty.DalBase.Delete(p);
      }
      /// <summary>
      /// 保存
      /// </summary>
      /// <param name="serviceName">属性名称</param>
      /// <param name="parentId">所属分类ID</param>
      /// <param name="values">属性值, 多个用逗号分隔</param>
      /// <returns></returns>
      public ServiceProperty SaveOrUpdate(Guid propertyId, string propertyName, Guid serviceTypeId, string values)
      {


          ServiceType currentServiceType = bllServiceType.GetOne(serviceTypeId);
          ServiceProperty serviceProperty = new ServiceProperty { Name = propertyName, ServiceType = currentServiceType };
          if (propertyId!=null && propertyId != Guid.Empty)
          { 
            serviceProperty=GetOne(propertyId);
          }
          IList<ServicePropertyValue> propertyValues = new List<ServicePropertyValue>();
          string[] arrPropertyValues = values.Split(',');
          foreach (string value in arrPropertyValues)
          {
              ServicePropertyValue propertyValue = new ServicePropertyValue { PropertyValue = value, ServiceProperty = serviceProperty };
              propertyValues.Add(propertyValue);
          }
          serviceProperty.Values.Clear();
          serviceProperty.Values = propertyValues;
          iDALServiceProperty.DalBase.SaveOrUpdate(serviceProperty);
          return serviceProperty;
      }
    }
}
