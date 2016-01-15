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

                    string urlWeoboToken = Dianzhu.Config.Config.GetAppSetting("WeiboTokenUrl");

                    var respDataWeibo = new NameValueCollection();
                    //respDataWeibo.Add("client_id", AppIDWeibo);
                    //respDataWeibo.Add("client_secret", AppSecretWeibo);
                    //respDataWeibo.Add("grant_type", "authorization_code");
                    //respDataWeibo.Add("code", code);
                    //respDataWeibo.Add("redirect_uri", urlWeiboBack);
                    respDataWeibo.Add("access_token", code);

                    string resultTokenWeibo = HttpHelper.CreateHttpRequest(urlWeoboToken.ToString(), "post", respDataWeibo);
                    WeiboRequestTokenInfo tokeninfoWeibo = JsonConvert.DeserializeObject<WeiboRequestTokenInfo>(resultTokenWeibo);

                    string uidWeibo = tokeninfoWeibo.uid;
                    //string screenName = //用户昵称,  参数uid与screen_name二者必选其一，且只能选其一
                    string urlWeiboUserinfo = Dianzhu.Config.Config.GetAppSetting("WeiboUserUrl");
                    Uri uriWeiboUserinfo = new Uri(urlWeiboUserinfo + "access_token=" + code + "&uid=" + uidWeibo);
                    string resultUserinfoWeibo= HttpHelper.CreateHttpRequest(uriWeiboUserinfo.ToString(), "get", null);
                    WeiboRespUserinfo userinfoWeibo = JsonConvert.DeserializeObject<WeiboRespUserinfo>(resultUserinfoWeibo);

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
public class WeiboRequestTokenObj
{
    public string client_id { get; set; }//申请应用时分配的AppKey。
    public string client_secret { get; set; }//申请应用时分配的AppSecret。
    public string grant_type { get; set; }//请求的类型，填写authorization_code
    public string code { get; set; }//调用authorize获得的code值。
    public string redirect_uri { get; set; }//回调地址，需需与注册应用里的回调地址一致。
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
    public WeiboStatus status { get; set; }//用户的最近一条微博信息字段
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

#region 微博（status）
public class WeiboStatus
{
    public long id { get; set; }//微博ID
    public long mid { get; set; }//微博MID
    public string created_at { get; set; }//微博创建时间
    public string idstr { get; set; }//字符串型的微博ID
    public string text { get; set; }//微博信息内容
    public string source { get; set; }//微博来源
    public bool favorited { get; set; }//是否已收藏，true：是，false：否
    public bool truncated { get; set; }//是否被截断，true：是，false：否
    public string in_reply_to_status_id { get; set; }//（暂未支持）回复ID
    public string in_reply_to_user_id { get; set; }//（暂未支持）回复人UID
    public string in_reply_to_screen_name { get; set; }//（暂未支持）回复昵称
    public string thumbnail_pic { get; set; }//缩略图片地址，没有时不返回此字段
    public string bmiddle_pic { get; set; }//中等尺寸图片地址，没有时不返回此字段
    public string original_pic { get; set; }//原始图片地址，没有时不返回此字段
    public WeiboGeo geo { get; set; }//地理信息字段
    public WeiboRespUserinfo user { get; set; }//微博作者的用户信息字段
    public WeiboStatus retweeted_status { get; set; }//被转发的原微博信息字段，当该微博为转发微博时返回
    public int reposts_count { get; set; }//转发数
    public int comments_count { get; set; }//评论数
    public int attitudes_count { get; set; }//表态数
    public int mlevel { get; set; }//暂未支持
    public object visible { get; set; }//微博的可见性及指定可见分组信息。该object中type取值，0：普通微博，1：私密微博，3：指定分组微博，4：密友微博；list_id为分组的组号
    public object pic_ids { get; set; }//微博配图ID。多图时返回多图ID，用来拼接图片url。用返回字段thumbnail_pic的地址配上该返回字段的图片ID，即可得到多个图片url。
    public object ad { get; set; }//微博流内的推广微博ID
}
#endregion

#region 微博地理信息（geo）
public class WeiboGeo
{
    public string longitude { get; set; }//经度坐标
    public string latitude { get; set; }//维度坐标
    public string city { get; set; }//所在城市的城市代码
    public string province { get; set; }//所在省份的省份代码
    public string city_name { get; set; }//所在城市的城市名称
    public string province_name { get; set; }//所在省份的省份名称
    public string address { get; set; }//所在的实际地址，可以为空
    public string pinyin { get; set; }//地址的汉语拼音，不是所有情况都会返回该字段
    public string more { get; set; }//更多信息，不是所有情况都会返回该字段
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