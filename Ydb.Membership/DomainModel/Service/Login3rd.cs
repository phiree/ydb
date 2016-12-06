using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel.Repository;
using PHSuit;
using Newtonsoft.Json;
using Ydb.Membership.DomainModel.Enums;
using System.Web;
using Ydb.Membership.Infrastructure;
using Ydb.Common.Infrastructure;
namespace Ydb.Membership.DomainModel.Service
{
   public  class Login3rd:ILogin3rd
    {
        IHttpRequest httpRequest;
        IDownloadAvatarToMediaServer avatarDownloader;
        IRepositoryDZMembership repMem;
        IEncryptService encryptService;

        
        public Login3rd(IHttpRequest httpRequest, IDownloadAvatarToMediaServer avatarDownloader,
            IRepositoryDZMembership repMem, IEncryptService encryptService)
        {
            
            this.httpRequest = httpRequest;
            this.avatarDownloader = avatarDownloader;
            this.repMem = repMem;// Bootstrap.Container.Resolve<IRepositoryDZMembership>();
            this.encryptService = encryptService;
            
        }
        public DZMembership Login(string platform, string code, string appName, string userType)
        {
            DZMembership newMember;
            switch (platform)
            {
                case "WeChat":
                    newMember = GetWechatUserInfo(code, appName, repMem, userType);
                    break;
                case "SinaWeiBo":
                    newMember = GetSinaWeiboUserInfo(code, appName, repMem, userType);
                    break;
                case "TencentQQ":
                    newMember = GeQQtUserInfo(code, appName, repMem, userType);
                    break;
                default:
                    throw new Exception("传入的第三方平台类型有误，请重新上传！!");
            }
            return newMember;
        }
        public    DZMembership GetSinaWeiboUserInfo(string code, string appName, IRepositoryDZMembership repositoryMembership, string userType)
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
            string resultReTokenWeibo = httpRequest.CreateHttpRequest(urlWeibo.ToString(), "post", respDataWeibo);
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
                string resultUserinfoWeibo = httpRequest.CreateHttpRequest(uriWeiboUserinfo.ToString(), "get", null);
                WeiboRespUserinfo userinfoWeibo = JsonConvert.DeserializeObject<WeiboRespUserinfo>(resultUserinfoWeibo);

