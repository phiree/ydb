﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using System.Collections.Specialized;
using PHSuit;
using FluentValidation.Results;

/// <summary>
/// 新增店铺
/// </summary>
public class ResponseWTM001005 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseWTM001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataWTM001005 requestData = this.request.ReqData.ToObject<ReqDataWTM001005>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
        BLLServiceOpenTimeForDay bllServiceOpenTimeForDay = new BLLServiceOpenTimeForDay();

        try
        {
            string workTime_id = requestData.workTimeID;

            Guid workTimeID;
            bool isWTId = Guid.TryParse(workTime_id, out workTimeID);
            if (!isWTId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "workTimeID格式有误";
                return;
            }

            //DZMembership member = null;
            //if (request.NeedAuthenticate)
            //{                
            //    bool validated = new Account(p).ValidateUser(userID, requestData.pWord, this, out member);
            //    if (!validated)
            //    {
            //        return;
            //    } 
            //}
            //else
            //{
            //    member = p.GetUserById(userID);
            //    if (member == null)
            //    {
            //        this.state_CODE = Dicts.StateCode[1];
            //        this.err_Msg = "不存在该商户！";
            //        return;
            //    }
            //}
            try
            {
                ServiceOpenTimeForDay sotForDay = bllServiceOpenTimeForDay.GetOne(workTimeID);
                if (sotForDay == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该服务时段！";
                    return;
                }

                RespDataWTM_workTimeObj workTimeObj = new RespDataWTM_workTimeObj().Adapt(sotForDay);

                RespDataWTM001005 respData = new RespDataWTM001005();
                respData.workTimeObj = workTimeObj;
                this.state_CODE = Dicts.StateCode[0];
                this.RespData = respData;
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

    private DayOfWeek StringToWeek(string str)
    {
        DayOfWeek week = new DayOfWeek();
        switch (str)
        {
            case "1": week = DayOfWeek.Monday; break;
            case "2": week = DayOfWeek.Tuesday; break;
            case "3": week = DayOfWeek.Wednesday; break;
            case "4": week = DayOfWeek.Thursday; break;
            case "5": week = DayOfWeek.Friday; break;
            case "6": week = DayOfWeek.Saturday; break;
            case "7": week = DayOfWeek.Sunday; break;
            default: break; ;
        }
        return week;
    }
}


