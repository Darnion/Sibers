namespace Sibers.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемый ресурс не найден
    /// </summary>
    public class SibersNotFoundException : SibersException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SibersNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public SibersNotFoundException(string message)
            : base(message)
        { }
    }
}
