using MemberPro.Core.Enums;

namespace MemberPro.Core.Entities.Achievements
{
    public class Requirement : BaseEntity
    {
        public int ComponentId { get; set; }
        public virtual AchievementComponent Component { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public string ValidatorTypeName { get; set; }
        public RequirementValidationParameter[] ValidationParameters { get; set; }

        public RequirementType Type { get; set; }

        // TODO: If the ValidationParameters works out, these can go away
        public decimal? MinCount { get; set; }

        public decimal? MaxCount { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class RequirementValidationParameter
    {
        public string Key { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public FormInputType InputType { get; set; }
        public string[] Options { get; set; }

        public bool IsRequired { get; set; }

        public decimal? Minimum { get; set; }
        public decimal? Maximum { get; set; }
    }
}
