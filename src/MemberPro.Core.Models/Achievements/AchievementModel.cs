using System;
using System.Collections.Generic;
using MemberPro.Core.Entities.Achievements;

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
