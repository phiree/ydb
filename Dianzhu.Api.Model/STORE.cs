using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class RespDataSTORE_storeObj
    {
        //public string merchantID { get; set; }
        public string userID { get; set; }
        public string alias { get; set; }
        public string area { get; set; }
        public string doc { get; set; }
        public string imgUrl { get; set; }
        public string linkMan { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string showImgUrls { get; set; }
        //todo:下面四个属性没有
        public string identity { get; set; }
        public string url { get; set; }
        public string vintage { get; set; }
        public string headCount { get; set; }
        public string appraise { get; set; }
        public RespDataSTORE_storeObj Adapt(Business business)
        {
            //if (business.Owner.Id != Guid.Empty)
            //{
            //    this.merchantID = business.Owner.Id != null ? business.Owner.Id.ToString() : "";
            //}
            this.userID = business.Id != null ? business.Id.ToString() : "";
            this.alias = business.Name != null ? business.Name : "";
            if (business.AreaBelongTo != null)
            {
                this.area = business.AreaBelongTo.Name != null ? business.AreaBelongTo.Name : "";
            }
            else
            {
                this.area = "";
            }
            this.doc = business.Description != null ? business.Description : "";
            if (business.BusinessAvatar.Id != Guid.Empty)
            {
                this.imgUrl = business.BusinessAvatar.ImageName != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + business.BusinessAvatar.ImageName : "";
            }
            else
            {
                this.imgUrl = "";
            }
            if (business.Contact != null)
            {
                this.linkMan = business.Contact;
            }
            else
            {
                if (business.Owner.Id != Guid.Empty)
                {
                    this.linkMan = business.Owner.NickName != null ? business.Owner.NickName : "";
                }
                else
                {
                    this.linkMan = "";
                }
            }
            this.phone = business.Phone != null ? business.Phone : "";
            this.address = business.Address != null ? business.Address : "";
            if (business.BusinessImages.Count > 0)
            {
                foreach (BusinessImage bi in business.BusinessImages)
                {
                    this.showImgUrls += Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + bi.ImageName + ",";
                }
                this.showImgUrls = this.showImgUrls.TrimEnd(',');
            }
            else
            {
                this.showImgUrls = "";
            }

            this.identity = string.Empty;
            this.url = string.Empty;
            this.vintage = string.Empty;
            this.headCount = string.Empty;
            this.appraise = "3.5";

            return this;
        }
    }

    #region STORE001001

    public class ReqDataSTORE001001
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
    }

    public class RespDataSTORE001001
    {
        public RespDataSTORE_storeObj storeObj { get; set; }
    }

    #endregion

    #region STORE001002

    public class ReqDataSTORE001002
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
    }

    #endregion

    #region STORE001003

    public class ReqDataSTORE001003
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public RespDataSTORE_storeObj storeObj { get; set; }
    }

    public class RespDataSTORE001003
    {
        public RespDataSTORE_storeObj storeObj { get; set; }
        public RespDataSTORE001003(string storeId)
        {
            //todo: 如果修改成功,则为"Y" 否则为"N"
            this.storeObj = new RespDataSTORE_storeObj();
            this.storeObj.userID = storeId;
            this.storeObj.alias = null;
            this.storeObj.area = null;
            this.storeObj.doc = null;
            this.storeObj.imgUrl = null;
            this.storeObj.linkMan = null;
            this.storeObj.phone = null;
            this.storeObj.address = null;
            this.storeObj.showImgUrls = null;
        }
    }

    #endregion

    #region STORE001004

    public class ReqDataSTORE001004
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
    }

    public class RespDataSTORE001004
    {
        public string sum { get; set; }
    }

    #endregion

    #region STORE001005

    public class ReqDataSTORE001005
    {
        public string storeID { get; set; }
    }

    public class RespDataSTORE001005
    {
        public RespDataSTORE_storeObj storeObj { get; set; }
    }

    #endregion

    #region STORE001006

    public class ReqDataSTORE001006
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
    }

    public class RespDataSTORE001006
    {
        public IList<RespDataSTORE_storeObj> arrayData { get; set; }
        public RespDataSTORE001006 AdaptList(IList<Business> storeList)
        {
            this.arrayData = new List<RespDataSTORE_storeObj>();
            RespDataSTORE_storeObj obj;
            foreach(Business b in storeList)
            {
                obj = new RespDataSTORE_storeObj().Adapt(b);
                this.arrayData.Add(obj);
            }

            return this;
        }
    }

    #endregion

    #region STORE002001

    public class ReqDataSTORE002001
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
        public string imgData { get; set; }
    }

    public class RespDataSTORE002001
    {
        public string imgUrl { get; set; }
    }

    #endregion

    #region STORE002002

    public class ReqDataSTORE002002
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
        public string imgData { get; set; }
    }

    public class RespDataSTORE002002
    {
        public string imgUrl { get; set; }
    }

    #endregion

    #region STORE002003

    public class ReqDataSTORE002003
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
        public string imgUrl { get; set; }
    }

    #endregion

    #region STORE002004

    public class ReqDataSTORE002004
    {
        public string merchantID { get; set; }
        public string pWord { get; set; }
        public string storeID { get; set; }
        public string imgData { get; set; }
        public string target { get; set; }
    }

    public class RespDataSTORE002004
    {
        public string imgUrl { get; set; }
    }

    #endregion
}
