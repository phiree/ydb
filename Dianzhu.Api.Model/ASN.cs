using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    #region ASN001001
    public class ReqDataASN001001
    {
        public string storeID { get; set; }
        public string pWord { get; set; }
        public RespDataUSM_userObj userObj { get; set; }
    }

    public class RespDataASN001001
    {
        public RespDataUSM_userObj userObj { get; set; }
        public RespDataASN001001 Adapt(Staff staff)
        {
            this.userObj = new RespDataUSM_userObj();
            this.userObj.userID = staff.Id.ToString();
            this.userObj.alias = staff.Name;
            this.userObj.email = staff.Email;
            this.userObj.phone = staff.Phone;
            this.userObj.imgUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + staff.Photo;
            this.userObj.address = staff.Address;

            return this;
        }
    }
    #endregion

    #region ASN001002
    public class ReqDataASN001002
    {
        public string storeID { get; set; }
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
        public string storeID { get; set; }
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
        public string userID { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string imgUrl { get; set; }
        public RespDataASN001003(string userID)
        {
            //todo: 如果修改成功,则为"Y" 否则为"N"
            this.userID = userID;
            this.alias = null;
            this.email = null;
            this.phone = null;
            this.address = null;
            this.imgUrl = null;
        }
    }
    #endregion

    #region ASN001004
    public class ReqDataASN001004
    {
        public string storeID { get; set; }
        public string pWord { get; set; }
    }

    public class RespDataASN001004
    {
        public string sum { get; set; }
    }
    #endregion

    #region ASN001005
    public class ReqDataASN001005
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderId { get; set; }
        public string staffId { get; set; }
    }

    public class RespDataASN001005
    {
        public string assigned { get; set; }
    }
    #endregion

    #region ASN001006
    public class ReqDataASN001006
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string businessId { get; set; }
        public string staffId { get; set; }
        public string pageNum { get; set; }
        public string pageSize { get; set; }
    }

    public class RespDataASN001006
    {
        public IList<RespDataASN_arrayData> arrayData { get; set; }
    }

    public class RespDataASN_arrayData
    {
        public string orderId { get; set; }
        public string assigned { get; set; }
    }
    #endregion

    public class ReqDataASN001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public bool assign { get; set; }
        public string staffId { get; set; }
        public IList<string> arrayOrderId { get; set; }
    }

    public class ReqDataASN001008
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public bool assign { get; set; }
        public string orderId { get; set; }
        public IList<string> arrayStaffId { get; set; }
    }
}
