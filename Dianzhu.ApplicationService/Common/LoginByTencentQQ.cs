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

namespace Dianzhu.ApplicationService
{
    /*
    public class LoginByTencentQQ
    {
        public static Model.DZMembership GetUserInfo(string code, string appName, BLL.DZMembershipProvider bllMember, string userType)
        {
            string AppIDQQ = string.Empty;
            switch (appName.ToLower())
            {
                case "ios":
                    AppIDQQ = Dicts.AppIDQQIos;
                    break;
                case "android":
                    AppIDQQ = Dicts.AppIDQQAndroid;
                    break;
                default:
                    throw new Exception("传入appName有误，请重新上传！");
            }
            string urlQQOpenid = Dianzhu.Config.Config.GetAppSetting("QQOpenidUrl");
            Uri uriQQOpenid = new Uri(urlQQOpenid + "access_token=" + code);
            string resultOpenidQQCallback = HttpHelper.CreateHttpRequest(uriQQOpenid.ToString(), "get", null);
            string resultOpenidQQ = resultOpenidQQCallback.Substring(9, resultOpenidQQCallback.LastIndexOf(")") - 9);
            QQRespOpenid refreshOpenidObj = JsonConvert.DeserializeObject<QQRespOpenid>(resultOpenidQQ);

            if (refreshOpenidObj.openid != null)
            {
                string urlQQUsrinfo = Dianzhu.Config.Config.GetAppSetting("QQUserInfoUrl");
                Uri uriQQUserinfo = new Uri(urlQQUsrinfo + "access_token=" + code + "&oauth_consumer_key=" + AppIDQQ + "&openid=" + refreshOpenidObj.openid + "&format=json");
                string resultUserinfoQQ = HttpHelper.CreateHttpRequest(uriQQUserinfo.ToString(), "get", null);
                QQRespUserinfo UserinfoObjQQ = JsonConvert.DeserializeObject<QQRespUserinfo>(resultUserinfoQQ);

                bool isAddQQ;
                DZMembershipQQ qqMember = ConvertToQQMember(refreshOpenidObj, UserinfoObjQQ, out isAddQQ, bllMember, userType);
                if (UserinfoObjQQ.ret < 0)
                {
                    throw new Exception("ret:" + UserinfoObjQQ.ret + ";msg:" + UserinfoObjQQ.msg);
                }
                if (isAddQQ)
                {
                    bllMember.CreateUserForU3rd(qqMember);
                    qqMember.UserName = qqMember.Id.ToString();
                    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
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

        private static DZMembershipQQ ConvertToQQMember(QQRespOpenid openidObj, QQRespUserinfo userObj, out bool isAdd, BLL.DZMembershipProvider bllMember, string userType)
        {
            isAdd = false;

            DZMembershipQQ member = (DZMembershipQQ)bllMember.GetUserByQQOpenId(openidObj.openid);
            if (member == null)
            {
                member = (DZMembershipQQ)DZMembership.Create(enum_LoginType.TencentQQ);
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
                member.AvatarUrl = utils.DownloadToMediaserver(userObj.figureurl_qq_1, string.Empty, "image");
            }
            else
            {
                member.AvatarUrl = utils.DownloadToMediaserver(userObj.figureurl_qq_2, string.Empty, "image");
            }
            
            member.Address = userObj.province + " " + userObj.city;
            member.PlainPassword = openidObj.openid;
            member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(openidObj.openid, "MD5");
            member.UserType = (enum_UserType)Enum.Parse(typeof(enum_UserType), userType);

            return member;
        }
    }*/
}
