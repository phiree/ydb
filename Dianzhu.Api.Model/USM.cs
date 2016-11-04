using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Ydb.Membership.Application.Dto;
namespace Dianzhu.Api.Model
{
    #region USM接口公用类
    public class RespDataUSM_userObj
    {
        public string userID { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string imgUrl { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public RespDataUSM_userObj Adapt(MemberDto membership)
        {
            this.userID = membership.Id.ToString();
            this.alias = membership.DisplayName ?? "";
            this.email = membership.Email ?? "";
            this.phone = membership.Phone ?? "";
            this.imgUrl = membership.AvatarUrl == null
                ? string.Empty
                : Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + membership.AvatarUrl;
            this.address = membership.Address ?? "";
            this.name =  membership.NickName ?? membership.UserName??"";
            return this;
        }
    }

    public class ReqDataUSM
    {
        public string email { get; set; }
        public string phone { get; set; }
        public string pWord { get; set; }
    }

    public class RespDataUSM
    {
        public RespDataUSM_userObj userObj { get; set; }
    }
    #endregion

    #region USM001003
    public class ReqDataUSM001003
    {
        //todo:初始化为不可能传递进来值,序列化之后对比,用以判断是否传递了该值.
        public ReqDataUSM001003()
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
        public string address { get; set; }
    }
    public class RespDataUSM001003
    {
        public string userID { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; } //new password
        public string address { get; set; }
        public RespDataUSM001003(string uid)
        {
            //todo: 如果修改成功,则为"Y" 否则为"N"
            this.userID = uid;
            this.alias = null;
            this.email = null;
            this.phone = null;
            this.password = null;
            this.address = null;
        }
    }
    #endregion

    #region USM001007
    public class ReqDataUSM001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string imgData { get; set; }

    }
    public class RespDataUSM001007
    {
        public string userID { get; set; }
        public string imgUrl { get; set; }
    }
    #endregion

    #region USM001008
    public class ReqDataUSM001008
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string Resource { get; set; }
        public string type { get; set; }
        public USM001008UploadedFileType FileType
        {
            get
            {
                USM001008UploadedFileType fileType;
                bool isType = Enum.TryParse<USM001008UploadedFileType>(type, out fileType);
                if (!isType) { throw new Exception("不可识别的文件类型"); }
                return fileType;
            }
        }


    }
    public enum USM001008UploadedFileType
    {
        voice,
        image,
        video
    }
    public class RespDataUSM001008
    {
        public string userID { get; set; }
        public string ResourceUrl { get; set; }
    }
    #endregion

    #region USM001009

    public class ReqDataUSM001009
    {
        public string phone { get; set; }
        public string newPWord { get; set; }
    }

    public class RespDataUSM001009
    {
        public string result { get; set; }
    }

    #endregion

    #region USM001010

    public class ReqDataUSM001010
    {
        public string phone { get; set; }
    }

    public class RespDataUSM001010
    {
        public string result { get; set; }
    }

    #endregion
}
