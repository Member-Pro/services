using System;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Entities.Organizations;
using MemberPro.Core.Models.Geography;
using MemberPro.Core.Models.Plans;

namespace MemberPro.Core.Models.Members
{
    public class MemberModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public MemberStatus Status { get; set; }
        public DateTime JoinedOn { get; set; }

        public string EmailAddress { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CountryId { get;set; }
        public CountryModel Country { get; set; }

        public int StateProvinceId { get; set; }
        public StateProvinceModel StateProvince { get; set; }

        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public bool ShowInDirectory { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public string Biography { get; set; }
        public string Interests { get; set; }

        public int? CurrentPlanId { get; set; }
        public virtual MembershipPlanModel CurrentPlan { get; set; }

        //public virtual List<MemberCustomFieldValue> FieldValues { get; set; } = new List<MemberCustomFieldValue>();

        //public virtual List<MemberRenewalModel> Renewals { get; set; } = new List<MemberRenewalModel>();

        //public virtual List<MemberAchievement> Achievements { get; set; } = new List<MemberAchievement>();
        //public virtual List<MemberAchievementProgress> AchievementProgressRecords { get; set; } = new List<MemberAchievementProgress>();
    }

    public class SimpleMemberModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public MemberStatus Status { get; set; }
        public DateTime JoinedOn { get; set; }

        public string EmailAddress { get; set; }
    }

    public class RegisterUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public int CountryId { get; set; }
        public int StateProvinceId { get; set; }

        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public int PlanId { get; set; }
    }
}
