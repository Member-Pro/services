using System;
using MemberPro.Core.Models.Achievements;

namespace MemberPro.Core.Models.Members
{
    public class MemberAchievementProgressModel : BaseModel
    {
        public MemberModel Member { get; set; }

        public AchievementModel Achievement { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Amount { get; set; }

        public string Comments { get; set; }
    }
}
