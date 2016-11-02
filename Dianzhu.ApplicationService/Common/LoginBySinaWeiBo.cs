using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Newtonsoft.Json;
using System.Collections.Specialized;
using PHSuit;

namespace Dianzhu.ApplicationService
{
    /*
    public class LoginBySinaWeiBo
    {
        public static Model.DZMembership GetUserInfo(string code, string appName, BLL.DZMembershipProvider bllMember, string userType)
        {
            string AppIDWeibo = Dicts.AppIDWeibo;
            string AppSecretWeibo = Dicts.AppSecretWeibo;
            string urlWeibo = Dianzhu.Config.Config.GetAppSetting("WeiboAccessTokenUrl");
            string urlWeiboBack = Dianzhu.Config.Config.GetAppSetting("WeiboBackUrl");

            var respDataWeibo = new NameValueCollection();
            respDataWeibo.Add("client_id", AppIDWeibo);
            respDataWeibo.Add("client_secret", AppSecretWeibo);
            respDataWeibo.Add("grant_type", "refresh_token");
            respDataWeibo.Add("code", code);
            respDataWeibo.Add("redirect_uri", urlWeiboBack);
            //respDataWeibo.Add("access_token", code);
            respDataWeibo.Add("refresh_token", code);
            string resultReTokenWeibo = HttpHelper.CreateHttpRequest(urlWeibo.ToString(), "post", respDataWeibo);
            WeiboRespTokenObj respTokenObjWeibo = JsonConvert.DeserializeObject<WeiboRespTokenObj>(resultReTokenWeibo);

            if (respTokenObjWeibo.uid != null)
            {
                string urlWeoboToken = Dianzhu.Config.Config.GetAppSetting("WeiboTokenUrl");
                var respDataTokenWeibo = new NameValueCollection();
                respDataTokenWeibo.Add("access_token", respTokenObjWeibo.access_token);
                string resultTokenWeibo = HttpHelper.CreateHttpRequest(urlWeoboToken.ToString(), "post", respDataTokenWeibo);
                WeiboRequestTokenInfo tokeninfoWeibo = JsonConvert.DeserializeObject<WeiboRequestTokenInfo>(resultTokenWeibo);

                string uidWeibo = tokeninfoWeibo.uid;
                //string screenName = //用户昵称,  参数uid与screen_name二者必选其一，且只能选其一
                string urlWeiboUserinfo = Dianzhu.Config.Config.GetAppSetting("WeiboUserUrl");
                Uri uriWeiboUserinfo = new Uri(urlWeiboUserinfo + "access_token=" + respTokenObjWeibo.access_token + "&uid=" + uidWeibo);
                string resultUserinfoWeibo = HttpHelper.CreateHttpRequest(uriWeiboUserinfo.ToString(), "get", null);
                WeiboRespUserinfo userinfoWeibo = JsonConvert.DeserializeObject<WeiboRespUserinfo>(resultUserinfoWeibo);

                bool isAddWeibo;
                DZMembershipSinaWeibo sinaWeiboMember = ConvertToSinaWeiboMember(respTokenObjWeibo, tokeninfoWeibo, userinfoWeibo, out isAddWeibo, bllMember, userType);
                if (isAddWeibo)
                {
                    bllMember.CreateUserForU3rd(sinaWeiboMember);
                    sinaWeiboMember.UserName = sinaWeiboMember.Id.ToString();
                    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                }
                else
                {
                    //bllMember.UpdateUserForU3rd(sinaWeiboMember);
                }
                return sinaWeiboMember;
            }
            else
            {
                throw new Exception("传入code有误，请重新上传！");
            }
        }

        private static DZMembershipSinaWeibo ConvertToSinaWeiboMember(WeiboRespTokenObj tokenObj, WeiboRequestTokenInfo tokenInfo, WeiboRespUserinfo userObj, out bool isAdd, BLL.DZMembershipProvider bllMember, string userType)
        {
            isAdd = false;

            DZMembershipSinaWeibo member = (DZMembershipSinaWeibo)bllMember.GetUserBySinaWeiboUId(long.Parse(tokenInfo.uid));
            if (member == null)
            {
                member = (DZMembershipSinaWeibo)DZMembership.Create(Model.Enums.enum_LoginType.SinaWeiBo);
                isAdd = true;
            }
            else
            {
                return member;
            }

            member.AccessToken = tokenObj.access_token;
            member.ExpiresId = tokenInfo.expire_in;
            member.RefreshToken = tokenObj.refresh_token;
            member.Scope = tokenInfo.scope;
            member.CreateAt = tokenInfo.create_at;
            member.UId = userObj.id;
            member.UIdstr = userObj.idstr;
            member.ScreenName = userObj.screen_name;
            member.Name = userObj.name;
            member.Province = userObj.province;
            member.City = userObj.city;
            member.Location = userObj.location;
            member.Description = userObj.description;
            member.Url = userObj.url;
            member.ProfileImageUrl = userObj.profile_image_url;
            member.ProfileUrl = userObj.profile_url;
            member.Domain = userObj.domain;
            member.WeiHao = userObj.weihao;
            member.Gender = userObj.gender;
            member.FollowersCount = userObj.followers_count;
            member.FriendsCount = userObj.friends_count;
            member.StatusesCount = userObj.statuses_count;
            member.FavouritesCount = userObj.favourites_count;
            member.CreatedAt = userObj.created_at;
            member.Following = userObj.following;
            member.AllowAllActMsg = userObj.allow_all_act_msg;
            member.GeoEnabled = userObj.geo_enabled;
            member.Verified = userObj.verified;
            member.VerifiedType = userObj.verified_type;
            member.Remark = userObj.remark;
            member.AllowAllComment = userObj.allow_all_comment;
            member.AvatarLarge = userObj.avatar_large;
            member.AvatarHd = userObj.avatar_hd;
            member.VerifiedReason = userObj.verified_reason;
            member.FollowMe = userObj.follow_me;
            member.OnlineStatus = userObj.online_status;
            member.BiFollowersCount = userObj.bi_followers_count;
            member.Lang = userObj.lang;

            member.NickName = userObj.screen_name;
            member.UserName = userObj.screen_name + new Guid();//防止重复;
            if (string.IsNullOrEmpty(userObj.avatar_hd))
            {
                member.AvatarUrl = "";
            }
            else
            {
                member.AvatarUrl = utils.DownloadToMediaserver(userObj.avatar_hd, string.Empty, "image");
            }
            member.Address = userObj.location;
            member.PlainPassword = tokenInfo.uid;
            member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(tokenInfo.uid, "MD5");
            member.UserType = (Model.Enums.enum_UserType)Enum.Parse(typeof(Model.Enums.enum_UserType), userType);

            return member;
        }
    }*/
}
