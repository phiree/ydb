using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
namespace Dianzhu.Api.Model
{
    public class ReqDataMERM001003
    {
        //todo:初始化为不可能传递进来值,序列化之后对比,用以判断是否传递了该值.
        public ReqDataMERM001003()
        {
            //alias = "nosuchalias#$#";
            //email = "a@nosuch.email";
            //phone = "nosuchphone#$#";
            //password = "nosuchpassword#$#";
            //address = "nosuchaddress#$#";
        }
        public string userID { get; set; }
        public string pWord { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; } //new password
    }

    public class ReqDataMERM001005
    {
        public string email { get; set; }
        public string phone { get; set; }
        public string pWord { get; set; }

    }
    /*
    public class RespDataMERM001005
    {
        public RespDataMERM001005_merObj merObj { get; set; }
    }
    public class RespDataMERM001005_merObj
    {
        public string merID { get; set; }
        public string alias { get; set; }
        public string area { get; set; }
        public string doc { get; set; }
        public string imgUrl { get; set; }
        public string linkMan { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string showImgUrls { get; set; }
        public RespDataMERM001005_merObj Adapt(Dianzhu.Model.Business business)
        {
            this.alias = business.Name;
            this.merID = business.Id.ToString();
            this.area = business.AreaBelongTo==null?string.Empty:business.AreaBelongTo.Name;
            this.doc = business.Description;
            this.imgUrl = business.BusinessAvatar.ImageName;
            this.linkMan = business.Contact;
            this.address = business.Address;
            this.phone = business.Phone;

            this.showImgUrls = string.Empty;
            foreach (BusinessImage img in business.BusinessShows)
            {
                showImgUrls += img.ImageName + ",";
            }
            this.showImgUrls.TrimEnd(',');

            return this;
        }
    }
    */
}

public class RespDataMERM_merObj
{
    public string userID { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string imgUrl { get; set; }
    public string userType { get; set; }

    public RespDataMERM_merObj Adapt(MemberDto membership)
    {
        this.userID = membership.Id.ToString();
        this.alias = membership.NickName ?? "";
        this.email = membership.Email ?? "";
        this.phone = membership.Phone ?? "";
        this.imgUrl = string.IsNullOrEmpty(membership.AvatarUrl) ? string.Empty
                : (
                  Dianzhu.Config.Config.GetAppSetting("MediaGetUrl")
                + membership.AvatarUrl);

        return this;

    }
}

public class RespDataMERM// 
{
    public RespDataMERM_merObj merObj { get; set; }
}
