using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
using AutoMapper;

using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.Application;
using Ydb.Common;

namespace Dianzhu.ApplicationService.Client
{
    public class ClientService:IClientService
    {
        BLL.Client.IBLLClient ibllclient;
        BLL.Client.IBLLRefreshToken ibllrefreshtoken;
        IUserTokenService userTokenService = null;
        
        IDZMembershipService memberService;
        IStaffService staffService;
        
        IMembershipLoginLogService membershipLoginLogService;

        public ClientService(IUserTokenService userTokenService, IStaffService staffService
            ,IDZMembershipService memberService, IMembershipLoginLogService membershipLoginLogService)
        {
            //this.ibllclient = ibllclient;
            //this.ibllrefreshtoken = ibllrefreshtoken;
            this.userTokenService = userTokenService;
        
            this.staffService = staffService;
            this.memberService = memberService;
            this.membershipLoginLogService = membershipLoginLogService;
        }

        /// <summary>
        /// 创建Token值
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="apiName"></param>
        /// <param name="apiKey"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public UserTokentDTO CreateToken(string loginName, string password, string apiName, string apiKey,string strPath,string appName)
        {

            log4net.ILog ilog = log4net.LogManager.GetLogger("Ydb.ClientController.NoRule.v1.RestfulApi.Web.Dianzhu");
            ValidateResult validateResult=   memberService.Login(loginName, password);
            
            if (!validateResult.IsValidated )
            {
                //throw new Exception("用户名或密码错误！");
                throw new Exception("001002");
            }
            //Model.DZMembership dzm = dzmp.GetUserByName(loginName);
            //if (dzm == null)
            //{
            //    dzm = dzmp.GetUserById(utils.CheckGuidID(loginName, "loginName"));
            //}
            //if (dzm == null)
            //{
            //    throw new Exception("该用户不存在！");
            //}
            MemberDto dzm = validateResult.ValidatedMember;


            //用户登录记录
            membershipLoginLogService.MemberLogin(dzm.Id.ToString(), "", (enum_appName)Enum.Parse(typeof(enum_appName), appName));

            string userUri = "";
            switch (dzm.UserType)
            {
                case "customer":
                    if (apiName != "UI3f4185e97b3E4a4496594eA3b904d60d" && apiName != "UA811Cd5343a1a41e4beB35227868541f8")
                    {
                        throw new Exception("用户类型不正确，不能登录该App！");
                    }
                    userUri = strPath+"/api/v1/customers/" +dzm.Id;
                    break;
                case "business":
                    if (apiName != "MI354d5aaa55Ff42fba7716C4e70e015f2" && apiName != "MAA6096436548346B0b70ffb58A9b0426d" && apiName != "ABc907a34381Cd436eBfed1F90ac8f823b")
                    {
                        throw new Exception("用户类型不正确，不能登录该App！");
                    }
                    userUri = strPath + "/api/v1/merchants/" + dzm.Id;
                    break;
                case "customerservice":
                    if (apiName != "CI5baFa6180f5d4b9D85026073884c3566" && apiName != "CA660838f88147463CAF3a52bae6c30cbd")
                    {
                        throw new Exception("用户类型不正确，不能登录该App！");
                    }
                    userUri = strPath + "/api/v1/CustomerServices/" + dzm.Id;
                    break;
                case "staff":
                    if (apiName != "MI354d5aaa55Ff42fba7716C4e70e015f2" && apiName != "MAA6096436548346B0b70ffb58A9b0426d")
                    {
                        throw new Exception("用户类型不正确，不能登录该App！");
                    }
             Ydb.BusinessResource.DomainModel. Staff staff = staffService.GetOneByUserID(Guid.Empty, dzm.Id.ToString()) ;
                    userUri = strPath + "/api/v1/stores/" + staff.Belongto.Id + "/staffs/" + staff.Id;
                    break;
                default:
                    throw new Exception("用户类型不正确，不能登录该App！");
            }
            Customer customer = new Customer();
            customer.loginName = loginName;
            customer.password = JWT.JsonWebToken.Encode(password, apiKey, JWT.JwtHashAlgorithm.HS256);
            customer.UserType = dzm.UserType.ToString();
            customer.UserID = dzm.Id.ToString();
            UserTokentDTO usertokendto = new UserTokentDTO();
            usertokendto.userEndpoint = userUri;
            usertokendto.token= JWT.JsonWebToken.Encode(customer, apiKey, JWT.JwtHashAlgorithm.HS256);

            //Model.UserToken usertoken = new Model.UserToken { UserID = dzm.Id.ToString(), Token = usertokendto.token, Flag = 1, CreatedTime = DateTime.Now };
            
            ilog.Debug("PostToken(Baegin1):" + dzm.Id.ToString() + "_" + loginName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            //if (bllusertoken.addToken(usertoken))
            //{
            //    throw new Exception("Token保存失败！");
            //}

            userTokenService.addToken(dzm.Id.ToString(), usertokendto.token, apiName);

            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Local);
            TimeSpan currentTs = DateTime.Now - epochStart;
            string requestTimeStamp = currentTs.TotalSeconds.ToString ();
            System.Runtime.Caching.MemoryCache.Default.Remove(dzm.Id.ToString());
            System.Runtime.Caching.MemoryCache.Default.Add(dzm.Id.ToString(), usertokendto.token, DateTimeOffset.UtcNow.AddSeconds(172800));
            //ilog.Debug("PostToken(End):" + loginName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            
            return usertokendto;
        }

        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
        public void RegisterClient(ClientDTO clientdto)
        {
            Model.Client client= Mapper.Map<ClientDTO, Model.Client>(clientdto);
            ibllclient.RegisterClient(client);
        }

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public ClientDTO FindClient(string clientId)
        {
            Model.Client client= ibllclient.FindClient(clientId);
            ClientDTO clientdto = Mapper.Map<Model.Client, ClientDTO>(client);
            return clientdto;
        }

