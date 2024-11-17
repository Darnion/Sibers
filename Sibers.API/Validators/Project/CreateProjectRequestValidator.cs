using Sibers.Api.ModelsRequest.Project;
using Sibers.Repositories.Contracts;
using FluentValidation;

namespace Sibers.Api.Validators.Project
{
    /// <summary>
    /// Валидатор запроса создания проекта
    public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
    {
        /// <summary>
        /// <inheritdoc cref="CreateProjectRequestValidator"/>
        /// </summary>
        public CreateProjectRequestValidator(ICompanyReadRepository companyReadRepository,
            IEmployeeReadRepository employeeReadRepository)
        {

            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage("Название проекта не должно быть null")
                .NotEmpty()
                .WithMessage("Название проекта не должно быть пустым");

            RuleFor(x => x.Priority)
                .NotNull()
                .WithMessage("Приоритет не должен быть null")
                .NotEmpty()
                .WithMessage("Приоритет не должен быть пустым")
                .Must(x => x >= 0)
                .WithMessage("Приоритет должен быть неотрицательным");

            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .WithMessage("Дата окончания проекта не должна быть раньше даты начала");

            RuleFor(x => x.CustomerCompanyId)
                .NotNull()
                .WithMessage("Идентификатор компании-заказчика не должен быть null")
                .NotEmpty()
                .WithMessage("Идентификатор компании-заказчика не должен быть пустым")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var isCompanyExists = await companyReadRepository.AnyByIdAsync(id, cancellationToken);
                    return isCompanyExists;
                })
                .WithMessage("Компания-заказчик не существует");

            RuleFor(x => x.ContractorCompanyId)
                .NotNull()
                .WithMessage("Идентификатор компании-исполнителя не должен быть null")
                .NotEmpty()
                .WithMessage("Идентификатор компании-исполнителя не должен быть пустым")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var isCompanyExists = await companyReadRepository.AnyByIdAsync(id, cancellationToken);
                    return isCompanyExists;
                })
                .WithMessage("Компания-исполнитель не существует");

            RuleFor(x => x.DirectorId)
                .NotNull()
                .WithMessage("Идентификатор руководителя не должен быть null")
                .NotEmpty()
                .WithMessage("Идентификатор руководителя не должен быть пустым")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var isEmployeeExists = await employeeReadRepository.AnyByIdAsync(id, cancellationToken);
                    return isEmployeeExists;
                })
                .WithMessage("Руководитель не существует");
        }
    }
}
