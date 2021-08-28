using System;

namespace MemberPro.Core.Models.Members
{
    public class MemberRoleModel : BaseModel
    {
        public int MemberId { get; set; }
        public int RoleId { get; set; }

        public DateTime AddedOn { get; set; }

        public RoleModel Role { get; set; }
    }

    public class CreateMemberRoleModel
    {
        public int MemberId { get; set; }

        public int RoleId { get; set; }
    }
}
