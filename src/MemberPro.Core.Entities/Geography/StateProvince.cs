namespace MemberPro.Core.Entities.Geography
{
    public class StateProvince : BaseEntity
    {
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
