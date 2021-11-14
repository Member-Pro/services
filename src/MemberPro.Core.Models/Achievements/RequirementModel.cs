using MemberPro.Core.Enums;

namespace MemberPro.Core.Models.Achievements
{
    public class RequirementModel : BaseModel
    {
        public int ComponentId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public string ValidatorTypeName { get; set; }
        public RequirementValidationParameterModel[] ValidationParameters { get; set; }

        public RequirementType Type { get; set; }

        public decimal? MinCount { get; set; }

        public decimal? MaxCount { get; set; }
    }

    public class RequirementValidationParameterModel
    {
        public string Key { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string InputType { get; set; }
        public string[] Options { get; set; }

        public bool IsRequired { get; set; }

        public decimal? Minimum { get; set; }
        public decimal? Maximum { get; set; }

        public string Value { get; set; }
    }
}
