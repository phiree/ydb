using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VMCustomerList 的摘要说明
/// </summary>
public class VMCustomer
{
    public string UserName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime TimeCreated { get; set; }
    public int LoginTimes { get; set; }
    public string FriendlyUserType { get; set; }
    public int CallTimes { get; set; }
    public int OrderCount { get; set; }
    public decimal OrderAmount { get; set; }
    public int LoginDates { get; set; }
    public decimal LoginRate
    {
        get
        {
            int registerDays = (DateTime.Now - TimeCreated).Days;
            if (registerDays == 0) { return 0; }
            return LoginDates / registerDays;
        }
    }




}
