namespace Sibers.Api.Infrastructures.Validator
{
    /// <summary>
    /// Сервис валидации
    /// </summary>
    public interface IApiValidatorService
    {
        /// <summary>
        /// Валидирует модель
        /// </summary>
        Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class;
    }
}
