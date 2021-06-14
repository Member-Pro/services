using System;
using MemberPro.Core.Entities.Achievements;

namespace MemberPro.Core.Entities.Members
{
    public class MemberAchievement : BaseEntity
    {
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int AchievementId { get; set; }
        public virtual Achievement Achievement { get; set; }

        public DateTime EarnedOn { get; set; }

        public bool DisplayPublicly { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedByMemberId { get; set; }
        public virtual Member CreatedByMember { get; set; }
    }
}
