using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Net;
using Dianzhu.Model.Enums;
using Newtonsoft.Json;
using PHSuit;
using System.Collections.Specialized;
using System.Web.Security;
using Dianzhu.Api.Model;
/// <summary>
/// Summary description for U3RD014008
/// </summary>
public class ResponseU3RD014008:BaseResponse
{
    DZMembershipProvider bllMember;

    public ResponseU3RD014008(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataU3RD014008 requestData = this.request.ReqData.ToObject<ReqDataU3RD014008>();

        bllMember = new DZMembershipProvider();

        try
        {
            string code = requestData.code;
            string pwd = "";
            RespDataUSM_userObj userObj = new RespDataUSM_userObj();
            switch (requestData.target)
            {
                case "WeChat":
                    string AppIDWeChat = Dicts.AppIDWeChat;
                    string AppSecretWeChat = Dicts.AppSecretWeChat;
                    string urlWechat = Dianzhu.Config.Config.GetAppSetting("WeChatAccessTokenUrl");
                    Uri uriWechat = new Uri(urlWechat + "appid=" + AppIDWeChat + "&secret=" + AppSecretWeChat + "&code=" + code+ "&grant_type=authorization_code");
                    
                    string resultTokenWechat = HttpHelper.CreateHttpRequest(uriWechat.ToString(), "get", null);
                    //string result = "{\"access_token\":\"OezXcEiiBSKSxW0eoylIeNg8viCgEshRQb_2LQaXG7SDEtM3VwuCOs5Wk24ne5F7kI2Cd7SJC0T_OelFMa3t_jPMt9OHavJzMj1Ud_JgfnOHx3b6RqpcP5VLkFK4k1yEPOHFGc7rFVS6FNuPAWoX1g\",\"expires_in\":7200,\"refresh_token\":\"OezXcEiiBSKSxW0eoylIeNg8viCgEshRQb_2LQaXG7SDEtM3VwuCOs5Wk24ne5F7aUteJDNwrhQLqqpTsxoOUZe-4QDQH3j6ouc1sOhLa9HaPATGaRVlfjuP8HeEoSJT9gP9fkLCrAOhStlqkGoOkw\",\"openid\":\"of-miv5zbNsvYJzYOD9K5KSVBpds\",\"scope\":\"snsapi_userinfo\",\"unionid\":\"oCXLmwRu-1igJ9c2O4x9mF4NDelc\"}";
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
                        WechatRespUserinfo userObjWeChat = JsonConvert.DeserializeObject<WechatRespUserinfo>(resultuser);

                        DZMembershipWeChat wechatMember = ConvertToWechatMember(refreshTokenObjWeChat, userObjWeChat);
                        bllMember.CreateUserForU3rd(wechatMember);

                        userObj.Adapt(wechatMember);
                        pwd = wechatMember.PlainPassword;
                    }
                    else
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "传入code有误，请重新上传！";
                        return;
                    }                    

                    break;
                case "SinaWeiBo":
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

                        DZMembershipSinaWeibo sinaWeiboMember = ConvertToSinaWeiboMember(respTokenObjWeibo, tokeninfoWeibo, userinfoWeibo);
                        bllMember.CreateUserForU3rd(sinaWeiboMember);

                        userObj.Adapt(sinaWeiboMember);
                        pwd = sinaWeiboMember.PlainPassword;
                    }
                    else
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "传入code有误，请重新上传！";
                        return;
                    }          

                    break;
                case "TencentQQ":
                    string urlQQOpenid = Dianzhu.Config.Config.GetAppSetting("QQOpenidUrl");
                    Uri uriQQOpenid = new Uri(urlQQOpenid + "access_token=" + code);
                    string resultOpenidQQCallback = HttpHelper.CreateHttpRequest(uriQQOpenid.ToString(), "get", null);
                    string resultOpenidQQ = resultOpenidQQCallback.Substring(9, resultOpenidQQCallback.LastIndexOf(")") - 9);
                    QQRespOpenid refreshOpenidObj = JsonConvert.DeserializeObject<QQRespOpenid>(resultOpenidQQ);

                    if (refreshOpenidObj.openid != null)
                    {
                        string urlQQUsrinfo = Dianzhu.Config.Config.GetAppSetting("QQUserInfoUrl");
                        Uri uriQQUserinfo = new Uri(urlQQUsrinfo + "access_token=" + code + "&oauth_consumer_key=" + Dicts.AppIDQQ + "&openid=" + refreshOpenidObj.openid + "&format=json");
                        string resultUserinfoQQ = HttpHelper.CreateHttpRequest(uriQQUserinfo.ToString(), "get", null);
                        QQRespUserinfo UserinfoObjQQ = JsonConvert.DeserializeObject<QQRespUserinfo>(resultUserinfoQQ);

                        DZMembershipQQ qqMember = ConvertToQQMember(refreshOpenidObj, UserinfoObjQQ);
                        if (UserinfoObjQQ.ret < 0)
                        {
                            this.state_CODE = Dicts.StateCode[0];
                            this.err_Msg = "ret:" + UserinfoObjQQ.ret + ";msg:" + UserinfoObjQQ.msg;
                            return;
                        }
                        bllMember.CreateUserForU3rd(qqMember);

                        userObj.Adapt(qqMember);
                        pwd = qqMember.PlainPassword;
                    }
                    else
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "传入code有误，请重新上传！";
                        return;
                    }

                    break;
                default:
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "传入类型有误，请重新上传！";
                    return;
            }

            RespData_userObj respObj = new RespData_userObj();
            respObj.userObj = userObj;
            respObj.pWord = pwd;
            this.state_CODE = Dicts.StateCode[0];
            this.RespData = respObj;
            return;
        }
        catch (Exception ex)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = ex.Message;
            return;
        }
    }

    private DZMembershipWeChat ConvertToWechatMember(WechatRespTokenObj tokenObj, WechatRespUserinfo userObj)
    {
        DZMembershipWeChat member = (DZMembershipWeChat)bllMember.GetUserByWechatOpenId(tokenObj.openid);
        if (member == null)
        {
            member = (DZMembershipWeChat)DZMembership.Create(enum_LoginType.WeChat);
        }
        else
        {
            return member;
        }
        //DZMembershipWeChat member =(DZMembershipWeChat)DZMembership.Create(enum_LoginType.WeChat);

        member.AccessToken = tokenObj.access_token;
        member.ExpiresIn = tokenObj.expires_in;
        member.RefreshToken = tokenObj.refresh_token;
        member.OpenId = tokenObj.openid;
        member.Scope = tokenObj.scope;
        member.Unionid = userObj.unionid;
        member.Nickname = userObj.nickname;
        member.Sex = userObj.sex;
        member.Province = userObj.province;
        member.City = userObj.city;
        member.Country = userObj.country;
        member.Headimgurl = userObj.headimgurl;

        member.NickName = userObj.nickname;
        member.UserName = userObj.nickname;
        member.AvatarUrl = DownloadToMediaserver(userObj.headimgurl);
        member.Address = userObj.province + " " + userObj.city;
        member.PlainPassword = userObj.openid;
        member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(userObj.openid, "MD5");
        member.UserType = enum_UserType.customer.ToString();

        return member;
    }    

    private DZMembershipSinaWeibo ConvertToSinaWeiboMember(WeiboRespTokenObj tokenObj, WeiboRequestTokenInfo tokenInfo, WeiboRespUserinfo userObj)
    {
        DZMembershipSinaWeibo member = (DZMembershipSinaWeibo)bllMember.GetUserBySinaWeiboUId(long.Parse(tokenInfo.uid));
        if (member == null)
        {
            member = (DZMembershipSinaWeibo)DZMembership.Create(enum_LoginType.SinaWeiBo);
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
        member.UserName = userObj.screen_name;
        member.AvatarUrl = DownloadToMediaserver(userObj.avatar_hd);
        member.Address = userObj.location;
        member.PlainPassword = tokenInfo.uid;
        member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(tokenInfo.uid, "MD5");
        member.UserType = enum_UserType.customer.ToString();

        return member;
    }

    private DZMembershipQQ ConvertToQQMember(QQRespOpenid openidObj, QQRespUserinfo userObj)
    {
        DZMembershipQQ member = (DZMembershipQQ)bllMember.GetUserByQQOpenId(openidObj.openid);
        if (member == null)
        {
            member = (DZMembershipQQ)DZMembership.Create(enum_LoginType.TencentQQ);
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
        member.UserName = userObj.nickname;
        if (userObj.figureurl_qq_2 != "")
        {
            member.AvatarUrl = DownloadToMediaserver(userObj.figureurl_qq_2);
        }
        else
        {
            member.AvatarUrl = DownloadToMediaserver(userObj.figureurl_qq_1);
        }        
        member.Address = userObj.province + " " + userObj.city;
        member.PlainPassword = openidObj.openid;
        member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(openidObj.openid, "MD5");
        member.UserType = enum_UserType.customer.ToString();

        return member;
    }

    private string DownloadToMediaserver(string fileUrl)
    {
        string url = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
        var respData = new NameValueCollection();
        respData.Add("fileUrl", HttpUtility.UrlEncode(fileUrl));
        respData.Add("originalName", string.Empty);
        respData.Add("domainType", "UserAvatar");
        respData.Add("fileType", "image");

        return HttpHelper.CreateHttpRequest(url.ToString(), "post", respData);
    }
}