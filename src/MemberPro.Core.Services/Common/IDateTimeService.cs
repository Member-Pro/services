using System;

namespace MemberPro.Core.Services.Common
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }

    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc { get; } = DateTime.UtcNow;
    }
}
