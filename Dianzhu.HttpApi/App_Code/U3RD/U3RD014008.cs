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
            switch (requestData.target)
            {
                case "WeChat":
                    string AppIDWeChat = Dicts.AppIDWeChat;
                    string AppSecretWeChat = Dicts.AppSecretWeChat;
                    string urlWechat = Dianzhu.Config.Config.GetAppSetting("WeChatAccessTokenUrl");
                    Uri uriWechat = new Uri(urlWechat + "appid=" + AppIDWeChat + "&secret=" + AppSecretWeChat + "&code=" + code+ "&grant_type=authorization_code");
                    
                    //string result = HttpHelper.CreateHttpRequest(uriWechat.ToString(), "get", null);
                    string result = "{\"access_token\":\"OezXcEiiBSKSxW0eoylIeNg8viCgEshRQb_2LQaXG7SDEtM3VwuCOs5Wk24ne5F7kI2Cd7SJC0T_OelFMa3t_jPMt9OHavJzMj1Ud_JgfnOHx3b6RqpcP5VLkFK4k1yEPOHFGc7rFVS6FNuPAWoX1g\",\"expires_in\":7200,\"refresh_token\":\"OezXcEiiBSKSxW0eoylIeNg8viCgEshRQb_2LQaXG7SDEtM3VwuCOs5Wk24ne5F7aUteJDNwrhQLqqpTsxoOUZe-4QDQH3j6ouc1sOhLa9HaPATGaRVlfjuP8HeEoSJT9gP9fkLCrAOhStlqkGoOkw\",\"openid\":\"of-miv5zbNsvYJzYOD9K5KSVBpds\",\"scope\":\"snsapi_userinfo\",\"unionid\":\"oCXLmwRu-1igJ9c2O4x9mF4NDelc\"}";
                    WechatRespTokenObj tokenObj = JsonConvert.DeserializeObject<WechatRespTokenObj>(result);

                    string urlRefresh = Dianzhu.Config.Config.GetAppSetting("WeChatRefreshTokenUrl");
                    Uri uriRefresh = new Uri(urlRefresh + "appid=" + AppIDWeChat + "&grant_type=refresh_token&refresh_token=" + tokenObj.refresh_token);
                    string resultRefresh = HttpHelper.CreateHttpRequest(uriRefresh.ToString(), "get", null);
                    WechatRespTokenObj refreshTokenObj = JsonConvert.DeserializeObject<WechatRespTokenObj>(resultRefresh);


                    string urlUser = Dianzhu.Config.Config.GetAppSetting("WeChatUserInfoUrl");
                    Uri uriUser = new Uri(urlUser + "access_token=" + refreshTokenObj.access_token + "&openid=" + refreshTokenObj.openid);
                    string resultuser = HttpHelper.CreateHttpRequest(uriUser.ToString(), "get", null);
                    WechatRespUserinfo userObj = JsonConvert.DeserializeObject<WechatRespUserinfo>(resultuser);

                    DZMembershipWeChat wechatMember = ConvertToWechatMember(refreshTokenObj, userObj);
                    bllMember.CreateUserForU3rd(wechatMember);

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

                    string urlWeoboToken = Dianzhu.Config.Config.GetAppSetting("WeiboTokenUrl");
                    var respDataTokenWeibo = new NameValueCollection();
                    respDataTokenWeibo.Add("access_token", respTokenObjWeibo.access_token);
                    string resultTokenWeibo = HttpHelper.CreateHttpRequest(urlWeoboToken.ToString(), "post", respDataTokenWeibo);
                    WeiboRequestTokenInfo tokeninfoWeibo = JsonConvert.DeserializeObject<WeiboRequestTokenInfo>(resultTokenWeibo);

                    string uidWeibo = tokeninfoWeibo.uid;
                    //string screenName = //用户昵称,  参数uid与screen_name二者必选其一，且只能选其一
                    string urlWeiboUserinfo = Dianzhu.Config.Config.GetAppSetting("WeiboUserUrl");
                    Uri uriWeiboUserinfo = new Uri(urlWeiboUserinfo + "access_token=" + respTokenObjWeibo.access_token + "&uid=" + uidWeibo);
                    string resultUserinfoWeibo= HttpHelper.CreateHttpRequest(uriWeiboUserinfo.ToString(), "get", null);
                    WeiboRespUserinfo userinfoWeibo = JsonConvert.DeserializeObject<WeiboRespUserinfo>(resultUserinfoWeibo);

                    DZMembershipSinaWeibo sinaWeiboMember = ConvertToSinaWeiboMember(respTokenObjWeibo, tokeninfoWeibo, userinfoWeibo);
                    bllMember.CreateUserForU3rd(sinaWeiboMember);

                    break;
                case "TencentQQ":
                    string urlQQOpenid = Dianzhu.Config.Config.GetAppSetting("QQOpenidUrl");
                    Uri uriQQOpenid = new Uri(urlQQOpenid + "access_token=" + code);
                    string resultOpenidQQCallback = HttpHelper.CreateHttpRequest(uriQQOpenid.ToString(), "get", null);
                    string resultOpenidQQ = resultOpenidQQCallback.Substring(9, resultOpenidQQCallback.LastIndexOf(")") - 9);
                    QQRespOpenid refreshOpenidObj = JsonConvert.DeserializeObject<QQRespOpenid>(resultOpenidQQ);

                    string urlQQUsrinfo = Dianzhu.Config.Config.GetAppSetting("QQUserInfoUrl");
                    Uri uriQQUserinfo = new Uri(urlQQUsrinfo + "access_token=" + code + "&oauth_consumer_key=" + Dicts.AppIDQQ + "&openid=" + refreshOpenidObj.openid + "&format=json");
                    string resultUserinfoQQ = HttpHelper.CreateHttpRequest(uriQQUserinfo.ToString(), "get", null);
                    QQRespUserinfo UserinfoObjQQ= JsonConvert.DeserializeObject<QQRespUserinfo>(resultUserinfoQQ);

                    DZMembershipQQ qqMember = ConvertToQQMember(refreshOpenidObj, UserinfoObjQQ);
                    if (UserinfoObjQQ.ret < 0)
                    {
                        this.state_CODE = Dicts.StateCode[0];
                        this.err_Msg = "ret:" + UserinfoObjQQ.ret + ";msg:" + UserinfoObjQQ.msg;
                        return;
                    }
                    bllMember.CreateUserForU3rd(qqMember);

                    break;
                default:
                    break;
            }

            this.state_CODE = Dicts.StateCode[0];
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
        //DZMembershipWeChat member =(DZMembershipWeChat)DZMembership.Create(enum_LoginType.WeChat);

        member.AccessToken = tokenObj.access_token;
        member.ExpiresIn = tokenObj.expires_in;
        member.RefreshToken = tokenObj.refresh_token;
        member.OpenId = tokenObj.openid;
        member.Scope = tokenObj.scope;
        member.Unionid = userObj.unionid;
        member.NickName = userObj.nickname;
        member.Sex = userObj.sex;
        member.Province = userObj.province;
        member.City = userObj.city;
        member.Country = userObj.country;

        member.Headimgurl = userObj.headimgurl;

        return member;
    }

    private DZMembershipQQ ConvertToQQMember(QQRespOpenid openidObj, QQRespUserinfo userObj)
    {
        DZMembershipQQ member = (DZMembershipQQ)bllMember.GetUserByQQOpenId(openidObj.openid);
        if (member == null)
        {
            member = (DZMembershipQQ)DZMembership.Create(enum_LoginType.TencentQQ);
        }
        //DZMembershipWeChat member =(DZMembershipWeChat)DZMembership.Create(enum_LoginType.WeChat);

        member.ClientId = openidObj.client_id;
        member.OpenId = openidObj.openid;
        member.Ret = userObj.ret;
        member.Msg = userObj.msg;
        member.NickName = userObj.nickname;
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

        return member;
    }

    private DZMembershipSinaWeibo ConvertToSinaWeiboMember(WeiboRespTokenObj tokenObj, WeiboRequestTokenInfo tokenInfo, WeiboRespUserinfo userObj)
    {
        DZMembershipSinaWeibo member = (DZMembershipSinaWeibo)bllMember.GetUserBySinaWeiboUId(long.Parse(tokenInfo.uid));
        if (member == null)
        {
            member = (DZMembershipSinaWeibo)DZMembership.Create(enum_LoginType.SinaWeiBo);
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

        return member;
    }
}

