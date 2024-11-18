using FluentValidation;
using Sibers.Api.ModelsRequest.Employee;
using Sibers.Repositories.Contracts;

namespace Sibers.Api.Validators.Employee
{
    /// <summary>
    /// Валидатор запроса создания работника
    /// </summary>
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        /// <summary>
        /// <inheritdoc cref="CreateEmployeeRequestValidator"/>
        /// </summary>
        public CreateEmployeeRequestValidator(IEmployeeReadRepository employeeReadRepository)
        {
            RuleFor(x => x.EmployeeType)
                .NotNull()
                .WithMessage("Должность не должна быть null");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("Фамилия не должна быть null")
                .NotEmpty()
                .WithMessage("Фамилия не должна быть пустой")
                .MaximumLength(50)
                .WithMessage("Фамилия не должна превышать 50 символов");

            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("Имя не должно быть null")
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым")
                .MaximumLength(50)
                .WithMessage("Имя не должно превышать 50 символов");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("E-mail не соответствует формату")
                .MaximumLength(254)
                .WithMessage("E-mail не должен превышать 254 символа")
                .MustAsync(async (email, cancellationToken) =>
                {
                    var isEmailExists = await employeeReadRepository.AnyOtherByEmailAsync(Guid.Empty, email, cancellationToken);
                    return !isEmailExists;
                })
                .WithMessage("Email должен быть уникальным!");
        }
    }
}
