using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ydb.Common;
/// <summary>
/// VMBusiness 的摘要说明
/// </summary>
public class VMBusiness
{
    public VMBusiness()
    {
       
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string WebSite { get; set; }
    public string RawAddressFromMapApi { get; set; }
    public string Contact { get; set; }
    public int StaffAmount { get; set; }
    public enum_IDCardType ChargePersonIdCardType { get; set; }
    public string ChargePersonIdCardNo { get; set; }
    public int WorkingYears { get; set; }
    public double Latitude { get; set; }
    public string Email { get; set; }
    public double Longtitude { get; set; }
    public Guid OwnerId { get; set; }
    public VMBusiness Adap(Ydb.BusinessResource.DomainModel.Business business)
    {
        this.Name = business.Name;
        this.Description = business.Description;
        this.Address = business.Address;
        this.Phone = business.Phone;
        this.WebSite = business.WebSite;
        this.StaffAmount = business.StaffAmount;
        this.ChargePersonIdCardType = business.ChargePersonIdCardType;
        this.WorkingYears = business.WorkingYears;
        this.Latitude = business.Latitude;
        this.Longtitude = business.Longitude;
        this.Email = business.Email;
        this.OwnerId = business.OwnerId;
        return this;
    }
}