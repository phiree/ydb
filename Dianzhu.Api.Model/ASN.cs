using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Ydb.BusinessResource.DomainModel;
using Ydb.Order.DomainModel;

namespace Dianzhu.Api.Model
{
    public class RespDataASN_staffObj
    {
        public string userID { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string imgUrl { get; set; }
        public RespDataASN_staffObj Adapt(Staff staff)
        {
            this.userID = staff.Id.ToString();
            this.alias = staff.NickName ?? "";
            this.email = staff.Email ?? "";
            this.phone = staff.Phone ?? "";
            this.address = staff.Address ?? "";
            if (string.IsNullOrEmpty(staff.Photo))
            {
                this.imgUrl = "";
            }
            else
            {
                this.imgUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + staff.Photo;
            }

            return this;
        }
    }

    #region ASN001001
    public class ReqDataASN001001
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
        public RespDataASN_staffObj userObj { get; set; }
    }

    public class RespDataASN001001
    {
        public RespDataASN_staffObj userObj { get; set; }
    }
    #endregion

    #region ASN001002
    public class ReqDataASN001002
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string userID { get; set; }
    }

    public class RespDataASN001002
    {
        public string result { get; set; }
    }
    #endregion

    #region ASN001003
    public class ReqDataASN001003
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string userID { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string imgUrl { get; set; }
    }

    public class RespDataASN001003
    {
        public RespDataASN_staffObj staffObj { get; set; }
        public RespDataASN001003(string userID)
        {
            //todo: 如果修改成功,则为"Y" 否则为"N"
            this.staffObj = new RespDataASN_staffObj();
            this.staffObj.userID = userID;
            this.staffObj.alias = null;
            this.staffObj.email = null;
            this.staffObj.phone = null;
            this.staffObj.address = null;
            this.staffObj.imgUrl = null;
        }
    }
    #endregion

    #region ASN001004
    public class ReqDataASN001004
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
    }

    public class RespDataASN001004
    {
        public string sum { get; set; }
    }
    #endregion

    #region ASN001005
    public class ReqDataASN001005
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string userID { get; set; }
    }

    public class RespDataASN001005
    {
        public RespDataASN_staffObj userObj { get; set; }
    }
    #endregion

    #region ASN001006
    public class ReqDataASN001006
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
    }

    public class RespDataASN001006
    {
        public IList<RespDataASN_staffObj> arrayData { get; set; }
        public RespDataASN001006 AdaptList(IList<Staff> staffList)
        {
            this.arrayData = new List<RespDataASN_staffObj>();
            foreach (Staff staff in staffList)
            {
                RespDataASN_staffObj staffObj = new RespDataASN_staffObj().Adapt(staff);
                this.arrayData.Add(staffObj);
            }

            return this;
        }
    }
    #endregion

    ///////////////////////////////////////////////    ASN002    ////////////////////////////////////////////////////////////

    public class RespDataASN_assignObj
    {
        public string userID { get; set; }
        public string orderID { get; set; }
        public string mark { get; set; }
        public RespDataASN_assignObj Adapt(OrderAssignment oa)
        {
            this.userID = oa.AssignedStaffId;
            this.orderID = oa.OrderId;
            if (oa.Enabled)
            {
                this.mark = "Y";
            }
            else
            {
                this.mark = "N";
            }

            return this;
        }
    }

    #region ASN002001
    public class ReqDataASN002001
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
        public IList<RespDataASN_assignObj> arrayData { get; set; }
    }

    public class RespDataASN002001
    {
        public IList<RespDataASN_assignObj> arrayError { get; set; }
    }
    #endregion

    #region ASN002004
    public class ReqDataASN002004
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
        public string orderID { get; set; }
        public string userID { get; set; }
    }

    public class RespDataASN002004
    {
        public IList<RespDataASN_assignObj> arrayData { get; set; }
    }
    #endregion
}
