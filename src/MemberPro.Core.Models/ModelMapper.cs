using AutoMapper;
using MemberPro.Core.Entities.Achievements;
using MemberPro.Core.Entities.Geography;
using MemberPro.Core.Entities.Media;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Entities.Organizations;
using MemberPro.Core.Entities.Plans;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Models.Geography;
using MemberPro.Core.Models.Media;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Models.Organizations;
using MemberPro.Core.Models.Plans;

namespace MemberPro.Core.Models
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<Achievement, AchievementModel>();
            CreateMap<AchievementActivity, AchievementActivityModel>();
            CreateMap<AchievementComponent, AchievementComponentModel>();
            CreateMap<Requirement, RequirementModel>();
            CreateMap<RequirementModel, Requirement>(); // This is stored as JSON so need to be able to map back
            CreateMap<RequirementValidationParameter, RequirementValidationParameterModel>();
            CreateMap<RequirementValidationParameterModel, RequirementValidationParameter>();
            CreateMap<Attachment, AttachmentModel>();

            CreateMap<Country, CountryModel>();
            CreateMap<Organization, OrganizationModel>();
            CreateMap<StateProvince, StateProvinceModel>();
            CreateMap<OfficerPosition, OfficerPositionModel>();
            CreateMap<Officer, OfficerModel>();

            CreateMap<CustomField, CustomFieldModel>();
            CreateMap<MemberAchievement, MemberAchievementModel>();
            CreateMap<MemberAchievementProgress, MemberAchievementProgressModel>();
            CreateMap<MemberRequirementState, MemberRequirementStateModel>();
            CreateMap<Member, MemberModel>();
            CreateMap<Member, SimpleMemberModel>();
            CreateMap<MemberRenewal, MemberRenewalModel>();
            CreateMap<MemberRole, MemberRoleModel>();
            CreateMap<Role, RoleModel>();
            CreateMap<FavoriteAchievement, FavoriteAchievementModel>();

            CreateMap<MembershipPlan, MembershipPlanModel>();
        }
    }
}