public class ReqDataU3RD014008
{
    public string target { get; set; }
    public string code { get; set; }
}

#region 微信常见返回对象数据结构

#region 微信请求token
public class WechatRespTokenObj
{
    public string access_token { get; set; }//接口调用凭证
    public int expires_in { get; set; }//access_token接口调用凭证超时时间，单位（秒）
    public string refresh_token { get; set; }//用户刷新access_token
    public string openid { get; set; }//授权用户唯一标识
    public string scope { get; set; }//用户授权的作用域，使用逗号（,）分隔
    public string unionid { get; set; }//当且仅当该移动应用已获得该用户的userinfo授权时，才会出现该字段
    //public WechatRespTokenObj Adapt(WechatMember member)
    //{
    //    this.access_token = member.AccessToken;
    //    this.expires_in = member.ExpiresIn;
    //    this.refresh_token = member.RefreshToken;
    //    this.openid = member.OpenId;
    //    this.scope = member.Scope;
    //    this.unionid = member.Unionid;

    //    return this;
    //}
}
#endregion 

#region 微信用户数据
public class WechatRespUserinfo
{
    public string openid { get; set; }//普通用户的标识，对当前开发者帐号唯一
    public string nickname { get; set; }//普通用户昵称
    public int sex { get; set; }//普通用户性别，1为男性，2为女性
    public string province { get; set; }//普通用户个人资料填写的省份
    public string city { get; set; }//普通用户个人资料填写的城市
    public string country { get; set; }//国家，如中国为CN
    public string headimgurl { get; set; }//用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
    public Array[] privilege { get; set; }//用户特权信息，json数组，如微信沃卡用户为（chinaunicom）
    public string unionid { get; set; }//用户统一标识。针对一个微信开放平台帐号下的应用，同一用户的unionid是唯一的。
}
#endregion 

