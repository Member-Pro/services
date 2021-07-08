using System;
using MemberPro.Core.Entities.Members;

namespace MemberPro.Core.Entities.Media
{
    public class Attachment : BaseEntity
    {
        public int OwnerId { get; set; }
        public virtual Member Owner { get; set; }

        ///<summary>
        /// The type of object this attachment belongs to (achievements, user profile, requirements, etc.)
        ///</summary>
        public string ObjectType { get; set; }
        public int? ObjectId { get; set; }

        public AttachmentType MediaType { get; set; }

        public string FileName { get; set; }

        public decimal FileSize { get; set; }
        public string ContentType { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public enum AttachmentType
    {
        Photo = 1,

        Pdf = 5,
        WordDocument = 6,
        ExcelDocument = 7,

        OtherDocument = 10,
    }
}
