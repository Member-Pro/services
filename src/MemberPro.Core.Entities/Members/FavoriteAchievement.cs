using System;
using MemberPro.Core.Entities.Achievements;

namespace MemberPro.Core.Entities.Members
{
    public class FavoriteAchievement : BaseEntity
    {
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int AchievementId { get; set; }
        public virtual Achievement Achievement { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
