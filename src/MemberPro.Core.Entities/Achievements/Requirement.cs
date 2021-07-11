namespace MemberPro.Core.Entities.Achievements
{
    public class Requirement : BaseEntity
    {
        // public int ComponentId { get; set; }
        // public virtual AchievementComponent Component { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public RequirementType Type { get; set; }

        public decimal? MinCount { get; set; }

        public decimal? MaxCount { get; set; }
    }

    public enum RequirementType
    {
        Completion = 1,

        Score = 2,
        Count = 5,
        Verifications = 10,
    }
}
