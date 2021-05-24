using System;
using System.Collections.Generic;
using MemberPro.Core.Entities.Geography;

namespace MemberPro.Core.Entities.Members
{
    public class Member : BaseEntity
    {
        public string SubjectId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public MemberStatus Status { get; set; }
        public DateTime JoinedOn { get; set; }

        public string EmailAddress { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int StateProvinceId { get; set; }
        public virtual StateProvince StateProvince { get; set; }

        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public bool ShowInDirectory { get; set; }

        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        public int? DivisionId { get; set; }
        public virtual Division Division { get; set; }

        public string Biography { get; set; }
        public string Interests { get; set; }

        public virtual List<MemberCustomFieldValue> FieldValues { get; set; } = new List<MemberCustomFieldValue>();

        public virtual List<MemberRenewal> Renewals { get; set; } = new List<MemberRenewal>();

        public virtual List<MemberAchievement> Achievements { get; set; } = new List<MemberAchievement>();
        public virtual List<MemberAchievementProgress> AchievementProgressRecords { get; set; } = new List<MemberAchievementProgress>();
    }
}
