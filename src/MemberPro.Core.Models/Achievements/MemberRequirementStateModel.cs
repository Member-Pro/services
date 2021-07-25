using System;
using System.Collections.Generic;

namespace MemberPro.Core.Models.Achievements
{
    public class MemberRequirementStateModel : BaseModel
    {
        public int MemberId { get; set; }

        public int RequirementId { get; set; }

        public DateTime UpdatedOn { get; set; }

        public Dictionary<string, object> Data { get; set; }

        public object GetValue(string key)
        {
            if (!Data.ContainsKey(key))
            {
                return null;
            }

            return Data[key];
        }
    }

    public class UpdateMemberRequirementStateModel
    {
        public int MemberId { get; set; }

        public int RequirementId { get; set; }

        public Dictionary<string, object> Data { get; set; }
    }
}
