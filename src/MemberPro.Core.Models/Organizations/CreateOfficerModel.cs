using System;

namespace MemberPro.Core.Models.Organizations
{
    public class CreateOfficerModel
    {
        public int PositionId { get; set; }
        public int MemberId { get; set; }
        public DateOnly? TermStart { get; set; }
        public DateOnly? TermEnd { get; set; }
    }
}
