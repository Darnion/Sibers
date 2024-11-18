using FluentValidation;
using Sibers.Api.ModelsRequest.Company;
using Sibers.Repositories.Contracts;

namespace Sibers.Api.Validators.Company
{
    /// <summary>
    /// Валидатор запроса изменения компании
    /// </summary>
    public class CompanyRequestValidator : AbstractValidator<CompanyRequest>
    {
        /// <summary>
        /// <inheritdoc cref="CompanyRequestValidator"/>
        /// </summary>
        public CompanyRequestValidator(ICompanyReadRepository companyReadRepository)
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage("Название компании не должно быть null")
                .NotEmpty()
                .WithMessage("Название компании не должно быть пустой")
                .MaximumLength(200)
                .WithMessage("Название компании не должно превышать 200 символов")
                .MustAsync(async (compReq, title, cancellationToken) =>
                {
                    var isTitleExists = await companyReadRepository.AnyOtherByTitleAsync(compReq.Id, title, cancellationToken);
                    return !isTitleExists;
                })
                .WithMessage("Название компании должно быть уникальным!");
        }
    }
}
