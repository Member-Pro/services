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

        public DateTime SubmittedOn { get; set; }
        public DateTime? ApprovedOn { get; set; }

        public int? ApprovedByMemberId { get; set; }
        public virtual Member ApprovedByMember { get; set; }

        public bool DisplayPublicly { get; set; }        
    }
}
