using System;
using System.Collections.Generic;

namespace MemberPro.Core.Entities.Achievements
{
    public class Achievement : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string InfoUrl { get; set; }

        public string ImageFilename { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public virtual List<AchievementComponent> Components { get; set; } = new List<AchievementComponent>();
    }
}
