using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
using AutoMapper;

namespace Dianzhu.ApplicationService.Client
{
    public class ClientService:IClientService
    {
        BLL.Client.IBLLClient ibllclient;
        BLL.Client.IBLLRefreshToken ibllrefreshtoken;
        BLL.Client.BLLUserToken bllusertoken = null;
        DZMembershipProvider dzmp = null;
        BLL.BLLStaff bllstaff = null;
        public ClientService(BLL.Client.BLLUserToken bllusertoken, DZMembershipProvider dzmp, BLL.BLLStaff bllstaff)
        {
            //this.ibllclient = ibllclient;
            //this.ibllrefreshtoken = ibllrefreshtoken;
            this.bllusertoken = bllusertoken;
            this.dzmp = dzmp;
            this.bllstaff = bllstaff;
        }

        /// <summary>
        /// 创建Token值
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="apiKey"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public UserTokentDTO CreateToken(string loginName, string password,string apiKey,string strPath)
        {
            if (!dzmp.ValidateUser(loginName, password))
            {
                //throw new Exception("用户名或密码错误！");
                throw new Exception("001002");
            }
            Model.DZMembership dzm = dzmp.GetUserByName(loginName);
            if (dzm == null)
            {
                dzm = dzmp.GetUserById(utils.CheckGuidID(loginName, "loginName"));
            }
            if (dzm == null)
            {
                throw new Exception("该用户不存在！");
            }
            string userUri = "";
            switch (dzm.UserType.ToString())
            {
                case "customer":
                    userUri = strPath+"/api/v1/customers/" +dzm.Id;
                    break;
                case "business":
                    userUri = strPath + "/api/v1/merchants/" + dzm.Id;
                    break;
                case "customerservice":
                    userUri = strPath + "/api/v1/CustomerServices/" + dzm.Id;
                    break;
                case "staff":
                    Model.Staff staff = bllstaff.GetOneByUserID(Guid.Empty, dzm.Id.ToString()) ;
                    userUri = strPath + "/api/v1/stores/" + staff.Belongto.Id + "/staffs/" + staff.Id;
                    break;
                default:
                    throw new Exception("用户类型不正确！");
            }
            Customer customer = new Customer();
            customer.loginName = loginName;
            customer.password = password;
            customer.UserType = dzm.UserType.ToString();
            customer.UserID = dzm.Id.ToString();
            UserTokentDTO usertokendto = new UserTokentDTO();
            usertokendto.userEndpoint = userUri;
            usertokendto.token= JWT.JsonWebToken.Encode(customer, apiKey, JWT.JwtHashAlgorithm.HS256);
            Model.UserToken usertoken = new Model.UserToken { UserID = dzm.Id.ToString(), Token = usertokendto.token, Flag = 1, CreatedTime = DateTime.Now };
            if (bllusertoken.addToken(usertoken))
            {
                throw new Exception("Token保存失败！");
            }
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Local);
            TimeSpan currentTs = DateTime.Now - epochStart;
            string requestTimeStamp = currentTs.TotalSeconds.ToString ();
            System.Runtime.Caching.MemoryCache.Default.Remove(dzm.Id.ToString());
            System.Runtime.Caching.MemoryCache.Default.Add(dzm.Id.ToString(), usertokendto.token, DateTimeOffset.UtcNow.AddSeconds(172800));
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
