using MemberPro.Core.Enums;

namespace MemberPro.Core.Entities.Members
{
    public class CustomField : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsRequired { get; set; }

        public FormInputType FieldType { get; set; }
        public string ValueOptions { get; set; }
    }
}
