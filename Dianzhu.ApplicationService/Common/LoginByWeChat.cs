using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Newtonsoft.Json;
using PHSuit;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
namespace Dianzhu.ApplicationService
{
    /*
    public class LoginByWeChat
    {
        public static Model.DZMembership GetUserInfo(string code,string appName,IDZMembershipService memberService,string userType)
        {
            string AppIDWeChat = Dicts.AppIDWeChat;
            string AppSecretWeChat = Dicts.AppSecretWeChat;
            string urlWechat = Dianzhu.Config.Config.GetAppSetting("WeChatAccessTokenUrl");
            Uri uriWechat = new Uri(urlWechat + "appid=" + AppIDWeChat + "&secret=" + AppSecretWeChat + "&code=" + code + "&grant_type=authorization_code");

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
                WechatRespUserinfo userObjWeChat = JsonConvert.DeserializeObject<WechatRespUserinfo>(HttpUtility.UrlDecode(resultuser, System.Text.Encoding.UTF8));

                bool isAddWechat;
                DZMembershipWeChat wechatMember = ConvertToWechatMember(refreshTokenObjWeChat, userObjWeChat, out isAddWechat, bllMember, userType);
                if (isAddWechat)
                {
                    memberService.CreateUserForU3rd(wechatMember);
                    wechatMember.UserName = wechatMember.Id.ToString();
                    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
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

        private static DZMembershipWeChat ConvertToWechatMember(WechatRespTokenObj tokenObj, WechatRespUserinfo userObj, out bool isAdd, BLL.DZMembershipProvider bllMember, string userType)
        {
            isAdd = false;

            DZMembershipWeChat member = (DZMembershipWeChat)bllMember.GetUserByWechatOpenId(tokenObj.openid);
            if (member == null)
            {
                member = (DZMembershipWeChat)DZMembership.Create(enum_LoginType.WeChat);
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
            member.UserName = userObj.nickname+new Guid();//防止重复
            if (string.IsNullOrEmpty(userObj.headimgurl))
            {
                member.AvatarUrl = "";
            }
            else
            {
                member.AvatarUrl = utils.DownloadToMediaserver(userObj.headimgurl, string.Empty, "image");
            }
            member.Address = userObj.province + " " + userObj.city;
            member.PlainPassword = userObj.openid;
            member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(userObj.openid, "MD5");
            member.UserType = (enum_UserType)Enum.Parse(typeof(enum_UserType), userType); 

            return member;
            
        }
    }*/
}
