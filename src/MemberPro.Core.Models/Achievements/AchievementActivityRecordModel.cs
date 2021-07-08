using System;

namespace MemberPro.Core.Models.Achievements
{
    public class AchievementActivityModel : BaseModel
    {
        public int AchievementId { get; set; }
        public virtual AchievementModel Achievement { get; set; }

        public int RequirementId { get; set; }
        public virtual AchievementRequirementModel Requirement { get; set; }

        public int MemberId { get; set; }

        public DateTime ActivityDate { get; set; }

        public string Description { get; set; }

        public decimal? QuantityCompleted { get; set; }

        public string Comments { get; set; }
    }

    public class CreateAchievementActivityModel : BaseModel
    {
        public int AchievementId { get; set; }

        public int RequirementId { get; set; }

        public int MemberId { get; set; }

        public DateTime ActivityDate { get; set; }

        public string Description { get; set; }

        public decimal? QuantityCompleted { get; set; }

        public string Comments { get; set; }
    }
}
