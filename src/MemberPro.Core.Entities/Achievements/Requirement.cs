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

        public ParameterInputType InputType { get; set; }
        public string[] Options { get; set; }

        public bool IsRequired { get; set; }

        public decimal? Minimum { get; set; }
        public decimal? Maximum { get; set; }
    }

    public enum ParameterInputType
    {
        TextBox = 1,
        TextArea = 2,
        Checkbox = 5,
        DatePicker = 6,
        DropDownList = 10,

        FileSelectorSingle = 20,
        FileSelectorMultiple = 21,
    }

    public enum RequirementType
    {
        Completion = 1,

        Score = 2,
        Count = 5,
        Verifications = 10,
    }
}
