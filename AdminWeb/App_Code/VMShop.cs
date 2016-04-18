using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VMBusiness 的摘要说明
/// </summary>
public class VMShop
{
    public VMShop()
    {
    }
    public string BusinessName { get; set; }
    public string ShopName { get; set; }
    public string CityName { get; set; }
    public IList<string> ServiceTypes { get; set; }
    public decimal OrderAmount { get; set; }
    public decimal OrderCount { get; set; }
    public decimal Score { get; set; }
    public decimal OrderCompleteCount { get; set; }
    public decimal OrderCompleteRate { get
        {
            return OrderCompleteCount / OrderCount;
        } }
    public decimal OrderCancelCount { get; set; }
    public decimal CustomerCancelCount { get; set; }
    public decimal ExposureRate { get; set; }
    public DateTime RegisterTime { get; set; }
    public string ServiceTypesDisplay { get {
            return string.Join(",", ServiceTypes.ToArray());
        } }
    


}