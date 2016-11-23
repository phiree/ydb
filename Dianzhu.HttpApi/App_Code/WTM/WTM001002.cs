﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using System.Collections.Specialized;
using PHSuit;
using FluentValidation.Results;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

/// <summary>
/// 新增店铺
/// </summary>
public class ResponseWTM001002 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseWTM001002(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataWTM001002 requestData = this.request.ReqData.ToObject<ReqDataWTM001002>();

        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

        IDZServiceService dzServiceService = Bootstrap.Container.Resolve<IDZServiceService>();

        IDZTagService tagService = Bootstrap.Container.Resolve<IDZTagService>();
        IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();


       

        //   BLLServiceOpenTime bllServiceOpenTime = Bootstrap.Container.Resolve <BLLServiceOpenTime>();
        IServiceOpenTimeForDayService worktimeService = Bootstrap.Container.Resolve<IServiceOpenTimeForDayService>();
       
        //20160620_longphui_modify
        //BLLServiceOpenTimeForDay bllServiceOpenTimeForDay = new BLLServiceOpenTimeForDay();
     
         try
        {
            string raw_id = requestData.merchantID;
            string workTime_id = requestData.workTimeID;

            Guid userID,workTimeID;
            bool isUserId = Guid.TryParse(raw_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isSvcId = Guid.TryParse(workTime_id, out workTimeID);
            if (!isSvcId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "svcId格式有误";
                return;
            }

 MemberDto member = null;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(memberService).ValidateUser(userID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = memberService.GetUserById(userID.ToString());
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该商户！";
                    return;
                }
            }
            try
            {
                ServiceOpenTimeForDay sotForDay = worktimeService.GetOne(workTimeID);
                if (sotForDay == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该工作时间！";
                    return;
                }

                worktimeService.Delete(sotForDay);
                
                this.state_CODE = Dicts.StateCode[0];
            }
            catch (Exception ex)
            {
                ilog.Error(ex.Message);
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }

        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }
    }
}


