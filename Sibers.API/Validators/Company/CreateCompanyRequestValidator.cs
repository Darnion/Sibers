using Sibers.Api.ModelsRequest.Company;
using Sibers.Repositories.Contracts;
using FluentValidation;

namespace Sibers.Api.Validators.Company
{
    /// <summary>
    /// Валидатор запроса создания компании
    /// </summary>
    public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
    {
        /// <summary>
        /// <inheritdoc cref="CreateCompanyRequestValidator"/>
        /// </summary>
        public CreateCompanyRequestValidator(ICompanyReadRepository companyReadRepository)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage("Название компании не должно быть null")
                .NotEmpty()
                .WithMessage("Название компании не должно быть пустой")
                .MaximumLength(200)
                .WithMessage("Название компании не должно превышать 200 символов")
                .MustAsync(async (title, cancellationToken) =>
                {
                    var isTitleExists = await companyReadRepository.AnyOtherByTitleAsync(Guid.Empty, title, cancellationToken);
                    return !isTitleExists;
                })
                .WithMessage("Название компании должно быть уникальным!");
        }
    }
}
