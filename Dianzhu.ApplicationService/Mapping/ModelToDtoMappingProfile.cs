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
            Mapper.CreateMap<Model.Client,ClientDTO > ();
            Mapper.CreateMap<Model.RefreshToken,RefreshTokenDTO > ();
            Mapper.CreateMap<Model.DZMembership,userObj>();
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
