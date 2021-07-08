using System;
using MemberPro.Core.Entities.Members;

namespace MemberPro.Core.Entities.Achievements
{
    public class AchievementActivity : BaseEntity
    {
        public int AchievementId { get; set; }
        public virtual Achievement Achievement { get; set; }

        public int RequirementId { get; set; }
        public virtual AchievementRequirement Requirement { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public DateTime ActivityDate { get; set; }

        public string Description { get; set; }

        public decimal? QuantityCompleted { get; set; }

        public string Comments { get; set; }

        // // TODO: Possibly move witness/attestation to a separate entity?
        // public int? WitnessMemberId { get; set; }
        // public virtual Member Witness { get; set; }

        // public DateTime? WitnessAcknowledgedOn { get; set; }
    }
}
