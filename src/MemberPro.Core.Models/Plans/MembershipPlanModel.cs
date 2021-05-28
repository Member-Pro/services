using System;

namespace MemberPro.Core.Models.Plans
{
    public class MembershipPlanModel : BaseModel
    {
        public string Name { get; set; }
        public string SKU { get; set; }

        public string Description { get; set; }

        public DateTime AvailableStartDate { get; set; }
        public DateTime? AvailableEndDate { get; set; }

        public decimal Price { get; set; }

        public int DurationInMonths { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }        
    }
}
