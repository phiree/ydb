using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common.Domain;
using Ydb.Finance.Application;
using Ydb.Membership.DomainModel.Enums;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Common.Specification;
using Ydb.Order.Application;


namespace AdminAgent.Controllers
{
    public class AgentCustomerServiceManageController : AgentBaseController
    {
        IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
        IUserTypeSharePointService userTypeSharePointService = Bootstrap.Container.Resolve<IUserTypeSharePointService>();
        IChatService chatService = Bootstrap.Container.Resolve<IChatService>();
        IIMUserStatusArchieveService imUserStatusArchieveService= Bootstrap.Container.Resolve<IIMUserStatusArchieveService>();
        IServiceOrderService serviceOrderService = Bootstrap.Container.Resolve<IServiceOrderService>();
        IComplaintService complaintService = Bootstrap.Container.Resolve<IComplaintService>();
        
        /// <summary>
        /// 获取验证助理列表
        /// </summary>
        /// <returns></returns>
        public ActionResult assistant_validate()
        {
            try
            {
                //模拟数据
                //IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto = MockData.dicDto;
                //接口
                IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto = dzMembershipService.GetVerifiedDZMembershipCustomerServiceByArea(CurrentUser.AreaList);
                foreach (KeyValuePair<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> d in dicDto)
                {
                    TempData[d.Key.ToString()] = d.Value.Select(x => x.Id.ToString()).ToList();
                }
                return View(dicDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取助理验证信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult assistant_validate_info(string id,string type)
        {
            try
            {
                TempData["assistant_validate_info_id"] = id;
                TempData["assistant_validate_info_type"] = type;
                ViewData["id"] = id;
                ViewData["type"] = type;
                //接口
                DZMembershipCustomerServiceDto member = dzMembershipService.GetDZMembershipCustomerServiceById(id);
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetDZMembershipCustomerServiceDtoById(id,type);
                return View(member);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 验证通过
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult assistant_validate_info_agree(string id)
        {
            Ydb.Common.Application.ActionResult result = dzMembershipService.VerifyDZMembershipCustomerService(id, true, "");
            if (result.IsSuccess)
            {
                return Content("Success");
            }
            else
            {
                Response.StatusCode = 400;
                return Content(result.ErrMsg);
            }
        }

        /// <summary>
        /// 验证拒绝
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public ActionResult assistant_validate_info_refuse(string id,string memo)
        {
            Ydb.Common.Application.ActionResult result = dzMembershipService.VerifyDZMembershipCustomerService(id, false, memo);
            if (result.IsSuccess)
            {
                return Content("Success");
            }
            else
            {
                Response.StatusCode = 400;
                return Content(result.ErrMsg);
            }
        }

        /// <summary>
        /// 获取下一个
        /// </summary>
        /// <returns></returns>
        public ActionResult assistant_validate_info_next()
        {
            string id=TempData["assistant_validate_info_id"].ToString();
            string type=TempData["assistant_validate_info_type"].ToString();
            var list =(List<string>)TempData[type];
            TempData[type] = list;
            int index = list.IndexOf(id);
            if (index == list.Count - 1)
            {
                Response.StatusCode = 400;
                return Content("这已经是最后一个了！");
            }
            else
            {
                return Redirect("./assistant_validate_info?type="+type+"&id="+list[index+1].ToString());
            }
        }

        /// <summary>
        /// 获取已验证所有助理列表区分账号是否封锁
        /// </summary>
        /// <returns></returns>
        public ActionResult assistant_list()
        {
            try
            {
                //模拟数据
                //IDictionary<Enum_LockCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto = MockData.dicLockDto;
                //ViewData["assistantPoint"] = MockData.assistantPoint;
                //接口
                IDictionary<Enum_LockCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto = dzMembershipService.GetLockDZMembershipCustomerServiceByArea(CurrentUser.AreaList);
                string errMsg = "";
                ViewData["assistantPoint"] = userTypeSharePointService.GetSharePoint(UserType.customerservice.ToString(), out errMsg);
                return View(dicDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 获取助理详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult assistant_detail(string id, string type)
        {
            try
            {
                ViewData["id"] = id;
                ViewData["type"] = type;
                //接口
                DZMembershipCustomerServiceDto member = dzMembershipService.GetDZMembershipCustomerServiceById(id);
                ViewData["totalOnlineTime"] = imUserStatusArchieveService.GetUserTotalOnlineTime(member.Id.ToString());
                ViewData["totalOrderCount"] = serviceOrderService.GetServiceOrderCountWithoutDraft(CurrentUser.UserId, true);
                ViewData["totalComplaintCount"] = complaintService.GetComplaintsCount(Guid.Empty, Guid.Empty, CurrentUser.UserId);
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //ViewData["totalOnlineTime"] = MockData.totalOnlineTime;
                //ViewData["totalOrderCount"] = MockData.totalOrderCount;
                //ViewData["totalComplaintCount"] = MockData.totalComplaintCount;
                return View(member);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult assistant_detail_recoverypassword(string id, string type)
        {
            try
            {
                //接口
                DZMembershipCustomerServiceDto member = dzMembershipService.GetDZMembershipCustomerServiceById(id);
                dzMembershipService.ChangePassword(member.UserName, member.PlainPassword, "123456");
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //member.PlainPassword = "123456";
                return View(member);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 封停/解封账号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult assistant_detail_Lock(string id, string type,bool islock)
        {
            try
            {
                //接口
                DZMembershipCustomerServiceDto member = dzMembershipService.GetDZMembershipCustomerServiceById(id);
                dzMembershipService.LockDZMembershipCustomerService(member.Id.ToString(), islock, "违规操作");
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //member.IsLocked = islock;
                //if (islock)
                //{
                //    member.LockReason = "违规操作";
                //}
                return View(member);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取助理详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult assistant_detail_message(string id)
        {
            try
            {
                ViewData["id"] = id;
                //接口
                DZMembershipCustomerServiceDto member = dzMembershipService.GetDZMembershipCustomerServiceById(id);
                IList<ReceptionChatDto> receptionChatDtoList = chatService.GetChats(new TraitFilter(), "", "", "", member.Id.ToString(), member.UserType);
                //模拟数据
                //IList<ReceptionChatDto> receptionChatDtoList = MockData.receptionChatDtoList;
                return View(receptionChatDtoList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


    }
}