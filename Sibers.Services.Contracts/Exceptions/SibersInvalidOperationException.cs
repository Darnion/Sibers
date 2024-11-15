namespace Sibers.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибка выполнения операции
    /// </summary>
    public class SibersInvalidOperationException : SibersException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SibersInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public SibersInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}
