using System;
using MemberPro.Core.Models.Achievements;

namespace MemberPro.Core.Models.Members
{
    public class MemberAchievementModel : BaseModel
    {
        public int MemberId { get; set; }
        public SimpleMemberModel Member { get; set; }

        public int AchievementId { get; set; }
        public AchievementModel Achievement { get; set; }

        public DateTime EarnedOn { get; set; }

        public bool DisplayPublicly { get; set; }
    }

    public class CreateMemberAchievementModel
    {
        public int MemberId { get; set; }
        public int AchievementId { get; set; }


        public DateTime EarnedOn { get; set; }

        public bool DisplayPublicly { get; set; }

        public int CreatedByMemberId { get; set; }
    }
}
