using System;
using MemberPro.Core.Entities.Achievements;

namespace MemberPro.Core.Models.Achievements
{
    public class AchievementComponentModel : BaseModel
    {
        public int AchievementId { get; set; }
        // public AchievementModel Achievement { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public RequirementModel[] Requirements { get; set; }
    }

    public class CreateAchievementComponentModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public RequirementModel[] Requirements { get; set; }
    }

    public class RequirementModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public RequirementType Type { get; set; }

        public decimal? MinCount { get; set; }

        public decimal? MaxCount { get; set; }
    }
}
