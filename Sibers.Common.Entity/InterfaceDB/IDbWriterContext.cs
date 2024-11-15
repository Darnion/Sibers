namespace Sibers.Common.Entity.InterfaceDB
{
    /// <summary>
    /// Определяет контекст репозитория записи сущностей
    /// </summary>
    public interface IDbWriterContext
    {
        /// <inheritdoc cref="IDbWriter"/>
        IDbWriter Writer { get; }


        /// <inheritdoc cref="IUnitOfWork"/>
        IUnitOfWork UnitOfWork { get; }


        /// <inheritdoc cref="IDateTimeProvider"/>
        IDateTimeProvider DateTimeProvider { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        /// <remarks>В реальной системе с авторизацией тут будет IIdentity</remarks>
        string UserName { get; }
    }
}