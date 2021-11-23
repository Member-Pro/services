using System;
using MemberPro.Core.Entities.Members;

namespace MemberPro.Core.Entities.Organizations
{
    public class Officer : BaseEntity
    {
        public int PositionId { get; set; }
        public virtual OfficerPosition Position { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public DateOnly? TermStart { get; set; }
        public DateOnly? TermEnd { get; set; }
    }
}
