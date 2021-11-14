using System;
using System.Collections.Generic;

namespace MemberPro.Core.Entities.Organizations
{
    public class Organization : BaseEntity
    {
        public int? ParentId { get; set; }
        public virtual Organization Parent { get; set; }

        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<Organization> Children { get; set; } = new List<Organization>();
    }
}