                bool isAddWeibo;
                DZMembershipSinaWeibo sinaWeiboMember = ConvertToSinaWeiboMember(respTokenObjWeibo, tokeninfoWeibo, userinfoWeibo,  repositoryMembership, userType, out isAddWeibo);
                if (isAddWeibo)
                {
                    repositoryMembership.Add(sinaWeiboMember);
                    sinaWeiboMember.UserName = sinaWeiboMember.Id.ToString();
                     
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

        private   DZMembershipSinaWeibo ConvertToSinaWeiboMember(WeiboRespTokenObj tokenObj, WeiboRequestTokenInfo tokenInfo, WeiboRespUserinfo userObj, IRepositoryDZMembership repoMember, string userType, out bool isAdd)
        {
            isAdd = false;

            DZMembershipSinaWeibo member = (DZMembershipSinaWeibo)repoMember.GetMemberBySinaWeiboUid(long.Parse(tokenInfo.uid));
            if (member == null)
            {
                member = (DZMembershipSinaWeibo)DZMembership.Create( Enums.LoginType.SinaWeiBo);
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
                member.AvatarUrl = avatarDownloader.DownloadToMediaserver(userObj.avatar_hd, string.Empty, "image");
            }
            member.Address = userObj.location;
            member.PlainPassword = tokenInfo.uid;
            member.Password = encryptService.GetMD5Hash(tokenInfo.uid);
            member.UserType = (Enums.UserType)Enum.Parse(typeof( UserType), userType);

            return member;
        }


        public  DZMembership GeQQtUserInfo(string code, string appName, IRepositoryDZMembership repositoryMembership, string userType)
        {
            string AppIDQQ = string.Empty;
            switch (appName)
            {
                case "IOS_Customer":
                case "IOS_Merchant":
                case "IOS_CustomerService":
                    AppIDQQ = Dicts.AppIDQQIos;
                    break;
                case "Android_Customer":
                case "Android_Merchant":
                case "Android_CustomerService":
                    AppIDQQ = Dicts.AppIDQQAndroid;
                    break;
                default:
                    throw new Exception("传入appName有误，请重新上传！");
            }
            string urlQQOpenid = Dianzhu.Config.Config.GetAppSetting("QQOpenidUrl");
            Uri uriQQOpenid = new Uri(urlQQOpenid + "access_token=" + code);
            string resultOpenidQQCallback = httpRequest.CreateHttpRequest(uriQQOpenid.ToString(), "get", null);
            string resultOpenidQQ = resultOpenidQQCallback.Substring(9, resultOpenidQQCallback.LastIndexOf(")") - 9);
            QQRespOpenid refreshOpenidObj = JsonConvert.DeserializeObject<QQRespOpenid>(resultOpenidQQ);

            if (refreshOpenidObj.openid != null)
            {
                string urlQQUsrinfo = Dianzhu.Config.Config.GetAppSetting("QQUserInfoUrl");
                Uri uriQQUserinfo = new Uri(urlQQUsrinfo + "access_token=" + code + "&oauth_consumer_key=" + AppIDQQ + "&openid=" + refreshOpenidObj.openid + "&format=json");
                string resultUserinfoQQ = httpRequest.CreateHttpRequest(uriQQUserinfo.ToString(), "get", null);
                QQRespUserinfo UserinfoObjQQ = JsonConvert.DeserializeObject<QQRespUserinfo>(resultUserinfoQQ);

                bool isAddQQ;
                DZMembershipQQ qqMember = ConvertToQQMember(refreshOpenidObj, UserinfoObjQQ, out isAddQQ, repositoryMembership, userType);
                if (UserinfoObjQQ.ret < 0)
                {
                    throw new Exception("ret:" + UserinfoObjQQ.ret + ";msg:" + UserinfoObjQQ.msg);
                }
                if (isAddQQ)
                {
                    repositoryMembership.Add(qqMember);
                    qqMember.UserName = qqMember.Id.ToString();
                   
                }
                else
                {
                    //bllMember.UpdateUserForU3rd(qqMember);
                }

                return qqMember;
            }
            else
            {
                throw new Exception("传入code有误，请重新上传！");
            }
        }

        private   DZMembershipQQ ConvertToQQMember(QQRespOpenid openidObj, QQRespUserinfo userObj, out bool isAdd, IRepositoryDZMembership repositoryMembership, string userType)
        {
            isAdd = false;

            DZMembershipQQ member = (DZMembershipQQ)repositoryMembership.GetMemberByQQOpenId(openidObj.openid);
            if (member == null)
            {
                member = (DZMembershipQQ)DZMembership.Create(LoginType.TencentQQ);
                isAdd = true;
            }
            else
            {
                return member;
            }
            //DZMembershipWeChat member =(DZMembershipWeChat)DZMembership.Create(enum_LoginType.WeChat);

            member.ClientId = openidObj.client_id;
            member.OpenId = openidObj.openid;
            member.Ret = userObj.ret;
            member.Msg = userObj.msg;
            member.Nickname = userObj.nickname;
            member.Gender = userObj.gender;
            member.Province = userObj.province;
            member.City = userObj.city;
            member.Year = userObj.year;
            member.Figureurl = userObj.figureurl;
            member.Figureurl1 = userObj.figureurl_1;
            member.Figureurl2 = userObj.figureurl_2;
            member.FigureurlQq1 = userObj.figureurl_qq_1;
            member.FigureurlQq2 = userObj.figureurl_qq_2;
            member.IsYellowVip = userObj.is_yellow_vip;
            member.Vip = userObj.vip;
            member.YellowVipLevel = userObj.yellow_vip_level;
            member.Level = userObj.level;
            member.IsYellowYearVip = userObj.yellow_vip_level;

            member.NickName = userObj.nickname;
            member.UserName = userObj.nickname + new Guid();//防止重复;
            if (string.IsNullOrEmpty(userObj.figureurl_qq_2))
            {
                member.AvatarUrl = avatarDownloader.DownloadToMediaserver(userObj.figureurl_qq_1, string.Empty, "image");
            }
            else
            {
                member.AvatarUrl = avatarDownloader.DownloadToMediaserver(userObj.figureurl_qq_2, string.Empty, "image");
            }

            member.Address = userObj.province + " " + userObj.city;
            member.PlainPassword = openidObj.openid;
            member.Password = encryptService.GetMD5Hash(openidObj.openid);
            member.UserType = ( Enums. UserType)Enum.Parse(typeof( Enums.UserType), userType);

            return member;
        }

        public    DZMembership GetWechatUserInfo(string code, string appName, IRepositoryDZMembership repositoryMembership, string userType)
        {
            string AppIDWeChat = Dicts.AppIDWeChat;
            string AppSecretWeChat = Dicts.AppSecretWeChat;
            string urlWechat = Dianzhu.Config.Config.GetAppSetting("WeChatAccessTokenUrl");
            Uri uriWechat = new Uri(urlWechat + "appid=" + AppIDWeChat + "&secret=" + AppSecretWeChat + "&code=" + code + "&grant_type=authorization_code");

            string resultTokenWechat = HttpHelper.CreateHttpRequest(uriWechat.ToString(), "get", null);
            /*string result = "{\"access_token\":\"OezXcEiiBSKSxW0eoylIeNg8viCgEshRQb_2LQaXG7SDEtM3VwuCOs5Wk24ne5F7kI2Cd7SJC0T_OelFMa3t_jPMt9OHavJzMj1Ud_JgfnOHx3b6RqpcP5VLkFK4k1yEPOHFGc7rFVS6FNuPAWoX1g\",
             * \"expires_in\":7200,\"refresh_token\":\"OezXcEiiBSKSxW0eoylIeNg8viCgEshRQb_2LQaXG7SDEtM3VwuCOs5Wk24ne5F7aUteJDNwrhQLqqpTsxoOUZe-4QDQH3j6ouc1sOhLa9HaPATGaRVlfjuP8HeEoSJT9gP9fkLCrAOhStlqkGoOkw\",
             * \"openid\":\"of-miv5zbNsvYJzYOD9K5KSVBpds\",
             * \"scope\":\"snsapi_userinfo\",\"unionid\":\"oCXLmwRu-1igJ9c2O4x9mF4NDelc\"}";*/
            WechatRespTokenObj tokenObjWeChat = JsonConvert.DeserializeObject<WechatRespTokenObj>(resultTokenWechat);

            if (tokenObjWeChat.access_token != null)
            {
                string urlRefreshWeChat = Dianzhu.Config.Config.GetAppSetting("WeChatRefreshTokenUrl");
                Uri uriRefreshWeChat = new Uri(urlRefreshWeChat + "appid=" + AppIDWeChat + "&grant_type=refresh_token&refresh_token=" + tokenObjWeChat.refresh_token);
                string resultRefreshWeChat = HttpHelper.CreateHttpRequest(uriRefreshWeChat.ToString(), "get", null);
                WechatRespTokenObj refreshTokenObjWeChat = JsonConvert.DeserializeObject<WechatRespTokenObj>(resultRefreshWeChat);


                string urlUserWeChat = Dianzhu.Config.Config.GetAppSetting("WeChatUserInfoUrl");
                Uri uriUserWeChat = new Uri(urlUserWeChat + "access_token=" + refreshTokenObjWeChat.access_token + "&openid=" + refreshTokenObjWeChat.openid);
                string resultuser = HttpHelper.CreateHttpRequest(uriUserWeChat.ToString(), "get", null);
                WechatRespUserinfo userObjWeChat = JsonConvert.DeserializeObject<WechatRespUserinfo>(HttpUtility.UrlDecode(resultuser, System.Text.Encoding.UTF8));

                bool isAddWechat;
                DZMembershipWeChat wechatMember = ConvertToWechatMember(refreshTokenObjWeChat, userObjWeChat, out isAddWechat, repositoryMembership, userType);
                if (isAddWechat)
                {
                    repositoryMembership.Add(wechatMember);
                    wechatMember.UserName = wechatMember.Id.ToString();
                 
                }
                else
                {
                    //bllMember.UpdateUserForU3rd(wechatMember);
                }
                return wechatMember;
            }
            else
            {
                throw new Exception("传入code有误，请重新上传！");
            }
        }

        private   DZMembershipWeChat ConvertToWechatMember(WechatRespTokenObj tokenObj, WechatRespUserinfo userObj, out bool isAdd, IRepositoryDZMembership repositoryMembership, string userType)
        {
            isAdd = false;

            DZMembershipWeChat member = (DZMembershipWeChat)repositoryMembership.GetMemberByWechatOpenId(tokenObj.openid);
            if (member == null)
            {
                member = (DZMembershipWeChat)DZMembership.Create( Enums.LoginType.WeChat);
                isAdd = true;
            }
            else
            {
                return member;
            }
            //DZMembershipWeChat member =(DZMembershipWeChat)DZMembership.Create(enum_LoginType.WeChat);

            member.AccessToken = tokenObj.access_token;
            member.ExpiresIn = tokenObj.expires_in;
            member.RefreshToken = tokenObj.refresh_token;
            member.WeChatOpenId = tokenObj.openid;
            member.Scope = tokenObj.scope;
            member.Unionid = userObj.unionid;
            member.Nickname = userObj.nickname;
            member.Sex = userObj.sex;
            member.Province = userObj.province;
            member.City = userObj.city;
            member.Country = userObj.country;
            member.Headimgurl = userObj.headimgurl;

            member.NickName = userObj.nickname;
            member.UserName = userObj.nickname + new Guid();//防止重复
            if (string.IsNullOrEmpty(userObj.headimgurl))
            {
                member.AvatarUrl = "";
            }
            else
            {
                member.AvatarUrl =avatarDownloader.DownloadToMediaserver(userObj.headimgurl, string.Empty, "image");
            }
            member.Address = userObj.province + " " + userObj.city;
            member.PlainPassword = userObj.openid;
            member.Password = encryptService.GetMD5Hash(userObj.openid);
            member.UserType = ( UserType)Enum.Parse(typeof( UserType), userType);

            return member;

        }
    }
}
