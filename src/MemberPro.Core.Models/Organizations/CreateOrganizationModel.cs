namespace MemberPro.Core.Models.Organizations
{
    public class CreateOrganizationModel
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
    }
}
