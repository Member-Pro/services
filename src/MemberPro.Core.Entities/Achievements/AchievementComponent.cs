using System;

namespace MemberPro.Core.Entities.Achievements
{
    public class AchievementComponent : BaseEntity
    {
        public int AchievementId { get; set; }
        public virtual Achievement Achievement { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public Requirement[] Requirements { get; set; }
    }
}