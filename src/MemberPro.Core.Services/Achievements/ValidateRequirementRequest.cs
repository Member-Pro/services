using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Models.Members;

namespace MemberPro.Core.Services.Achievements
{
    public class ValidateRequirementRequest
    {
        public MemberModel Member { get; set; }

        public AchievementModel Achievement { get; set; }
        public AchievementComponentModel Component { get; set; }
        public RequirementModel Requirement { get; set; }

        public MemberRequirementStateModel RequirementState { get; set; }
    }
}
