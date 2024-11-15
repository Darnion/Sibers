namespace Sibers.Common
{
    /// <summary>
    /// Интерфейс получения даты
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Текущий момент (utc)
        /// </summary>
        DateTimeOffset UtcNow { get; }
    }
}
