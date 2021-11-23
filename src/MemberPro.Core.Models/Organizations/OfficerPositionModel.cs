using MemberPro.Core.Enums;

namespace MemberPro.Core.Models.Organizations
{
    public class OfficerPositionModel : BaseModel
    {
        public virtual OrganizationModel Organization { get; set; }

        public string Title { get; set; }

        public OfficerPositionType PositionType { get; set; }
    }
}
