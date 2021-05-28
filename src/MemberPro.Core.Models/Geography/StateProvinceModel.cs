namespace MemberPro.Core.Models.Geography
{
    public class StateProvinceModel : BaseModel
    {
        public CountryModel Country { get; set; }

        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
