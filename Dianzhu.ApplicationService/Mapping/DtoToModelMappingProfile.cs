using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dianzhu.Model;
using Dianzhu.DAL;


namespace Dianzhu.ApplicationService.Mapping
{

    public class DtoToModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DtoToModelMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<common_Trait_Filtering, Model.Trait_Filtering>()
            //.ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<ClientDTO, Model.Client>()
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<RefreshTokenDTO, RefreshToken>()
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<customerObj, DZMembership>()
            .ForMember(x => x.NickName, opt => opt.MapFrom(source => source.alias))
            .ForMember(x => x.AvatarUrl, opt => opt.MapFrom(source => source.imgUrl))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<cityObj, Model.Area>()
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<complaintObj, Model.Complaint>()
            .ForMember(x => x.ComplaitResourcesUrl, opt => opt.MapFrom(source => source.resourcesUrl))
            .ForMember(x => x.Order, opt => opt.MapFrom(source => new DAL.DALServiceOrder().FindById(utils.CheckGuidID(source.orderID, "orderID"))))
            .ForMember(x => x.Operator, opt => opt.MapFrom(source => new DAL.DALMembership().FindById(utils.CheckGuidID(source.senderID, "senderID"))))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<adObj, Model.Advertisement>()
            .ForMember(x => x.LastUpdateTime, opt => opt.MapFrom(source => source.updateTime))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            //Mapper.CreateMap<appObj, Model.DeviceBind>()
            //.ForMember(x => x.AppName, opt => opt.MapFrom(source => source.appName .ToString ()))
            //.ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<remindObj, Model.ServiceOrderRemind>()
            .ForMember(x => x.RemindTime, opt => opt.MapFrom(source => utils.CheckDateTime(source.remindTime, "yyyyMMddHHmmss", "remindObj.time")))
            .ForMember(x => x.Open, opt => opt.MapFrom(source => source.bOpen))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<assignObj, Model.OrderAssignment>()
            .ForMember(x => x.Order, opt => opt.MapFrom(source => new DAL.DALServiceOrder().FindById(utils.CheckGuidID(source.orderID, "orderID"))))
            .ForMember(x => x.AssignedStaff, opt => opt.MapFrom(source => new DAL.DALOrderAssignment().FindById(utils.CheckGuidID(source.staffID, "staffID"))))
            .ForMember(x => x.CreateTime, opt => opt.MapFrom(source => utils.CheckDateTime(source.createTime, "yyyyMMddHHmmss", "remindObj.time")))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<workTimeObj, Model.ServiceOpenTimeForDay>()
            //.ForMember(x => x.Id, opt => opt.MapFrom(source => source.id==""?Guid.Empty:utils.CheckGuidID(source.id, "workTimeObj.id")))
            .ForMember(x => x.MaxOrderForOpenTime, opt => opt.MapFrom(source => source.maxCountOrder))
            .ForMember(x => x.Enabled, opt => opt.MapFrom(source => source.bOpen))
            .ForMember(x => x.TimeStart, opt => opt.MapFrom(source => source.startTime))
            .ForMember(x => x.TimeEnd, opt => opt.MapFrom(source => source.endTime))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<servicesObj, Model.DZService>()
            .ForMember(x => x.Description, opt => opt.MapFrom(source => source.introduce))
            .ForMember(x => x.MinPrice, opt => opt.MapFrom(source => decimal.Parse(source.startAt)))
            .ForMember(x => x.DepositAmount, opt => opt.MapFrom(source => decimal.Parse(source.deposit)))
            .ForMember(x => x.OrderDelay, opt => opt.MapFrom(source => Int32.Parse(source.appointmentTime)))
            .ForMember(x => x.ServiceMode, opt => opt.MapFrom(source => source.bDoorService? "ToHouse" : "NotToHouse"))
            .ForMember(x => x.IsForBusiness, opt => opt.MapFrom(source => (source.eServiceTarget=="all")))
            .ForMember(x => x.AllowedPayType, opt => opt.MapFrom(source => source.eSupportPayWay))
            .ForMember(x => x.Enabled, opt => opt.MapFrom(source => source.bOpen))
            .ForMember(x => x.MaxOrdersPerDay, opt => opt.MapFrom(source => Int32.Parse(source.maxCount)))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<staffObj, Model.Staff>()
            .ForMember(x => x.NickName, opt => opt.MapFrom(source => source.alias))
            .ForMember(x => x.Code, opt => opt.MapFrom(source => source.number))
            .ForMember(x => x.Photo, opt => opt.MapFrom(source => source.imgUrl))
            .ForMember(x => x.Gender, opt => opt.MapFrom(source => source.sex?"女":"男"))
            .ForMember(x => x.Name, opt => opt.MapFrom(source => source.realName))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<storeObj, Model.Business>()
            .ForMember(x => x.Description, opt => opt.MapFrom(source => source.introduction))
            .ForMember(x => x.Phone, opt => opt.MapFrom(source => source.storePhone))
            .ForMember(x => x.Contact, opt => opt.MapFrom(source => source.linkMan))
            .ForMember(x => x.WorkingYears, opt => opt.MapFrom(source => source.vintage))
            .ForMember(x => x.StaffAmount, opt => opt.MapFrom(source => source.headCount))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));


            Mapper.CreateMap<payObj, Model.Payment>()
            .ForMember(x => x.Status, opt => opt.MapFrom(source => source.payStatus))
            .ForMember(x => x.PayTarget, opt => opt.MapFrom(source => source.type))
            .ForMember(x => x.LastUpdateTime, opt => opt.MapFrom(source => utils.CheckDateTime(source.updateTime, "yyyyMMddHHmmss", "remindObj.time")))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<chatObj, Model.ReceptionChat>()
            .ForMember(x => x.MessageBody, opt => opt.MapFrom(source => source.body))
            .ForMember(x => x.ChatType, opt => opt.MapFrom(source => source.type))
            .ForMember(x => x.SendTime, opt => opt.MapFrom(source => utils.CheckDateTime(source.sendTime, "yyyyMMddHHmmss", "remindObj.time")))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));


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
