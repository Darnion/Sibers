namespace Sibers.Shared
{
    /// <summary>
    /// Модель инвалидации запросов
    /// </summary>
    public class InvalidateItemModel
    {
        /// <summary>
        /// Создаёт <see cref="InvalidateItemModel"/>
        /// </summary>
        public static InvalidateItemModel New(string field, string message)
            => new InvalidateItemModel(field, message);

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="InvalidateItemModel"/>
        /// </summary>
        private InvalidateItemModel(string field, string message)
        {
            Field = field;
            Message = message;
        }

        /// <summary>
        /// Имя инвалидного поля
        /// </summary>
        /// <remarks>Если пустое, значит инвалидация относится ко всей модели</remarks>
        public string Field { get; }

        /// <summary>
        /// Сообщение инвалидации
        /// </summary>
        public string Message { get; }
    }
}