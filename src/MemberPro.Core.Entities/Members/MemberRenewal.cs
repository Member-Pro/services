using System;
using MemberPro.Core.Entities.Plans;

namespace MemberPro.Core.Entities.Members
{
    public class MemberRenewal : BaseEntity
    {
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int PlanId { get; set; }
        public virtual MembershipPlan Plan { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime? PaidDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public string TransactionId { get; set; }

        public string Comments { get; set; }
    }
}
