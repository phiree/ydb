﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Configuration;
using AutoMapper;
using System.Text.RegularExpressions;
using Ydb.Common.Specification;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.Application.Dto;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common.Application;
using Ydb.Common;
using Ydb.Common.Domain;

using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.Common.Infrastructure;
namespace Dianzhu.ApplicationService.User
{
    public class UserService:IUserService
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Ydb.UserService.NoRule.v1.RestfulApi.Web.Dianzhu");
        IDZMembershipService memberService;
        IReceptionService receptionService;
        IAreaService areaService;
        IServiceOrderService bllServiceOrder;
        IHttpRequest httpRequest;
        public UserService(IDZMembershipService memberService,  IReceptionService receptionService, IServiceOrderService bllServiceOrder, IAreaService areaService,IHttpRequest httpRequest)
        {
            this.memberService = memberService;
            this.receptionService = receptionService;
            this.bllServiceOrder = bllServiceOrder;
            this.areaService = areaService;
            this.httpRequest = httpRequest;
        }

        /// <summary>
        /// 验证用户名密码验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUser(string username, string password)
        {
             var result= memberService.ValidateUser(username, password,false);
            return result.IsValidated;
        }

        /// <summary>
        /// 根据userID获取user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public customerObj GetUserById(string userID,string userType)
        {
         MemberDto member =memberService .GetUserById(utils.CheckGuidID(userID, "userID").ToString());
            customerObj userobj = null;
            if (member.UserType == userType)//customer=1"customer"
            {
                userobj = Mapper.Map<MemberDto, customerObj>(member);
            }
            //if (userobj == null)
            //{
            //    throw new Exception(Dicts.StateCode[4]);
            //}
            return userobj;
        }

        /// <summary>
        /// 根据用户信息获取user
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="userFilter"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public IList<customerObj> GetUsers(common_Trait_Filtering filter,common_Trait_UserFiltering userFilter, string userType)
        {
            TraitFilter filter1 = utils.CheckFilter(filter, "DZMembership");
            IList<MemberDto> dzm = memberService.GetUsers(filter1, userFilter.alias, userFilter.email, userFilter.phone, userFilter.platform, userType);
            if (dzm == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<customerObj>();
            }
            IList<customerObj> customerobj = Mapper.Map<IList<MemberDto>, IList<customerObj>>(dzm);
            return customerobj;
        }

        /// <summary>
        /// 统计用户数量
        /// </summary>
        /// <param name="userFilter"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public countObj GetUsersCount(common_Trait_UserFiltering userFilter, string userType)
        {
            countObj c = new countObj();
            c.count = memberService.GetUsersCount(userFilter.alias, userFilter.email, userFilter.phone, userFilter.platform, userType).ToString();
            return c;
        }


        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="userBody"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public object PostUser(Common_Body userBody, string userType)
        {
            if (string.IsNullOrEmpty(userBody.phone) && string.IsNullOrEmpty(userBody.email))
            {
                throw new FormatException("手机号码或邮箱至少一个不能没空！");
            }
            if (string.IsNullOrEmpty(userBody.pWord))
            {
                throw new FormatException("密码不能没空！");
            }
            if (!string.IsNullOrEmpty(userBody.phone))
            {
                Regex reg = new Regex(@"^1[3578]\d{9}$");
                if (userBody.phone.Length != 11 || !reg.IsMatch(userBody.phone))
                {
                    throw new FormatException("手机号码格式错误！");
                }
            }
            System.Web.Security.MembershipCreateStatus createStatus;
            enum_UserType usertype = (enum_UserType)Enum.Parse(typeof(enum_UserType), userType);

          RegisterResult registerResult=    memberService.RegisterMember(userBody.phone, userBody.pWord, userBody.pWord, userType,
                System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority);

            if (!registerResult.RegisterSuccess)
            {
                throw new Exception(registerResult.RegisterErrMsg);
            }
             if (!registerResult.SendEmailSuccess)
            {
                throw new Exception(registerResult.SendEmailErrMsg);
            }
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //Dianzhu.Model.DZMembership dzm = dzmsp.GetUserById(newMember.Id);
            //if (dzm == null)
            //{
            //    throw new Exception("注册失败!");
            //}

            if (userType == "customer")
            {
                customerObj customerobj = Mapper.Map<MemberDto, customerObj>(registerResult.ResultObject);
                return customerobj;
            }
            else
            {
                merchantObj merchantobj = Mapper.Map<merchantObj>(registerResult.ResultObject);
                return merchantobj;
            }
        }

