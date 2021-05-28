using System;
using MemberPro.Core.Models.Achievements;

namespace MemberPro.Core.Models.Members
{
    public class MemberAchievementModel : BaseModel
    {
        public int MemberId { get; set; }
        public MemberModel Member { get; set; }

        public AchievementModel Achievement { get; set; }

        public DateTime SubmittedOn { get; set; }
        public DateTime? ApprovedOn { get; set; }

        public MemberModel ApprovedByMember { get; set; }

        public bool DisplayPublicly { get; set; }
    }
}
