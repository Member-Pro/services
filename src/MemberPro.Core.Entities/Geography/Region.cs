using System.Collections.Generic;

namespace MemberPro.Core.Entities.Geography
{
    public class Region : BaseEntity
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public virtual List<Division> Divisions { get; set; } = new List<Division>();
    }
}
