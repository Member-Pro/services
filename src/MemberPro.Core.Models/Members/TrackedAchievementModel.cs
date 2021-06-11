using System;
using MemberPro.Core.Entities.Achievements;
using MemberPro.Core.Entities.Members;

namespace MemberPro.Core.Models.Members
{
    public class TrackedAchievementModel : BaseModel
    {
        public int MemberId { get; set; }
        public SimpleMemberModel Member { get; set; }

        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public class CreateTrackedAchievementModel
    {
        public int MemberId { get; set; }

        public int AchievementId { get; set; }

        public string Notes { get; set; }
    }
}
