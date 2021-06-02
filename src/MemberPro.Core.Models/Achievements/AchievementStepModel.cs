using System;

namespace MemberPro.Core.Models.Achievements
{
    public class AchievementStepModel : BaseModel
    {
        public virtual AchievementModel Achievement { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsRequired { get; set; }
        public int? MinimumCount { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class CreateAchievementStepModel
    {
        public int AchievementId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsRequired { get; set; }
        public int? MinimumCount { get; set; }

        public bool IsDisabled { get; set; }
    }
}
