using Sibers.Repositories.Contracts;
using FluentValidation;
using Sibers.Api.ModelsRequest.Project;

namespace Sibers.Api.Validators.EmployeeProject
{
    /// <summary>
    /// Валидатор запроса изменения связей проекта с работниками
    /// </summary>
    public class EmployeeProjectRequestValidator : AbstractValidator<EmployeeProjectRequest>
    {
        /// <summary>
        /// <inheritdoc cref="EmployeeProjectRequestValidator"/>
        /// </summary>
        public EmployeeProjectRequestValidator(IEmployeeReadRepository employeeReadRepository)
        {
            RuleForEach(x => x.WorkersIds)
                .MustAsync(async (id, cancellationToken) =>
                {
                    var isWorkerExists = await employeeReadRepository.AnyByIdAsync(id, cancellationToken);
                    return isWorkerExists;
                })
                .WithMessage((x, y) => $"Сотрудника с id={y} не существует");
        }
    }
}
