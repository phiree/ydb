using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VMBusiness 的摘要说明
/// </summary>
public class VMBusiness
{
    public VMBusiness()
    {
    }
    public string BusinessName { get; set; }
    public string ShopName { get; set; }
    public string CityName { get; set; }
    public IList<string> ServiceTypes { get; set; }
    public decimal OrderAmount { get; set; }
    public decimal OrderCount { get; set; }
    public decimal Score { get; set; }
    public decimal OrderCompleteRate { get; set; }
    public decimal OrderUncompleteRate { get; set; }
    public decimal CustomerCancelRate { get; set; }
    public decimal ExposureRate { get; set; }
    public DateTime RegisterTime { get; set; }

}