        /// <summary>
        /// 添加新的RefreshToken
        /// </summary>
        /// <param name="token"></param>
        public bool AddRefreshToken(RefreshTokenDTO tokendto)
        {
            Model.RefreshToken token = Mapper.Map<RefreshTokenDTO, Model.RefreshToken>(tokendto);
            return ibllrefreshtoken.AddRefreshToken(token);
        }

        /// <summary>
        /// 根据Id删除RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        public void RemoveRefreshToken(string refreshTokenId)
        {
            ibllrefreshtoken.RemoveRefreshToken(refreshTokenId);
        }

        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="refreshtoken"></param>
        public void RemoveRefreshToken(RefreshTokenDTO tokendto)
        {
            Model.RefreshToken refreshtoken = Mapper.Map<RefreshTokenDTO, Model.RefreshToken>(tokendto);
            ibllrefreshtoken.RemoveRefreshToken(refreshtoken);
        }

        /// <summary>
        /// 根据Id获取RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public RefreshTokenDTO FindRefreshToken(string refreshTokenId)
        {
            Model.RefreshToken refreshtoken= ibllrefreshtoken.FindRefreshToken(refreshTokenId);
            RefreshTokenDTO tokendto = Mapper.Map< Model.RefreshToken, RefreshTokenDTO>(refreshtoken);
            return tokendto;
        }

        /// <summary>
        /// 获取所有的RefreshToken
        /// </summary>
        /// <returns></returns>
        public IList<RefreshTokenDTO> GetAllRefreshTokens()
        {
            IList < Model.RefreshToken > refreshtoken = ibllrefreshtoken.GetAllRefreshTokens();
            IList < RefreshTokenDTO> tokendto = Mapper.Map< IList<Model.RefreshToken>, IList< RefreshTokenDTO> >(refreshtoken);
            return tokendto;
        }

        public void Dispose()
        {
            //ibllclient.Dispose();
            //ibllrefreshtoken.Dispose();
        }
    }
}
