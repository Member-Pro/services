using System;

namespace MemberPro.Core.Entities.Members
{
    public class MemberRole : BaseEntity
    {
        public int MemberId { get; set; }
        public int RoleId { get; set; }

        public DateTime AddedOn { get; set; }

        public virtual Member Member { get; set; }
        public virtual Role Role { get; set; }
    }
}
