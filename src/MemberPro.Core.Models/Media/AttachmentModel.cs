using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MemberPro.Core.Enums;
using MemberPro.Core.Models.Members;

namespace MemberPro.Core.Models.Media
{
    public class AttachmentModel : BaseModel
    {
        public int OwnerId { get; set; }
        public SimpleMemberModel Owner { get; set; }

        ///<summary>
        /// The type of object this attachment belongs to (achievements, user profile, requirements, etc.)
        ///</summary>
        public string ObjectType { get; set; }
        public int? ObjectId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AttachmentType MediaType { get; set; }

        public string FileName { get; set; }
        public string SaveFileName { get; set; }

        public decimal FileSize { get; set; }
        public string ContentType { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Url { get; set; }

        public Dictionary<string, string> VariantUrls { get; set; } = new Dictionary<string, string>();
    }

    public class CreateAttachmentModel
    {
        public int OwnerId { get; set; }

        ///<summary>
        /// The type of object this attachment belongs to (achievements, user profile, requirements, etc.)
        ///</summary>
        public string ObjectType { get; set; }
        public int? ObjectId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AttachmentType MediaType { get; set; }

        public string FileName { get; set; }

        public decimal FileSize { get; set; }
        public string ContentType { get; set; }
    }
}
