using System;
using MemberPro.Core.Entities.Members;

namespace MemberPro.Core.Entities.Media
{
    public class Attachment : BaseEntity
    {
        public int OwnerId { get; set; }
        public virtual Member Owner { get; set; }

        ///<summary>
        /// The group this attachment belongs to (achievements, user profile, requirements, etc.)
        ///</summary>
        public string AttachmentGroup { get; set; }

        public AttachmentMediaType MediaType { get; set; }

        public string OriginalFileName { get; set; }
        public string SavedFileName { get; set; }

        public decimal FileSize { get; set; }
        public string ContentType { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public enum AttachmentMediaType
    {
        Photo = 1,

        Pdf = 5,
        WordDocument = 6,
        ExcelDocument = 7,

        OtherDocument = 10,
    }
}
