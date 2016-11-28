using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ydb.BusinessResource.DomainModel;
using Ydb.Finance.DomainModel;

/// <summary>
/// VMServiceTypeAndPoint 的摘要说明
/// </summary>
public class VMServiceTypeAndPoint
{
    public VMServiceTypeAndPoint()
    {
        
    }

    public ServiceType ServiceType { get; set; }
    public ServiceTypePoint TypePoint { get; set; }
}