namespace MemberPro.Core.Models.Geography
{
    public class RegionModel : BaseModel
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }

    public class CreateRegionModel
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}
