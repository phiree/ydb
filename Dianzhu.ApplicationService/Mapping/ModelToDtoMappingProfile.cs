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
            Mapper.CreateMap<Model.Client,ClientDTO > ()
     .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<Model.RefreshToken,RefreshTokenDTO > ()
     .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<Model.DZMembership,userObj>()
            .ForMember(x => x.alias, opt => opt.MapFrom(source => source.UserName))
            .ForMember(x => x.imgUrl, opt => opt.MapFrom(source => source.AvatarUrl))
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