#endregion 

#region 微博常见返回对象数据结构

#region 微博请求token
public class WeiboRespTokenObj
{
    public string access_token { get; set; }//用于调用access_token，接口获取授权后的access token。
    public string remind_in { get; set; }//access_token的生命周期（该参数即将废弃，开发者请使用expires_in）。
    public string expires_id { get; set; }//access_token的生命周期，单位是秒数。
    public string refresh_token { get; set; }//刷新access_token
    public string uid { get; set; }//当前授权用户的UID。
}
#endregion

#region 微博查询token信息

public class WeiboRequestTokenInfo
{
    public string uid { get; set; }//授权用户的uid。
    public string appkey { get; set; }//access_token所属的应用appkey。
    public string scope { get; set; }//用户授权的scope权限。
    public string create_at { get; set; }//access_token的创建时间，从1970年到创建时间的秒数。
    public string expire_in { get; set; }//access_token的剩余时间，单位是秒数。
}

#endregion

#region 微博用户（user）
public class WeiboRespUserinfo
{
    public long id { get; set; }//用户UID
    public string idstr { get; set; }//字符串型的用户UID
    public string screen_name { get; set; }//用户昵称
    public string name { get; set; }//友好显示名称
    public int province { get; set; }//用户所在省级ID
    public int city { get; set; }//用户所在城市ID
    public string location { get; set; }//用户所在地
    public string description { get; set; }//用户个人描述
    public string url { get; set; }//用户博客地址
    public string profile_image_url { get; set; }//用户头像地址（中图），50×50像素
    public string profile_url { get; set; }//用户的微博统一URL地址
    public string domain { get; set; }//用户的个性化域名
    public string weihao { get; set; }//用户的微号
    public string gender { get; set; }//性别，m：男、f：女、n：未知
    public int followers_count { get; set; }//粉丝数
    public int friends_count { get; set; }//关注数
    public int statuses_count { get; set; }//微博数
    public int favourites_count { get; set; }//收藏数
    public string created_at { get; set; }//用户创建（注册）时间
    public bool following { get; set; }//暂未支持
    public bool allow_all_act_msg { get; set; }//是否允许所有人给我发私信，true：是，false：否
    public bool geo_enabled { get; set; }//是否允许标识用户的地理位置，true：是，false：否
    public bool verified { get; set; }//是否是微博认证用户，即加V用户，true：是，false：否
    public int verified_type { get; set; }//暂未支持
    public string remark { get; set; }//用户备注信息，只有在查询用户关系时才返回此字段
    public bool allow_all_comment { get; set; }//是否允许所有人对我的微博进行评论，true：是，false：否
    public string avatar_large { get; set; }//用户头像地址（大图），180×180像素
    public string avatar_hd { get; set; }//用户头像地址（高清），高清头像原图
    public string verified_reason { get; set; }//认证原因
    public bool follow_me { get; set; }//该用户是否关注当前登录用户，true：是，false：否
    public int online_status { get; set; }//用户的在线状态，0：不在线、1：在线
    public int bi_followers_count { get; set; }//用户的互粉数
    public string lang { get; set; }//用户当前的语言版本，zh-cn：简体中文，zh-tw：繁体中文，en：英语
}
#endregion

