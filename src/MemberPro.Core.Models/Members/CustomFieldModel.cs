using MemberPro.Core.Entities.Members;

namespace MemberPro.Core.Models.Members
{
    public class CustomFieldModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsRequired { get; set; }

        public CustomFieldType FieldType { get; set; }
        public string ValueOptions { get; set; }
    }
}
