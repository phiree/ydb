﻿using System;
using System.Collections.Generic;
using System.Data;
using Ydb.BusinessResource.DomainModel;
using Ydb.Finance.DomainModel;
using Ydb.Infrastructure;
namespace Ydb.ApplicationService.ExcelImporter
{
    /// <summary>
    /// ServiceTypeAndPointAdapter 的摘要说明
    /// </summary>
    public class ServiceTypeAndPointAdapter
{
    public ServiceTypeAndPointAdapter()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }


    public IList<VMServiceTypeAndPoint> Adapt(DataTable dataFromExcel)
    {
        
        List<VMServiceTypeAndPoint> vmTypeAndPointList = new List<VMServiceTypeAndPoint>();


        // var  pointList = new List<ServiceTypePoint>();
      
        //先构建每行的服务类别 和 该类别的层级(列索引)


        DataRow row1 = dataFromExcel.NewRow();
        for (int i = 0; i < 6; i++)
        {
            row1[i] = dataFromExcel.Columns[i].ColumnName;
        }
        row1[3] = "";
        row1[4] = "";
        dataFromExcel.Rows.InsertAt(row1, 0);

        string[] pointLast = {"0", "0"};
        ServiceType currentParentL1 = null;
        ServiceType currentParentL2 = null;
        int currentParentIndex =99;
        foreach (DataRow row in dataFromExcel.Rows)
        {
            Guid gid = new Guid(row[0].ToString());
            string code = row[1].ToString();

            for (var i = 2; i < 5; i++)
            {
                string cellvalue = row[i].ToString();

                if (!string.IsNullOrEmpty(cellvalue))
                {
                       
                  ServiceType  s = new ServiceType
                    {
                        Id = gid,
                        Code = code,
                        Name = cellvalue,
                        
                        DeepLevel = i - 2,
                        OrderNumber = dataFromExcel.Rows.IndexOf(row)
                    };
                    if (i == 2) { currentParentL1 = s;
                        s.Parent = null;
                    }
                    else if (i == 3)
                    {
                        currentParentL2 = s;
                        s.Parent = currentParentL1;
                    }
                    else
                    {
                        s.Parent = currentParentL2;

                    }

                    ServiceTypePoint typePoint = null;
                    string pointValue = row[5].ToString();
                    if (!string.IsNullOrEmpty(Ydb.Common.StringHelper.ReplaceSpace(pointValue)))
                    {
                        decimal point = Convert.ToDecimal(pointValue);
                        typePoint = new ServiceTypePoint {Id = s.Id, Point = point, ServiceTypeId = gid.ToString()};
                    }
                   
                    VMServiceTypeAndPoint vmTypeAndPoint = new VMServiceTypeAndPoint {ServiceType = s, TypePoint = typePoint};
                    vmTypeAndPointList.Add(vmTypeAndPoint);
                }
            }
        }
        return vmTypeAndPointList;
    }
}

 
class ServiceTypeAndPointLevel{
    public VMServiceTypeAndPoint VMServiceTypeAndPoint { get; set; }
    public int ColIndex { get; set; }
}
}