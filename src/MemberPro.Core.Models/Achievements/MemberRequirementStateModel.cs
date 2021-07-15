using System;

namespace MemberPro.Core.Models.Achievements
{
    public class MemberRequirementStateModel : BaseModel
    {
        public int MemberId { get; set; }

        public int RequirementId { get; set; }

        public DateTime UpdatedOn { get; set; }

        public object Data { get; set; }
    }
}
