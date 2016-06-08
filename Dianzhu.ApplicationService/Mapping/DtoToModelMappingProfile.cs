using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dianzhu.Model;


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
            Mapper.CreateMap<ClientDTO, Model.Client>()
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<RefreshTokenDTO, RefreshToken>()
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<userObj, DZMembership>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(source => source.alias))
            .ForMember(x => x.AvatarUrl, opt => opt.MapFrom(source => source.imgUrl))
            .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull))
     ;

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
