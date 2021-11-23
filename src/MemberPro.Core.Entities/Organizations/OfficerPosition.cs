using MemberPro.Core.Enums;

namespace MemberPro.Core.Entities.Organizations
{
    public class OfficerPosition : BaseEntity
    {
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public string Title { get; set; }

        public OfficerPositionType PositionType { get; set; }
    }
}
