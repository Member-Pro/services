namespace MemberPro.Core.Models.Geography
{
    public class DivisionModel : BaseModel
    {
        public virtual RegionModel Region { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }

    public class CreateDivisionModel
    {
        public int RegionId { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}
