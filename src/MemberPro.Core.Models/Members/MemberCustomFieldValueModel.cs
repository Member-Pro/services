namespace MemberPro.Core.Models.Members
{
    public class MemberCustomFieldValueModel : BaseModel
    {
        public MemberModel Member { get; set; }

        public CustomFieldModel Field { get; set; }

        public string Value { get; set; }
    }
}
