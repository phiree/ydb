using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Dianzhu.Model;

namespace Dianzhu.ApplicationService
{
    public class SortMapping
    {
        public static string SortMap(string sortby, string TName)
        {
            if (string.IsNullOrEmpty(sortby))
            {
                return "";
            }
            string sort = "";
            if (TName == "Business")
            {
                switch (sortby)
                {
                    case "id":
                        sort = "Id";
                        break;
                    case "name":
                        sort = "Name";
                        break;
                    case "vintage":
                        sort = "WorkingYears";
                        break;
                    case "headCount":
                        sort = "StaffAmount";
                        break;
                    default:
                        sort = "CreatedTime";
                        break;
                }
            }
            if (TName == "OrderAssignment")
            {
                sort = "CreateTime";
            }
            if (TName == "ReceptionChat")
            {
                switch (sortby)
                {
                    case "sendTime":
                        sort = "SavedTime";
                        break;
                    default:
                        sort = "SavedTime";
                        break;
                }
            }
            if (TName == "Area")
            {
                switch (sortby)
                {
                    case "name":
                        sort = "Name";
                        break;
                    case "code":
                        sort = "Code";
                        break;
                    default:
                        sort = "AreaOrder";
                        break;
                }
            }
            if (TName == "Complaint")
            {
                sort = "CreatTime";
            }
            if (TName == "ServiceOrder")
            {
                switch (sortby)
                {
                    case "title":
                        sort = "Title";
                        break;
                    case "closeTime":
                        sort = "OrderFinished";
                        break;
                    case "serviceTime":
                        sort = "OrderServerStartTime";
                        break;
                    case "doneTime":
                        sort = "OrderServerFinishedTime";
                        break;
                    case "updateTime":
                        sort = "LatestOrderUpdated";
                        break;
                    default:
                        sort = "OrderCreated";
                        break;
                }
            }
            if (TName == "Payment")
            {
                switch (sortby)
                {
                    case "amount":
                        sort = "Amount";
                        break;
                    case "updateTime":
                        sort = "LastUpdateTime";
                        break;
                    default:
                        sort = "CreatedTime";
                        break;
                }
            }
            if (TName == "DZService")
            {
                switch (sortby)
                {
                    case "name":
                        sort = "Name";
                        break;
                    case "unitPrice":
                        sort = "UnitPrice";
                        break;
                    case "startAt":
                        sort = "MinPrice";
                        break;
                    default:
                        sort = "CreatedTime";
                        break;
                }
            }
            if (TName == "Staff")
            {
                switch (sortby)
                {
                    case "alias":
                        sort = "DisplayName";
                        break;
                    case "number":
                        sort = "Code";
                        break;
                    case "phone":
                        sort = "Phone";
                        break;
                    default:
                        sort = "Code";
                        break;
                }
            }
            if (TName == "ServiceOrderRemind")
            {
                switch (sortby)
                {
                    case "title":
                        sort = "Title";
                        break;
                    case "remindTime":
                        sort = "RemindTime";
                        break;
                    default:
                        sort = "CreateTime";
                        break;
                }
            }
            if (TName == "DZMembership")
            {
                switch (sortby)
                {
                    case "alias":
                        sort = "DisplayName";
                        break;
                    case "phone":
                        sort = "Phone";
                        break;
                    default:
                        sort = "TimeCreated";
                        break;
                }
            }
            if (TName == "Snapshots")
            {
                switch (sortby)
                {
                    case "date":
                        sort = "OrderCreated";
                        break;
                    case "phone":
                        sort = "Phone";
                        break;
                    default:
                        sort = "OrderCreated";
                        break;
                }
            }
            if (TName == "ClaimsDetails")
            {
                switch (sortby)
                {
                    case "amount":
                        sort = "Amount";
                        break;
                    default:
                        sort = "CreatTime";
                        break;
                }
            }
            if (TName == "BillSatisticService")
            {
                sort = sortby;
            }

           return sort;
        }

       
    }
}
