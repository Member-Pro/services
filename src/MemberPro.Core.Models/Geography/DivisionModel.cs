namespace MemberPro.Core.Models.Geography
{
    public class DivisionModel : BaseModel
    {
        public virtual RegionModel Region { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}
