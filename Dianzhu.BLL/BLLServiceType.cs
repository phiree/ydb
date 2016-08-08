using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.Model;
using Dianzhu.DAL;
using System.IO;
using System.Data;
using PHSuit;
using Dianzhu.Model.Finance;
using DDDCommon;

namespace Dianzhu.BLL
{
    public class BLLServiceType
    {
        IDAL.IDALServiceType dalServiceType;

        public BLLServiceType(IDAL.IDALServiceType dalServiceType)
        {
            this.dalServiceType = dalServiceType;
        }

        public void Save(ServiceType serviceType)
        {
            dalServiceType.Add(serviceType);
        }
        public void Update(ServiceType serviceType)
        {
            dalServiceType.Update(serviceType);
        }
        public ServiceType GetOne(Guid id)
        {
            return dalServiceType.FindById(id);
        }

        public ServiceType GetOneByName(string name, int level)
        {
            return dalServiceType.GetOneByName(name, level);
        }

        public IList<ServiceType> GetAll()
        {
            return dalServiceType.Find(x => true); 
        }

        /// <summary>
        /// 查询 superID 的下级服务类型列表数组,当 superID 为空时，默认查询顶层服务类型列表
        /// </summary>
        /// <param name="guidSuperID"></param>
        /// <returns></returns>
        public IList<ServiceType> GetAllServiceTypes(Guid guidSuperID)
        {
            var where = PredicateBuilder.True<ServiceType>();
            if (guidSuperID == Guid.Empty)
            {
                where = where.And(x => x.DeepLevel == 0);
            }
            else
            {
                where = where.And(x => x.Parent.Id == guidSuperID);
            }
            return dalServiceType.Find(where);
        }

        /// <summary>
        /// 获取最顶层的类型
        /// </summary>
        /// <returns></returns>
        public IList<ServiceType> GetTopList()
        {
            return dalServiceType.GetTopList();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="serviceName">属性名称</param>
        /// <param name="parentId">所属分类ID</param>
        /// <param name="values">属性值, 多个用逗号分隔</param>
        /// <returns></returns>
        

        DAL.Finance.DALServiceTypePoint dalPoint = new DAL.Finance.DALServiceTypePoint();
        
        public void Import(System.IO.Stream excelFileStream)
        {
            PHSuit.ReadExcelToDataTable rtdt = new ReadExcelToDataTable(excelFileStream, false, false, 0);
            string msg;
            IList<ServiceTypePoint> pointList;
            IList<ServiceType> topServiceTypes = ServiceTypePointAdapter(rtdt.Read(out msg),out pointList);
            
            dalServiceType.SaveList(topServiceTypes);
            
             dalPoint.SaveList(pointList);

        }
        public IList<ServiceType> ObjectAdapter(DataTable dtFromExcel)
        {
            List<ServiceType> topTypeList = new List<ServiceType>();
            List<ServiceTypeFromExcel> tfeList = new List<ServiceTypeFromExcel>();
            //先构建每行的服务类别 和 该类别的层级(列索引)
            foreach (DataRow row in dtFromExcel.Rows)
            {
                Guid gid = new Guid(row[0].ToString());
                string code = row[1].ToString();
                for (int i = 2; i < dtFromExcel.Columns.Count; i++)
                {
                    string cellvalue = row[i].ToString();
                    if (!string.IsNullOrEmpty(cellvalue))
                    {
                        ServiceType s = new ServiceType {Id=gid,Code=code, Name = cellvalue, DeepLevel = i-2, OrderNumber = dtFromExcel.Rows.IndexOf(row) };
                        ServiceTypeFromExcel tfe = new ServiceTypeFromExcel{ServiceType=s, ColIndex=i  };
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
                if (tfe2.ColIndex == 2)
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
        public IList<ServiceType> ServiceTypePointAdapter(DataTable dtFromExcel,out IList<ServiceTypePoint> pointList)
        {
            List<ServiceType> topTypeList = new List<ServiceType>();
             pointList = new List<ServiceTypePoint>();
            List<ServiceTypeFromExcel> tfeList = new List<ServiceTypeFromExcel>();
            //先构建每行的服务类别 和 该类别的层级(列索引)
            foreach (DataRow row in dtFromExcel.Rows)
            {
                Guid gid = new Guid(row[0].ToString());
                string code = row[1].ToString();
                 
                ServiceType s=null;
                for (int i = 2; i < 5; i++)
                {
                    string cellvalue = row[i].ToString();
                    
                    if (!string.IsNullOrEmpty(cellvalue))
                    {
                        
                        s = new ServiceType { Id = gid, Code = code, Name = cellvalue, DeepLevel = i - 2, OrderNumber = dtFromExcel.Rows.IndexOf(row) };
                       
                        ServiceTypeFromExcel tfe = new ServiceTypeFromExcel { ServiceType = s, ColIndex = i };
                        tfeList.Add(tfe);
                    }
                }
                if (s != null)
                {
                    string pointValue = row[5].ToString();
                    if(!string.IsNullOrEmpty(pointValue))
                    {
                        decimal point = Convert.ToDecimal(pointValue);
                        ServiceTypePoint typePoint = new ServiceTypePoint { Id = s.Id, Point = point, ServiceType = s };
                        pointList.Add(typePoint);
                    }
                }

            }
            //构建类别之间的父子关系
            IList<ServiceTypeFromExcel> cacheParents = new List<ServiceTypeFromExcel>();

            foreach (ServiceTypeFromExcel tfe2 in tfeList)
            {
                ServiceType currentSt = tfe2.ServiceType;
                //发现根,清除缓存
                if (tfe2.ColIndex == 2)
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
