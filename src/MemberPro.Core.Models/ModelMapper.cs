using AutoMapper;
using MemberPro.Core.Entities.Achievements;
using MemberPro.Core.Entities.Geography;
using MemberPro.Core.Entities.Media;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Entities.Plans;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Models.Geography;
using MemberPro.Core.Models.Media;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Models.Plans;

namespace MemberPro.Core.Models
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<Achievement, AchievementModel>();
            CreateMap<AchievementActivityRecord, AchievementActivityRecordModel>();
            CreateMap<AchievementRequirement, AchievementRequirementModel>();
            CreateMap<Attachment, AttachmentModel>();

            CreateMap<Country, CountryModel>();
            CreateMap<Division, DivisionModel>();
            CreateMap<Region, RegionModel>();
            CreateMap<StateProvince, StateProvinceModel>();

            CreateMap<CustomField, CustomFieldModel>();
            CreateMap<MemberAchievement, MemberAchievementModel>();
            CreateMap<MemberAchievementProgress, MemberAchievementProgressModel>();
            CreateMap<Member, MemberModel>();
            CreateMap<Member, SimpleMemberModel>();
            CreateMap<MemberRenewal, MemberRenewalModel>();
            CreateMap<TrackedAchievement, TrackedAchievementModel>();

            CreateMap<MembershipPlan, MembershipPlanModel>();
        }
    }
}
