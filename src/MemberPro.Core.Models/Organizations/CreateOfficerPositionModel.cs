using MemberPro.Core.Enums;

namespace MemberPro.Core.Models.Organizations
{
    public class CreateOfficerPositionModel
    {
        public int OrganizationId { get; set; }

        public string Title { get; set; }

        public OfficerPositionType PositionType { get; set; }
    }
}
