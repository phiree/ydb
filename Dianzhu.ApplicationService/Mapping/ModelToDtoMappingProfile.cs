using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.Application;
using Dianzhu.Model;
using Ydb.Common;

namespace Dianzhu.ApplicationService.Mapping
{

    public class ModelToDtoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ModelToDtoMappings"; }
        }

        protected override void Configure()
        {

            //Mapper.CreateMap< Model.Trait_Filtering, common_Trait_Filtering>()
            //.ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<Model.Client, ClientDTO>()
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.RefreshToken, RefreshTokenDTO>()
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<MemberDto, customerObj>()
            .ForMember(x => x.alias, opt => opt.MapFrom(source => source.DisplayName))
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.AvatarUrl != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.AvatarUrl : ""))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<MemberDto, merchantObj>()
           .ForMember(x => x.alias, opt => opt.MapFrom(source => source.DisplayName))
           .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.AvatarUrl != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.AvatarUrl : ""))
           .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap< Area, cityObj>()
            .ForMember(x => x.name, opt => opt.MapFrom(source => source.Name.Substring(source.Name.IndexOf('省') + 1)))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Complaint, complaintObj>()
            .ForMember(x => x.resourcesUrls, opt => opt.MapFrom(source => source.ComplaitResourcesUrl))
            .ForMember(x => x.orderID, opt => opt.MapFrom(source => source.Order.Id))
            .ForMember(x => x.senderID, opt => opt.MapFrom(source => source.OperatorId))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Advertisement, adObj>()
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.ImgUrl != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.ImgUrl : ""))
            .ForMember(x => x.updateTime, opt => opt.MapFrom(source => source.LastUpdateTime))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.DeviceBind, appObj>()
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ServiceOrderRemind, remindObj>()
            .ForMember(x => x.remindTime, opt => opt.MapFrom(source => source.RemindTime == DateTime.MinValue ? "" : source.RemindTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.Open))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.OrderAssignment, assignObj>()
            .ForMember(x => x.orderID, opt => opt.MapFrom(source => source.Order.Id))
            .ForMember(x => x.staffID, opt => opt.MapFrom(source => source.AssignedStaffId))
            .ForMember(x => x.createTime, opt => opt.MapFrom(source => source.CreateTime == DateTime.MinValue ? "" : source.CreateTime.ToString("yyyyMMddHHmmss")))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.WorkTimeSnapshot, workTimeObj>()
            .ForMember(x => x.maxCountOrder, opt => opt.MapFrom(source => source.MaxOrderForWorkDay))
            .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.Enabled))
            //todo:refactor: 需要转换成字符串.
            .ForMember(x => x.startTime, opt => opt.MapFrom(source => source.TimePeriod.StartTime.ToString()))
            .ForMember(x => x.endTime, opt => opt.MapFrom(source => source.TimePeriod.EndTime.ToString()))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<ServiceType, serviceTypeObj>()
           .ForMember(x => x.id, opt => opt.MapFrom(source => source.Id.ToString()))
           .ForMember(x => x.superID, opt => opt.MapFrom(source => source.ParentId==null?"":source.ParentId.ToString ()))
           .ForMember(x => x.fullDescription, opt => opt.MapFrom(source => source.ToString()))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<ServiceSnapShot, servicesObj>()
           .ForMember(x => x.introduce, opt => opt.MapFrom(source => source.Description))
           .ForMember(x => x.startAt, opt => opt.MapFrom(source => source.MinPrice))
           .ForMember(x => x.deposit, opt => opt.MapFrom(source => source.DepositAmount))
           .ForMember(x => x.appointmentTime, opt => opt.MapFrom(source => source.OrderDelay))
           .ForMember(x => x.bDoorService, opt => opt.MapFrom(source => source.ServiceModeType == "ToHouse" ? true : false))
           .ForMember(x => x.eServiceTarget, opt => opt.MapFrom(source => source.IsForBusiness ? "all" : "company"))
           .ForMember(x => x.eSupportPayWay, opt => opt.MapFrom(source => source.AllowedPayType.ToString()))
           .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.Enabled))
         //  .ForMember(x => x.maxCount, opt => opt.MapFrom(source => source.MaxOrdersPerDay))
           .ForMember(x => x.chargeUnit, opt => opt.MapFrom(source => source.ChargeUnitType))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ServiceOrderPushedService, serviceSnapshotObj>()
           .ForMember(x => x.name, opt => opt.MapFrom(source => source.ServiceSnapShot.ServiceName))
 
           .ForMember(dest=>dest.serviceType,input=>input.MapFrom(i=>new serviceTypeObj {  fullDescription=i.ServiceTypeName, id=i.ServiceTypeId, superID=i.ServiceTypeParentId}))
 
           .ForMember(x => x.introduce, opt => opt.MapFrom(source => source.ServiceSnapShot.Description))
           .ForMember(x => x.startAt, opt => opt.MapFrom(source => source.ServiceSnapShot.MinPrice))
           .ForMember(x => x.deposit, opt => opt.MapFrom(source => source.ServiceSnapShot.DepositAmount))
           .ForMember(x => x.appointmentTime, opt => opt.MapFrom(source => source.TargetTime == DateTime.MinValue ? "" : source.TargetTime.ToString("yyyyMMddHHmmss")))
           .ForMember(x => x.bDoorService, opt => opt.MapFrom(source => source.ServiceSnapShot.ServiceModeType == "ToHouse" ? true : false))
           .ForMember(x => x.eServiceTarget, opt => opt.MapFrom(source => source.ServiceSnapShot.IsForBusiness ? "all" : "company"))
           .ForMember(x => x.eSupportPayWay, opt => opt.MapFrom(source => source.ServiceSnapShot.AllowedPayType.ToString()))
           .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.ServiceSnapShot.Enabled))
           .ForMember(x => x.maxCount, opt => opt.MapFrom(source => source.ServiceOpenTimeSnapShot.MaxOrderForWorkDay))
           .ForMember(x => x.originalServiceID, opt => opt.MapFrom(source => source.OriginalServiceId.ToString()))
           .ForMember(x => x.chargeUnit, opt => opt.MapFrom(source => source.ServiceSnapShot.ChargeUnitType))
            .ForAllMembers(opt => opt.NullSubstitute(""));
            Mapper.CreateMap<ServiceDto, ServiceSnapShot>();
            Mapper.CreateMap<Model.ServiceOrderDetail, serviceSnapshotObj>()
           .ForMember(x => x.name, opt => opt.MapFrom(source => source.ServiceSnapShot.ServiceName))
           .ForMember(x => x.serviceType, opt => opt.MapFrom(source => source.ServiceSnapShot.ServiceTypeName ))
           .ForMember(x => x.introduce, opt => opt.MapFrom(source => source.ServiceSnapShot.Description))
           .ForMember(x => x.startAt, opt => opt.MapFrom(source => source.ServiceSnapShot.MinPrice))
           .ForMember(x => x.unitPrice, opt => opt.MapFrom(source => source.ServiceSnapShot.UnitPrice))
           .ForMember(x => x.deposit, opt => opt.MapFrom(source => source.ServiceSnapShot.DepositAmount))
           .ForMember(x => x.appointmentTime, opt => opt.MapFrom(source => source.TargetTime == DateTime.MinValue ? "" : source.TargetTime.ToString("yyyyMMddHHmmss")))
           .ForMember(x => x.bDoorService, opt => opt.MapFrom(source => source.ServiceSnapShot.ServiceModeType  == "ToHouse" ? true : false))
           .ForMember(x => x.eServiceTarget, opt => opt.MapFrom(source => source.ServiceSnapShot.IsForBusiness ? "all" : "company"))
         //  .ForMember(x => x.eSupportPayWay, opt => opt.MapFrom(source => source.OriginalService.AllowedPayType.ToString()))
          // .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.OriginalService.Enabled))
           .ForMember(x => x.maxCount, opt => opt.MapFrom(source => source.ServiceOpentimeSnapshot.MaxOrderForWorkDay))
           .ForMember(x => x.originalServiceID, opt => opt.MapFrom(source => source.OriginalServiceId))
          // .ForMember(x => x.chargeUnit, opt => opt.MapFrom(source => source.OriginalService.ChargeUnitFriendlyName))
            .ForAllMembers(opt => opt.NullSubstitute(""));


            Mapper.CreateMap<Ydb.BusinessResource.DomainModel.Staff, staffObj>()
            .ForMember(x => x.alias, opt => opt.MapFrom(source => source.DisplayName))
            .ForMember(x => x.number, opt => opt.MapFrom(source => source.Code))
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.Photo != null ? Dianzhu.Config.Config.GetAppSetting("ImageHandler") + source.Photo : source.AvatarCurrent==null?"": Dianzhu.Config.Config.GetAppSetting("ImageHandler") + source.AvatarCurrent.ImageName))//MediaGetUrl
            .ForMember(x => x.sex, opt => opt.MapFrom(source => source.Gender == "女" ? true : false))
            .ForMember(x => x.realName, opt => opt.MapFrom(source => source.Name))
            //.ForMember(x => x.identity, opt => opt.MapFrom(source => source.Code))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap< Business, storeObj>()
            .ForMember(x => x.appraise, opt => opt.MapFrom(source => "3"))
            .ForMember(x => x.introduction, opt => opt.MapFrom(source => source.Description))
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.BusinessAvatar.ImageName != null ? Dianzhu.Config.Config.GetAppSetting("ImageHandler") + source.BusinessAvatar.ImageName : ""))//MediaGetUrl
            .ForMember(x => x.storePhone, opt => opt.MapFrom(source => source.Phone))
            .ForMember(x => x.linkMan, opt => opt.MapFrom(source => source.Contact))
            .ForMember(x => x.linkPhone, opt => opt.MapFrom(source => source.Phone))
            .ForMember(x => x.linkIdentity, opt => opt.MapFrom(source => source.ChargePersonIdCardNo))
            .ForMember(x => x.headCount, opt => opt.MapFrom(source => source.StaffAmount))
            .ForMember(x => x.vintage, opt => opt.MapFrom(source => source.WorkingYears))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Payment, payObj>()
            .ForMember(x => x.payStatus, opt => opt.MapFrom(source => source.Status== enum_PaymentStatus.Wait_Buyer_Pay? "waitforpay": source.Status == enum_PaymentStatus.Trade_Success ? "success": source.Status == enum_PaymentStatus.Trade_Finished? "success": "failed"))
            .ForMember(x => x.type, opt => opt.MapFrom(source => source.PayTarget==  enum_PayTarget.Deposit? "deposit": source.PayTarget == enum_PayTarget.FinalPayment? "finalPayment": "compensation"))
            .ForMember(x => x.updateTime, opt => opt.MapFrom(source => source.LastUpdateTime == DateTime.MinValue ? "" : source.LastUpdateTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.bOnline, opt => opt.MapFrom(source =>  source.PayType==  enum_PayType.Online))
            .ForMember(x => x.payTarget, opt => opt.MapFrom(source => source.PayApi.ToString()))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            //order.OpenFireLinkMan + "@" + strIp + "/" + enum_XmppResource.YDBan_Store;            source.ChatType == enum_ChatType.PushedService ? source.ServiceOrder.Id.ToString() :
            Mapper.CreateMap<ReceptionChatDto, chatObj>()
            .ForMember(x => x.to, opt => opt.MapFrom(source => source.ToId + "@"+ System.Web.HttpContext.Current.Request.Url.Host+ "/" + source.ToResource.ToString()))
            .ForMember(x => x.from, opt => opt.MapFrom(source => source.FromId+ "@" + System.Web.HttpContext.Current.Request.Url.Host + "/" + source.FromResource.ToString()))
            .ForMember(x => x.orderID, opt => opt.MapFrom(source => source.SessionId))
            .ForMember(x => x.body, opt => opt.MapFrom(source => source.GetType() == typeof(ReceptionChatMediaDto)? string.IsNullOrEmpty(((ReceptionChatMediaDto)source).MedialUrl)?"": Dianzhu.Config.Config.GetAppSetting("MediaGetUrl")+ ((ReceptionChatMediaDto)source).MedialUrl :  source.MessageBody))
            .ForMember(x => x.type, opt => opt.MapFrom(source => source.GetType() == typeof(ReceptionChatDto) ? "chat": source.GetType() ==typeof(ReceptionChatMediaDto)?((ReceptionChatMediaDto)source).MediaType: source.GetType() == typeof(ReceptionChatPushServiceDto) ? "pushOrder" : source.ChatType.ToString()))
            .ForMember(x => x.sendTime, opt => opt.MapFrom(source => source.SavedTime == DateTime.MinValue ? "" : source.SavedTime.ToString("yyyyMMddHHmmss")))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ServiceOrder, orderObj>()
            .ForMember(x => x.createTime, opt => opt.MapFrom(source => source.OrderConfirmTime== DateTime.MinValue ? "" : source.OrderConfirmTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.closeTime, opt => opt.MapFrom(source => source.OrderFinished==DateTime.MinValue?"":source.OrderFinished.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.serviceTime, opt => opt.MapFrom(source => source.Details==null || source.Details.Count==0? "":source.Details[0].TargetTime == DateTime.MinValue ? "" : source.Details[0].TargetTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.startTime, opt => opt.MapFrom(source => source.OrderServerStartTime == DateTime.MinValue ? "" : source.OrderServerStartTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.doneTime, opt => opt.MapFrom(source => source.OrderServerFinishedTime == DateTime.MinValue ? "" : source.OrderServerFinishedTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.updateTime, opt => opt.MapFrom(source => source.LatestOrderUpdated == DateTime.MinValue ? "" : source.LatestOrderUpdated.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.notes, opt => opt.MapFrom(source => source.Memo))
            .ForMember(x => x.title, opt => opt.MapFrom(source => source.SerialNo))
            .ForMember(x => x.serviceAddress, opt => opt.MapFrom(source => source.TargetAddress))
            .ForAllMembers(opt => opt.NullSubstitute(""));



            Mapper.CreateMap<Model.ServiceOrderStateChangeHis, orderStatusObj>()
            .ForMember(x => x.status, opt => opt.MapFrom(source => source.NewStatus.ToString()))
            .ForMember(x => x.createTime, opt => opt.MapFrom(source => source.CreatTime == DateTime.MinValue ? "" : source.CreatTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.lastStatus, opt => opt.MapFrom(source => source.OldStatus.ToString()))
            .ForMember(x => x.title, opt => opt.MapFrom(source => source.Order.GetStatusTitleFriendly(source.NewStatus)))
            .ForMember(x => x.content, opt => opt.MapFrom(source => source.Order.GetStatusContextFriendly(source.NewStatus)))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.StorageFileInfo, imageObj>()
           .ForMember(x => x.url, opt => opt.MapFrom(source => source.FileName != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.FileName : ""))
           .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.StorageFileInfo, avatarImageObj>()
           .ForMember(x => x.url, opt => opt.MapFrom(source => source.FileName != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.FileName : ""))
           .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.StorageFileInfo, audioObj>()
          .ForMember(x => x.url, opt => opt.MapFrom(source => source.FileName != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.FileName : ""))
          .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ClaimsDetails, refundStatusObj>()
          .ForMember(x => x.content, opt => opt.MapFrom(source => source.Context))
          .ForMember(x => x.target, opt => opt.MapFrom(source => source.Target.ToString()))
          .ForMember(x => x.orderStatus, opt => opt.MapFrom(source => source.Claims.Order.OrderStatus.ToString()))
            .ForMember(x => x.createTime, opt => opt.MapFrom(source => source.CreatTime == DateTime.MinValue ? "" : source.CreatTime.ToString("yyyyMMddHHmmss")))
          .ForAllMembers(opt => opt.NullSubstitute(""));

                 
        }


    }
}
