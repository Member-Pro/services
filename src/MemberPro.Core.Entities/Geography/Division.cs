namespace MemberPro.Core.Entities.Geography
{
    public class Division : BaseEntity
    {
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}
