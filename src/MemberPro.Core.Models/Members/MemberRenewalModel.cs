using System;
using MemberPro.Core.Models.Plans;

namespace MemberPro.Core.Models.Members
{
    public class MemberRenewalModel : BaseModel
    {
        public MemberModel Member { get; set; }

        public MembershipPlanModel Plan { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime? PaidDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public string TransactionId { get; set; }

        public string Comments { get; set; }
    }

    public class CreateRenewalModel
    {
        public int MemberId { get; set; }
        public int PlanId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime? PaidDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public string TransactionId { get; set; }

        public string Comments { get; set; }
    }
}
