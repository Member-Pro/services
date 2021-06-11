using System;
using MemberPro.Core.Entities.Achievements;

namespace MemberPro.Core.Entities.Members
{
    public class TrackedAchievement : BaseEntity
    {
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int AchievementId { get; set; }
        public virtual Achievement Achievement { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    // public class TrackedAchievementRequirement : BaseEntity
    // {
    //     public int TrackedAchievementId { get; set; }
    //     public virtual TrackedAchievement TrackedAchievement { get; set; }

    //     public int AchievementStepId { get; set; }
    //     public virtual AchievementStep AchievementStep { get; set; }

    //     public DateTime? CompletedOn { get; set; }

    //     public string Comments { get; set; }
    // }
}
