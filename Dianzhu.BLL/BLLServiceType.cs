using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.Model;
using Dianzhu.DAL;
using System.IO;
using System.Data;
using PHSuit;

namespace Dianzhu.BLL
{
    public class BLLServiceType
    {
        public DALServiceType DALServiceType=DALFactory.DALServiceType;

         
        public ServiceType GetOne(Guid id)
        {
            return DALServiceType.GetOne(id);
        }
        public IList<ServiceType> GetAll()
        {
            return DALServiceType.GetAll<ServiceType>();
        }
        public void SaveOrUpdate(ServiceType serviceType)
        {
            DALServiceType.SaveOrUpdate(serviceType);
        }
        /// <summary>
        /// 获取最顶层的类型
        /// </summary>
        /// <returns></returns>
        public IList<ServiceType> GetTopList()
        {
            return DALServiceType.GetTopList();
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
         

        public void Import(System.IO.Stream excelFileStream)
        {
            PHSuit.ReadExcelToDataTable rtdt = new ReadExcelToDataTable(excelFileStream, false, false, 0);
            string msg;
            IList<ServiceType> topServiceTypes = ObjectAdapter(rtdt.Read(out msg));
            DALServiceType.SaveList(topServiceTypes);

        }
        public IList<ServiceType> ObjectAdapter(DataTable dtFromExcel)
        {
            List<ServiceType> topTypeList = new List<ServiceType>();
            List<ServiceTypeFromExcel> tfeList = new List<ServiceTypeFromExcel>();
            //先构建每行的服务类别 和 该类别的层级(列索引)
            foreach (DataRow row in dtFromExcel.Rows)
            {

                for (int i = 0; i < dtFromExcel.Columns.Count; i++)
                {

                    string cellValue = row[i].ToString();
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        ServiceType s = new ServiceType { Name = cellValue, DeepLevel = i,OrderNumber=dtFromExcel.Rows.IndexOf(row) };
                        ServiceTypeFromExcel tfe = new ServiceTypeFromExcel { ServiceType = s, ColIndex = i };
                        tfeList.Add(tfe);
                    }
                }
            }
            //构建类别之间的父子关系
            IList<ServiceTypeFromExcel> cacheParents = new List<ServiceTypeFromExcel>();

            foreach (ServiceTypeFromExcel tfe2 in tfeList)
            {
                ServiceType currentSt = tfe2.ServiceType;
                //发现根,清除缓存
                if (tfe2.ColIndex == 0)
                {
                    cacheParents.Clear();
                    currentSt.Parent = null;
                    topTypeList.Add(currentSt);
                }
                int parentCol = tfe2.ColIndex - 1;
                ServiceTypeFromExcel parent = cacheParents.LastOrDefault(x => x.ColIndex == parentCol);
                if (parent != null)
                {
                    parent.ServiceType.Children.Add(currentSt);
                    currentSt.Parent = parent.ServiceType;
                }

                cacheParents.Add(tfe2);

            }
            return topTypeList;
        }
    }
    class ServiceTypeFromExcel
    {
        public ServiceType ServiceType { get; set; }
        public int ColIndex { get; set; }
    }
}
