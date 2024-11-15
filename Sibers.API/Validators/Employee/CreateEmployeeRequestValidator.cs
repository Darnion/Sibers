//using Sibers.API.ModelsRequest.Employee;
//using Sibers.Repositories.Contracts;
//using FluentValidation;

//namespace AutoRest.Api.Validators.Employee
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        public CreateEmployeeRequestValidator(IPersonReadRepository personReadRepository)
//        {

//            RuleFor(x => x.EmployeeType)
//                .NotNull()
//                .WithMessage("Должность не должна быть null");

//            RuleFor(x => x.PersonId)
//                .NotNull()
//                .NotEmpty()
//                .WithMessage("Личность не должна быть пустым или null")
//                .MustAsync(async (id, CancellationToken) =>
//                {
//                    var personExists = await personReadRepository.AnyByIdAsync(id, CancellationToken);
//                    return personExists;
//                })
//                .WithMessage("Такой личности не существует!");
//        }
//    }
//}
