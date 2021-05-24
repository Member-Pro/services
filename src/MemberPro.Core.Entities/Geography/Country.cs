using System.Collections.Generic;

namespace MemberPro.Core.Entities.Geography
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual List<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
    }
}
