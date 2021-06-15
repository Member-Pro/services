using System;
using System.Collections.Generic;

namespace MemberPro.Core.Models.Achievements
{
    public class AchievementModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string InfoUrl { get; set; }

        public string ImageFilename { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual List<AchievementRequirementModel> Requirements { get; set; } = new List<AchievementRequirementModel>();
    }

    public class CreateAchievementModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string InfoUrl { get; set; }

        public string ImageFilename { get; set; }

        public bool IsDisabled { get; set; }
    }
}
