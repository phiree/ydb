using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class RespDataUSM_userObj
    {
        public string userID { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string imgUrl { get; set; }
        public string address { get; set; }
        public RespDataUSM_userObj Adapt(DZMembership membership)
        {
            this.userID = membership.Id.ToString();
            this.alias = membership.DisplayName ?? "";
            this.email = membership.Email ?? "";
            this.phone = membership.Phone ?? "";
            this.imgUrl = membership.AvatarUrl == null
                ? string.Empty
                : Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + membership.AvatarUrl;
            this.address = membership.Address ?? "";
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
}
