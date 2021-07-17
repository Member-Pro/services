using System;
using System.Collections.Generic;
using MemberPro.Core.Entities.Members;

namespace MemberPro.Core.Entities.Achievements
{
    public class MemberRequirementState : BaseEntity
    {
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int RequirementId { get; set; }
        public virtual Requirement Requirement { get; set; }

        public DateTime UpdatedOn { get; set; }

        public Dictionary<string, object> Data { get; set; }
    }
}
