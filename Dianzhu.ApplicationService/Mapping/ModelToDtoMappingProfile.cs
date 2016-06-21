using System;
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
            Mapper.CreateMap<Model.Client,ClientDTO > ()
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.RefreshToken,RefreshTokenDTO > ()
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.DZMembership,userObj>()
            .ForMember(x => x.alias, opt => opt.MapFrom(source => source.NickName))
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.AvatarUrl))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Area, cityObj>()
            .ForMember(x => x.name, opt => opt.MapFrom(source => source.Name.Substring(source.Name.IndexOf('省')+1)))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Complaint, complaintObj>()
            .ForMember(x => x.orderID, opt => opt.MapFrom(source => source.Order.Id))
            .ForMember(x => x.senderID, opt => opt.MapFrom(source => source.Operator.Id))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Advertisement, adObj>()
            .ForMember(x => x.updateTime, opt => opt.MapFrom(source => source.LastUpdateTime))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.DeviceBind, appObj>()
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.ServiceOrderRemind, remindObj>()
            .ForMember(x => x.time, opt => opt.MapFrom(source => source.RemindTime.ToString("yyyyMMddHHmmss")))
            .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.Open))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.OrderAssignment, assignObj>()
            .ForMember(x => x.orderID, opt => opt.MapFrom(source => source.Order.Id))
            .ForMember(x => x.staffID, opt => opt.MapFrom(source => source.AssignedStaff.Id))
            .ForMember(x => x.createTime, opt => opt.MapFrom(source => source.CreateTime.ToString("yyyyMMddHHmmss")))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap< Model.ServiceOpenTimeForDay, workTimeObj>()
            .ForMember(x => x.maxCountOrder , opt => opt.MapFrom(source => source.MaxOrderForOpenTime))
            .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.Enabled ))
            .ForMember(x => x.startTime, opt => opt.MapFrom(source => source.TimeStart))
            .ForMember(x => x.endTime, opt => opt.MapFrom(source => source.TimeEnd))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.DZService, servicesObj>()
           .ForMember(x => x.introduce, opt => opt.MapFrom(source => source.Description))
           .ForMember(x => x.startAt, opt => opt.MapFrom(source => source.MinPrice))
           .ForMember(x => x.deposit, opt => opt.MapFrom(source => source.DepositAmount))
           .ForMember(x => x.appointmentTime, opt => opt.MapFrom(source => source.OrderDelay))
           .ForMember(x => x.bDoorService, opt => opt.MapFrom(source => source.ServiceMode.ToString()== "ToHouse" ?true  : false))
           .ForMember(x => x.eServiceTarget, opt => opt.MapFrom(source =>source.IsForBusiness? "all": "company"))
           .ForMember(x => x.eSupportPayWay, opt => opt.MapFrom(source => source.AllowedPayType))
           .ForMember(x => x.bOpen, opt => opt.MapFrom(source => source.Enabled))
           .ForMember(x => x.maxCount, opt => opt.MapFrom(source => source.MaxOrdersPerDay))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<Model.Staff, staffObj>()
            .ForMember(x => x.alias, opt => opt.MapFrom(source => source.NickName))
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.Photo))
            .ForMember(x => x.sex, opt => opt.MapFrom(source => source.Gender=="女"?true:false))
            .ForMember(x => x.realName, opt => opt.MapFrom(source => source.Name))
            .ForMember(x => x.identity, opt => opt.MapFrom(source => source.Code))
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