#endregion

#region QQ常见返回对象数据结构

#region QQ请求opid
public class QQRespOpenid
{
    public string client_id { get; set; } //appid
    public string openid { get; set; }//openid是此网站上唯一对应用户身份的标识
}
#endregion

#region QQ获取用户信息
public class QQRespUserinfo
{
    public int ret { get; set; }//返回码
    public string msg { get; set; }//如果ret<0，会有相应的错误信息提示，返回数据全部用UTF-8编码。
    public int is_lost { get; set; }//判断是否有数据丢失。如果应用不使用cache，不需要关心此参数。0或者不返回：没有数据丢失，可以缓存。1：有部分数据丢失或错误，不要缓存。
    public string nickname { get; set; } //用户在QQ空间的昵称。
    public string gender { get; set; } //性别。 如果获取不到则默认返回"男"
    public string province { get; set; } //省（当pf=qzone、pengyou或qplus时返回）。
    public string city { get; set; } //性市（当pf=qzone、pengyou或qplus时返回）。
    public string year { get; set; } //出生年份
    public string figureurl { get; set; } //大小为30×30像素的QQ空间头像URL。
    public string figureurl_1 { get; set; } //大小为50×50像素的QQ空间头像URL。
    public string figureurl_2 { get; set; } //大小为100×100像素的QQ空间头像URL。
    public string figureurl_qq_1 { get; set; } //大小为40×40像素的QQ头像URL。。
    public string figureurl_qq_2 { get; set; } //大小为100×100像素的QQ头像URL。需要注意，不是所有的用户都拥有QQ的100x100的头像，但40x40像素则是一定会有。    
    public string is_yellow_vip { get; set; } //标识用户是否为黄钻用户（0：不是；1：是）。
    public string vip { get; set; } //标识用户是否为黄钻用户（0：不是；1：是）
    public string yellow_vip_level { get; set; } //黄钻等级
    public string level { get; set; } //黄钻等级。
    public string is_yellow_year_vip { get; set; } //标识是否为年费黄钻用户（0：不是； 1：是）
}
#endregion

#endregion