﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Configuration;
using AutoMapper;
using DDDCommon;
using System.Text.RegularExpressions;
using Ydb.Common.Specification;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.Application.Dto;
using Ydb.InstantMessage.DomainModel.Chat;

namespace Dianzhu.ApplicationService.User
{
    public class UserService:IUserService
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.Web.RestfulApi.UserService");
        DZMembershipProvider dzmsp;
        BLL.Client.BLLUserToken bllUserToken;
        IReceptionService receptionService;
        IBLLServiceOrder bllServiceOrder;
        public UserService(DZMembershipProvider dzmsp, BLL.Client.BLLUserToken bllUserToken, IReceptionService receptionService, IBLLServiceOrder bllServiceOrder)
        {
            this.dzmsp = dzmsp;
            this.bllUserToken = bllUserToken;
            this.receptionService = receptionService;
            this.bllServiceOrder = bllServiceOrder;
        }

        /// <summary>
        /// 验证用户名密码验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUser(string username, string password)
        {
            return dzmsp.ValidateUser(username, password);
        }

        /// <summary>
        /// 根据userID获取user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public customerObj GetUserById(string userID,string userType)
        {
            Dianzhu.Model.DZMembership dzm =dzmsp .GetUserById(utils.CheckGuidID(userID, "userID"));
            customerObj userobj = null;
            if (dzm.UserType.ToString()== userType)//customer=1"customer"
            {
                userobj = Mapper.Map<Dianzhu.Model.DZMembership, customerObj>(dzm);
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
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "DZMembership");
            IList<Dianzhu.Model.DZMembership> dzm = dzmsp.GetUsers(filter1.filter2, userFilter.alias, userFilter.email, userFilter.phone, userFilter.platform, userType);
            if (dzm == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<customerObj>();
            }
            IList<customerObj> customerobj = Mapper.Map<IList<Dianzhu.Model.DZMembership>, IList<customerObj>>(dzm);
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
            c.count = dzmsp.GetUsersCount(userFilter.alias, userFilter.email, userFilter.phone, userFilter.platform, userType).ToString();
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
            Dianzhu.Model.Enums.enum_UserType usertype = (Model.Enums.enum_UserType)Enum.Parse(typeof(Model.Enums.enum_UserType), userType); 
            DZMembership newMember = dzmsp.CreateUser(string.Empty,
                 userBody.phone,
                 string.Empty,
                 userBody.pWord,
                 out createStatus,
                 usertype);
            if (createStatus == System.Web.Security.MembershipCreateStatus.DuplicateUserName)
            {
                throw new Exception("该手机号码用户已存在!");
            }
            else if (createStatus != System.Web.Security.MembershipCreateStatus.Success)
            {
                throw new Exception("注册失败!");
            }
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //Dianzhu.Model.DZMembership dzm = dzmsp.GetUserById(newMember.Id);
            //if (dzm == null)
            //{
            //    throw new Exception("注册失败!");
            //}

            if (userType == "customer")
            {
                customerObj customerobj = Mapper.Map<Dianzhu.Model.DZMembership, customerObj>(newMember);
                return customerobj;
            }
            else
            {
                merchantObj merchantobj = Mapper.Map<Dianzhu.Model.DZMembership, merchantObj>(newMember);
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
            Dianzhu.Model.DZMembership newMember = new DZMembership();
            switch (u3rd_Model.platform)
            {
                case "WeChat":
                    newMember=LoginByWeChat.GetUserInfo(u3rd_Model.code, u3rd_Model.appName, dzmsp, userType);
                    break;
                case "SinaWeiBo":
                    newMember = LoginBySinaWeiBo.GetUserInfo(u3rd_Model.code, u3rd_Model.appName, dzmsp, userType);
                    break;
                case "TencentQQ":
                    newMember = LoginByTencentQQ.GetUserInfo(u3rd_Model.code, u3rd_Model.appName, dzmsp, userType);
                    break;
                default:
                    throw new Exception("传入的第三方平台类型有误，请重新上传！!");
            }
           
            Dianzhu.Model.DZMembership dzm = dzmsp.GetUserById(newMember.Id);
            if (dzm == null)
            {
                throw new Exception("注册失败!");
            }
            if (userType == "customer")
            {
                customerObj customerobj = Mapper.Map<Dianzhu.Model.DZMembership, customerObj>(dzm);
                customerobj.pWord = dzm.PlainPassword;
                return customerobj;
            }
            else
            {
                merchantObj merchantobj = Mapper.Map<Dianzhu.Model.DZMembership, merchantObj>(dzm);
                merchantobj.pWord = dzm.PlainPassword;
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
            Dianzhu.Model.DZMembership dzm = dzmsp.GetUserById(guidUser);
            if (dzm == null)
            {
                throw new Exception("该用户不存在！");
            }
            if (dzm.PlainPassword != userChangeBody.oldPassWord)
            {
                throw new Exception("原密码错误！");
            }
            if (!string.IsNullOrEmpty(userChangeBody.alias))
            {
                dzm.NickName = userChangeBody.alias;
            }
            if (!string.IsNullOrEmpty(userChangeBody.email))
            {
                dzm.Email = userChangeBody.email;
            }
            if (!string.IsNullOrEmpty(userChangeBody.phone))
            {
                dzm.Phone = userChangeBody.phone;
            }
            if (!string.IsNullOrEmpty(userChangeBody.address))
            {
                dzm.Address = userChangeBody.address;
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
                dzm.AvatarUrl = utils.GetFileName(userChangeBody.imgUrl);
            }
            if (!string.IsNullOrEmpty(userChangeBody.newPassWord))
            {
                if (!dzm.ChangePassword(userChangeBody.oldPassWord, userChangeBody.newPassWord))
                {
                    throw new Exception("密码错误!");
                }
                System.Runtime.Caching.MemoryCache.Default.Remove(dzm.Id.ToString());
                Model.UserToken usertoken=bllUserToken.GetToken(dzm.Id.ToString());
                usertoken.Flag = 0;
                //Model.UserToken usertoken = new Model.UserToken { UserID = dzm.Id.ToString(), Token = usertokendto.token, Flag = 0, CreatedTime = DateTime.Now };
                //UserToken ut = usertoken.GetToken(member.Id.ToString());
                //ut.Flag = 0;
                //usertoken.Update(ut);
            }
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
            if (userType == "customer")
            {
                customerObj customerobj = Mapper.Map<Dianzhu.Model.DZMembership, customerObj>(dzm);
                return customerobj;
            }
            else
            {
                merchantObj merchantobj = Mapper.Map<Dianzhu.Model.DZMembership, merchantObj>(dzm);
                return merchantobj;
            }

        }

        /// <summary>
        /// 修改用户城市信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public object PatchCurrentGeolocation(string userID, string cityCode)
        {
            Guid guidUser = utils.CheckGuidID(userID, "userID");
            Dianzhu.Model.DZMembership dzm = dzmsp.GetUserById(guidUser);
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
            DZMembership member = dzmsp.GetUserById(guidUserID);
            if (member == null)
            {
                throw new Exception("该用户不存在");
            }

            string errorMessage = string.Empty;
            ReceptionStatusDto rs = receptionService.AssignCustomerLogin(customer.UserID, out errorMessage);

            DZMembership customerService = dzmsp.GetUserById(Guid.Parse(rs.CustomerServiceId));

            ServiceOrder orderToReturn=null;
            Guid guidOrder;
            if (Guid.TryParse(rs.OrderId, out guidOrder))
            {
                orderToReturn = bllServiceOrder.GetOne(guidOrder);
            }
            if (orderToReturn == null)
            {
                orderToReturn = bllServiceOrder.GetDraftOrder(member, customerService);
            }
            ilog.Debug("7");
            if (orderToReturn == null)
            {

                orderToReturn = ServiceOrderFactory.CreateDraft(customerService, member);

                bllServiceOrder.Save(orderToReturn);
            }
            ilog.Debug("8");
            //更新 ReceptionStatus 中订单
            receptionService.UpdateOrderId(rs.Id, orderToReturn.Id.ToString());
            ilog.Debug("9");
            applyCustomerServicesObj applycustomerservicesobj = new applyCustomerServicesObj();
            applycustomerservicesobj.customerServicesObj.id = rs.CustomerServiceId;
            applycustomerservicesobj.customerServicesObj.imgUrl = string.IsNullOrEmpty(customerService.AvatarUrl)?string.Empty: Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + customerService.AvatarUrl;
            applycustomerservicesobj.customerServicesObj.alias = customerService.DisplayName ?? string.Empty;
            applycustomerservicesobj.draftOrderID = orderToReturn.Id.ToString();
            return applycustomerservicesobj;
        }



        public void Dispose()
        {
            //dzmsp.Dispose();
        }
    }
}
