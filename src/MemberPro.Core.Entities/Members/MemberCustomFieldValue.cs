namespace MemberPro.Core.Entities.Members
{
    public class MemberCustomFieldValue : BaseEntity
    {
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int FieldId { get; set; }
        public virtual CustomField Field { get; set; }

        public string Value { get; set; }
    }
}
