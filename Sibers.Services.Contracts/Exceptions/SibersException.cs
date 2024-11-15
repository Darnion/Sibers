namespace Sibers.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений
    /// </summary>
    public abstract class SibersException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SibersException"/> без параметров
        /// </summary>
        protected SibersException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SibersException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected SibersException(string message)
            : base(message) { }
    }
}
