﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

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

            Mapper.CreateMap<Model.DZMembership, customerObj>()
            .ForMember(x => x.alias, opt => opt.MapFrom(source => source.DisplayName))
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.AvatarUrl != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.AvatarUrl : ""))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.DZMembership, merchantObj>()
           .ForMember(x => x.alias, opt => opt.MapFrom(source => source.DisplayName))
           .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.AvatarUrl))
           .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Area, cityObj>()
            .ForMember(x => x.name, opt => opt.MapFrom(source => source.Name.Substring(source.Name.IndexOf('省') + 1)))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Complaint, complaintObj>()
            .ForMember(x => x.resourcesUrl, opt => opt.MapFrom(source => source.ComplaitResourcesUrl))
            .ForMember(x => x.orderID, opt => opt.MapFrom(source => source.Order.Id))
            .ForMember(x => x.senderID, opt => opt.MapFrom(source => source.Operator.Id))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Advertisement, adObj>()
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.ImgUrl != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.ImgUrl : ""))
            .ForMember(x => x.updateTime, opt => opt.MapFrom(source => source.LastUpdateTime))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.DeviceBind, appObj>()
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ServiceOrderRemind, remindObj>()
            .ForMember(x => x.remindTime, opt => opt.MapFrom(source => source.RemindTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.Open))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.OrderAssignment, assignObj>()
            .ForMember(x => x.orderID, opt => opt.MapFrom(source => source.Order.Id))
            .ForMember(x => x.staffID, opt => opt.MapFrom(source => source.AssignedStaff.Id))
            .ForMember(x => x.createTime, opt => opt.MapFrom(source => source.CreateTime.ToString("yyyyMMddHHmmss")))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ServiceOpenTimeForDay, workTimeObj>()
            .ForMember(x => x.maxCountOrder, opt => opt.MapFrom(source => source.MaxOrderForOpenTime))
            .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.Enabled))
            .ForMember(x => x.startTime, opt => opt.MapFrom(source => source.TimeStart))
            .ForMember(x => x.endTime, opt => opt.MapFrom(source => source.TimeEnd))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ServiceType, serviceTypeObj>()
           .ForMember(x => x.id, opt => opt.MapFrom(source => source.Id.ToString()))
           .ForMember(x => x.superID, opt => opt.MapFrom(source => source.ParentId==null?"":source.ParentId.ToString ()))
           .ForMember(x => x.fullDescription, opt => opt.MapFrom(source => source.ToString()))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.DZService, servicesObj>()
           .ForMember(x => x.introduce, opt => opt.MapFrom(source => source.Description))
           .ForMember(x => x.startAt, opt => opt.MapFrom(source => source.MinPrice))
           .ForMember(x => x.deposit, opt => opt.MapFrom(source => source.DepositAmount))
           .ForMember(x => x.appointmentTime, opt => opt.MapFrom(source => source.OrderDelay))
           .ForMember(x => x.bDoorService, opt => opt.MapFrom(source => source.ServiceMode.ToString() == "ToHouse" ? true : false))
           .ForMember(x => x.eServiceTarget, opt => opt.MapFrom(source => source.IsForBusiness ? "all" : "company"))
           .ForMember(x => x.eSupportPayWay, opt => opt.MapFrom(source => source.AllowedPayType.ToString()))
           .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.Enabled))
           .ForMember(x => x.maxCount, opt => opt.MapFrom(source => source.MaxOrdersPerDay))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ServiceOrderPushedService, servicesObj>()
           .ForMember(x => x.name, opt => opt.MapFrom(source => source.ServiceName))
           .ForMember(x => x.serviceType, opt => opt.MapFrom(source => source.OriginalService.ServiceType.ToString()))
           .ForMember(x => x.introduce, opt => opt.MapFrom(source => source.Description))
           .ForMember(x => x.startAt, opt => opt.MapFrom(source => source.MinPrice))
           .ForMember(x => x.deposit, opt => opt.MapFrom(source => source.DepositAmount))
           .ForMember(x => x.appointmentTime, opt => opt.MapFrom(source => source.TargetTime))
           .ForMember(x => x.bDoorService, opt => opt.MapFrom(source => source.ServiceMode.ToString() == "ToHouse" ? true : false))
           .ForMember(x => x.eServiceTarget, opt => opt.MapFrom(source => source.OriginalService.IsForBusiness ? "all" : "company"))
           .ForMember(x => x.eSupportPayWay, opt => opt.MapFrom(source => source.OriginalService.AllowedPayType.ToString()))
           .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.OriginalService.Enabled))
           .ForMember(x => x.maxCount, opt => opt.MapFrom(source => source.OriginalService.MaxOrdersPerDay))
            .ForAllMembers(opt => opt.NullSubstitute(""));
           

            Mapper.CreateMap<Model.Staff, staffObj>()
            .ForMember(x => x.alias, opt => opt.MapFrom(source => source.DisplayName))
            .ForMember(x => x.number, opt => opt.MapFrom(source => source.Code))
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.Photo != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.Photo : ""))
            .ForMember(x => x.sex, opt => opt.MapFrom(source => source.Gender == "女" ? true : false))
            .ForMember(x => x.realName, opt => opt.MapFrom(source => source.Name))
            //.ForMember(x => x.identity, opt => opt.MapFrom(source => source.Code))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Business, storeObj>()
            .ForMember(x => x.introduction, opt => opt.MapFrom(source => source.Description))
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.BusinessAvatar.ImageName != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + source.BusinessAvatar.ImageName : ""))
            .ForMember(x => x.storePhone, opt => opt.MapFrom(source => source.Phone))
            .ForMember(x => x.linkMan, opt => opt.MapFrom(source => source.Contact))
            .ForMember(x => x.linkPhone, opt => opt.MapFrom(source => source.Phone))
            .ForMember(x => x.linkIdentity, opt => opt.MapFrom(source => source.ChargePersonIdCardNo))
            .ForMember(x => x.headCount, opt => opt.MapFrom(source => source.StaffAmount))
            .ForMember(x => x.vintage, opt => opt.MapFrom(source => source.WorkingYears))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Payment, payObj>()
            .ForMember(x => x.payStatus, opt => opt.MapFrom(source => source.Status.ToString()))
            .ForMember(x => x.type, opt => opt.MapFrom(source => source.PayTarget.ToString()))
            .ForMember(x => x.updateTime, opt => opt.MapFrom(source => source.LastUpdateTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.bOnline, opt => opt.MapFrom(source => source.PayType== Model.Enums.enum_PayType.Online))
            .ForMember(x => x.payTarget, opt => opt.MapFrom(source => source.PayApi.ToString()))
            .ForAllMembers(opt => opt.NullSubstitute(""));


            Mapper.CreateMap<Model.ReceptionChat, chatObj>()
            .ForMember(x => x.to, opt => opt.MapFrom(source => source.To.Id))
            .ForMember(x => x.from, opt => opt.MapFrom(source => source.From.Id))
            .ForMember(x => x.orderID, opt => opt.MapFrom(source => source.ServiceOrder.Id))
            .ForMember(x => x.body, opt => opt.MapFrom(source => source.MessageBody))
            .ForMember(x => x.type, opt => opt.MapFrom(source => source.ChatType.ToString()))
            .ForMember(x => x.sendTime, opt => opt.MapFrom(source => source.SendTime.ToString("yyyyMMddHHmmss")))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ServiceOrder, orderObj>()
            .ForMember(x => x.createTime, opt => opt.MapFrom(source => source.OrderCreated.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.closeTime, opt => opt.MapFrom(source => source.OrderFinished.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.serviceTime, opt => opt.MapFrom(source => source.OrderServerStartTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.doneTime, opt => opt.MapFrom(source => source.OrderServerFinishedTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.notes, opt => opt.MapFrom(source => source.Memo))
            .ForMember(x => x.serviceAddress, opt => opt.MapFrom(source => source.TargetAddress))
            .ForAllMembers(opt => opt.NullSubstitute(""));



            Mapper.CreateMap<Model.ServiceOrderStateChangeHis, orderStatusObj>()
            .ForMember(x => x.status, opt => opt.MapFrom(source => source.NewStatus.ToString()))
            .ForMember(x => x.createTime, opt => opt.MapFrom(source => source.CreatTime))
            .ForMember(x => x.lastStatus, opt => opt.MapFrom(source => source.OldStatus.ToString()))
            .ForMember(x => x.title, opt => opt.MapFrom(source => source.NewStatusStr))
            .ForMember(x => x.content, opt => opt.MapFrom(source => source.NewStatusCon))
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

            //.ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));



            //Mapper.CreateMap<CommentFormModel, Comment>();
            //Mapper.CreateMap<GroupFormModel, Group>();
            //Mapper.CreateMap<FocusFormModel, Focus>();
            //Mapper.CreateMap<UpdateFormModel, Update>();
            //Mapper.CreateMap<UserFormModel, ApplicationUser>();
            //Mapper.CreateMap<UserProfileFormModel, UserProfile>();
            //Mapper.CreateMap<GroupGoalFormModel, GroupGoal>();
            //Mapper.CreateMap<GroupUpdateFormModel, GroupUpdate>();
            //Mapper.CreateMap<GroupCommentFormModel, GroupComment>();
            //Mapper.CreateMap<GroupRequestFormModel, GroupRequest>();
            //Mapper.CreateMap<FollowRequestFormModel, FollowRequest>();
            //Mapper.CreateMap<GoalFormModel, Goal>();
            //Mapper.CreateMap<XViewModel, X()
            //    .ForMember(x => x.PropertyXYZ, opt => opt.MapFrom(source => source.Property1));     
        }


    }
}
