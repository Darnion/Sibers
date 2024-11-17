using Sibers.Api.Validators.Employee;
using Sibers.Api.Validators.Project;
using Sibers.Api.Validators.Company;
using Sibers.Repositories.Contracts;
using Sibers.Services.Contracts.Exceptions;
using Sibers.Shared;
using FluentValidation;
using Sibers.Api.Infrastructures.Validator;
using Sibers.Api.Validators.EmployeeProject;

namespace Sibers.Api.Infrastructures.Validator
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(IEmployeeReadRepository employeeReadRepository,
            ICompanyReadRepository companyReadRepository)
        {

            Register<CreateEmployeeRequestValidator>(employeeReadRepository);
            Register<EmployeeRequestValidator>(employeeReadRepository);

            Register<CreateCompanyRequestValidator>(companyReadRepository);
            Register<CompanyRequestValidator>(companyReadRepository);

            Register<CreateProjectRequestValidator>(companyReadRepository, employeeReadRepository);
            Register<ProjectRequestValidator>(companyReadRepository, employeeReadRepository);
            Register<EmployeeProjectRequestValidator>(employeeReadRepository);
        }

        ///<summary>
        /// Регистрирует валидатор в словаре
        /// </summary>
        public void Register<TValidator>(params object[] constructorParams)
            where TValidator : IValidator
        {
            var validatorType = typeof(TValidator);
            var innerType = validatorType.BaseType?.GetGenericArguments()[0];
            if (innerType == null)
            {
                throw new ArgumentNullException($"Указанный валидатор {validatorType} должен быть generic от типа IValidator");
            }

            if (constructorParams?.Any() == true)
            {
                var validatorObject = Activator.CreateInstance(validatorType, constructorParams);
                if (validatorObject is IValidator validator)
                {
                    validators.TryAdd(innerType, validator);
                }
            }
            else
            {
                validators.TryAdd(innerType, Activator.CreateInstance<TValidator>());
            }
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new SibersValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