        /// <summary>
        /// 第三方用户注册
        /// </summary>
        /// <param name="u3rd_Model"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public object PostUser3rds(U3RD_Model u3rd_Model, string userType)
        {
            MemberDto newMember = memberService.Login3rd(u3rd_Model.platform, u3rd_Model.code, u3rd_Model.appName, userType);

              if (newMember == null)
            {
                throw new Exception("注册失败!");
            }

            if (userType == "customer")
            {
                customerObj customerobj = Mapper.Map<customerObj>(newMember);
                //客户端返回密码是不安全的行为,需要重新整理需求
                customerobj.pWord = newMember.PlainPassword;

                return customerobj;
            }
            else
            {
                merchantObj merchantobj = Mapper.Map<  merchantObj>(newMember);
            
                return merchantobj;
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userChangeBody"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public object PatchUser(string userID, UserChangeBody userChangeBody, string userType)
        {
            if (string.IsNullOrEmpty(userChangeBody.oldPassWord))
            {
                throw new FormatException("原密码不能为空！");
            }
            Guid guidUser = utils.CheckGuidID(userID, "userID");
            ValidateResult validateResult = memberService.ValidateUser(userID, userChangeBody.oldPassWord, false);

            if (!validateResult.IsValidated)
            {
                throw new Exception(validateResult.ValidateErrMsg);
            }

            MemberDto validatedUser = validateResult.ValidatedMember;
                
            if (validatedUser == null)
            {
                throw new Exception("该用户不存在！");
            }
            
            if (!string.IsNullOrEmpty(userChangeBody.alias))
            {
                memberService.ChangeAlias(userID, userChangeBody.alias);
            }
            if (!string.IsNullOrEmpty(userChangeBody.email))
            {

                memberService.ChangeEmail(userID, userChangeBody.email);
            }
            if (!string.IsNullOrEmpty(userChangeBody.phone))
            {
                memberService.ChangePhone(userID, userChangeBody.phone);
            }
            if (!string.IsNullOrEmpty(userChangeBody.address))
            {
               memberService.ChangeAddress(userID, userChangeBody.address);
            }
            if (!string.IsNullOrEmpty(userChangeBody.sex))
            {
                //dzm.Address = userChangeBody.sex;
                //if (userType == "customer")
                //{
                //    ()
                //}
            } 
            if (!string.IsNullOrEmpty(userChangeBody.imgUrl))
            {
                
                memberService.ChangeAvatar(userID, utils.GetFileName(userChangeBody.imgUrl, "MediaGetUrl"));
            }
            if (!string.IsNullOrEmpty(userChangeBody.newPassWord))
            {
                //todo: 需要完善

             ActionResult changePasswordResult=   memberService.ChangePassword(memberService.GetUserById(userID).UserName, userChangeBody.oldPassWord, userChangeBody.newPassWord);
                if (!changePasswordResult.IsSuccess)
                {
                    throw new Exception(changePasswordResult.ErrMsg);
                }
                System.Runtime.Caching.MemoryCache.Default.Remove(userID.ToString());
                //Model.UserToken usertoken=bllUserToken.GetToken(userID.ToString());
                //usertoken.Flag = 0;
                //Model.UserToken usertoken = new Model.UserToken { UserID = dzm.Id.ToString(), Token = usertokendto.token, Flag = 0, CreatedTime = DateTime.Now };
                //UserToken ut = usertoken.GetToken(member.Id.ToString());
                //ut.Flag = 0;
                //usertoken.Update(ut);
            }
            /* 领域内部验证.
            DateTime dt = DateTime.Now;
            dzm.LastLoginTime = dt;

            BLL.Validator.ValidatorDZMembership vd_member = new BLL.Validator.ValidatorDZMembership();
            FluentValidation.Results.ValidationResult result = vd_member.Validate(dzm);
            if (!result.IsValid)
            {
                string strErrors = "[";
                for (int i = 0; i < result.Errors.Count; i++)
                {
                    strErrors += "{";
                    strErrors += "ErrorCode:" + result.Errors[i].ErrorCode + ",";
                    strErrors += "ErrorMessage:" + result.Errors[i].ErrorMessage + "";
                    strErrors += "},";
                }
                strErrors.TrimEnd(',');
                strErrors += "]";
                throw new Exception(strErrors);
            }

            dzmsp.UpdateDZMembership(dzm);

            dzm = dzmsp.GetUserById(guidUser);
            if (dzm.LastLoginTime != dt)
            {
                throw new Exception("修改失败!");
            }
            */
            validatedUser = memberService.GetUserById(userID);
            if (userType == "customer")
            {
                customerObj customerobj = Mapper.Map<customerObj>(validatedUser);
                return customerobj;
            }
            else
            {
                merchantObj merchantobj = Mapper.Map<merchantObj>(validatedUser);
                return merchantobj;
            }

        }

        /// <summary>
        /// 用户忘记密码，修改用户密码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public object PatchPasswordForForget(string phone,string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                throw new Exception("密码不能为空!");
            }
            ActionResult actionResult = memberService.RecoveryPasswordByPhone(phone, newPassword);
            if (!actionResult.IsSuccess)
            {
                throw new Exception(actionResult.ErrMsg);
            }

            //System.Runtime.Caching.MemoryCache.Default.Remove(dzm.Id.ToString());
            //Model.UserToken usertoken = bllUserToken.GetToken(dzm.Id.ToString());
            //if (usertoken != null)
            //{
            //    usertoken.Flag = 0;
            //}
            return new string[] { "修改成功" };

        }

