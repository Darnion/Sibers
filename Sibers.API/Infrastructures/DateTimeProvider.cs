using Sibers.Common;

namespace Sibers.Api.Infrastructures
{
    /// <summary>
    /// Провайдер даты и времени
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow => DateTimeOffset.UtcNow;
    }
}
