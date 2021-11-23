using System;

namespace MemberPro.Core.Services.Common
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        DateOnly Today { get; }
    }

    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc { get; } = DateTime.UtcNow;

        public DateOnly Today { get; } = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
