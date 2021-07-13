using MemberPro.Core.Entities.Achievements;

namespace MemberPro.Core.Models.Achievements
{
    public class RequirementModel : BaseModel
    {
        public int ComponentId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ValidatorTypeName { get; set; }
        public object ValidationParameters { get; set; }

        public RequirementType Type { get; set; }

        public decimal? MinCount { get; set; }

        public decimal? MaxCount { get; set; }
    }
}
