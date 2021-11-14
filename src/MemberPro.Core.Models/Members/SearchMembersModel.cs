using System;

namespace MemberPro.Core.Models.Members
{
    public class SearchMembersModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public int? CountryId { get; set; }
        public int? StateProvinceId { get; set; }

        public bool? ShowInDirectory { get; set; }

        public int? OrganizationId { get; set; }

        public int? PlanId { get; set; }

        public DateTime? JoinedOnFrom { get; set; }
        public DateTime? JoinedOnTo { get; set; }

        public DateTime? DateOfBirthFrom { get; set; }
        public DateTime? DateOfBirthTo { get; set; }
    }
}
