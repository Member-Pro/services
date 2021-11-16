using System;
using MemberPro.Core.Models.Members;

namespace MemberPro.Core.Models.Organizations
{
    public class OfficerModel : BaseModel
    {
        public virtual OfficerPositionModel Position { get; set; }

        public virtual SimpleMemberModel Member { get; set; }

        public DateOnly? TermStart { get; set; }
        public DateOnly? TermEnd { get; set; }
    }
}