        /// <summary>
        /// 修改用户城市信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public object PatchCurrentGeolocation(string userID, common_Trait_LocationFiltering cityCode, Customer customer)
        {
            Guid guidUser = utils.CheckGuidID(userID, "userID");
            Area area= areaService.GetCityByAreaCode(cityCode.code);
            if (!string.IsNullOrEmpty(cityCode.longitude) && !string.IsNullOrEmpty(cityCode.latitude))
            {
                RespGeo geoObj = utils.Deserialize<RespGeo>(utils.GetCity(cityCode.longitude, cityCode.latitude));
                area = areaService.GetCityByAreaCode(geoObj.result.addressComponent.adcode);
                if (area == null)
                {
                    area = areaService.GetAreaByAreaname(geoObj.result.addressComponent.province + geoObj.result.addressComponent.city + geoObj.result.addressComponent.district);
                }
                if (area == null)
                {
                    area = areaService.GetAreaByAreaname(geoObj.result.addressComponent.province + geoObj.result.addressComponent.city );
                }
            }
            if (area == null)
            {
                throw new Exception("该城市不存在！");
            }
            ActionResult actionResult = memberService.ChangeUserCity(guidUser, area.Code, cityCode.longitude, cityCode.latitude,area.Id.ToString ());
            if (!actionResult.IsSuccess)
            {
                throw new Exception(actionResult.ErrMsg);
            }
            else
            {
                
                httpRequest.CreateHttpRequest(Dianzhu.Config.Config.GetAppSetting("NotifyServer") + "type=customer_change_city"
                            + "&userid=" + customer.UserID + "&areacode=" + area.Code);
                // receptionService.DeleteReception(customer.UserID);


            }
            return new string[] { "修改成功" };
        }

        /// <summary>
        ///  读取客服信息(申请客服资源)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public applyCustomerServicesObj GetCustomerServices(Customer customer)
        {
            Guid guidUserID = utils.CheckGuidID(customer.UserID, "customer.UserID");
            MemberDto member = memberService.GetUserById(guidUserID.ToString());
            if (member == null)
            {
                throw new Exception("该用户不存在");
            }

            string errorMessage = string.Empty;
            IList<MemberDto> meberList = memberService.GetUsersByIdList(receptionService.GetOnlineUserList("YDBan_CustomerService"));
                                                                                                            
var csOnline = meberList.Select(x => new MemberArea(x.Id.ToString(), x.UserCity)).ToList();

            ReceptionStatusDto rs = receptionService.AssignCustomerLogin(customer.UserID, member.UserCity,out errorMessage, csOnline);

            MemberDto customerService = memberService.GetUserById( rs.CustomerServiceId);

            ServiceOrder orderToReturn=null;
            Guid guidOrder;
            if (Guid.TryParse(rs.OrderId, out guidOrder))
            {
                orderToReturn = bllServiceOrder.GetOne(guidOrder);
            }
            if (orderToReturn == null)
            {
                orderToReturn = bllServiceOrder.GetDraftOrder(member.Id.ToString(), customerService.Id.ToString());
            }
            ilog.Debug("7");
            if (orderToReturn == null)
            {
                orderToReturn = new ServiceOrder()
                {
                    CustomerId = customer.UserID,
                    CustomerServiceId = customerService.Id.ToString()
                };

                bllServiceOrder.Save(orderToReturn);
            }
            ilog.Debug("8");
            //更新 ReceptionStatus 中订单
            receptionService.UpdateOrderId(rs.Id, orderToReturn.Id.ToString());
            ilog.Debug("9");
            applyCustomerServicesObj applycustomerservicesobj = new applyCustomerServicesObj();
            applycustomerservicesobj.customerServicesObj.id = rs.CustomerServiceId;
            applycustomerservicesobj.customerServicesObj.imgUrl = string.IsNullOrEmpty(customerService.AvatarUrl)?string.Empty: Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + customerService.AvatarUrl;
            applycustomerservicesobj.customerServicesObj.alias = customerService.NickName ?? string.Empty;
            applycustomerservicesobj.draftOrderID = orderToReturn.Id.ToString();
            return applycustomerservicesobj;
        }



        public void Dispose()
        {
            //dzmsp.Dispose();
        }
    }
}
