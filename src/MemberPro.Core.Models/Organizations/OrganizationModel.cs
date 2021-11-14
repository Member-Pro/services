using System;

namespace MemberPro.Core.Models.Organizations
{
    public class OrganizationModel : BaseModel
    {
        public int? ParentId { get; set; }
        public virtual OrganizationModel Parent { get; set; }

        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
