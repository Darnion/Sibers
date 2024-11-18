using Sibers.Context.Contracts.Enums;
using Sibers.Context.Contracts.Models;
using Sibers.Services.Contracts.ModelsRequest;

namespace Sibers.Services.Tests
{
    static internal class TestDataGenerator
    {
        static internal Employee Employee(Action<Employee>? action = null)
        {
            var item = new Employee
            {
                Id = Guid.NewGuid(),
                EmployeeType = EmployeeTypes.Worker,
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
                Email = $"Email{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal EmployeeRequestModel EmployeeRequestModel(Action<EmployeeRequestModel>? action = null)
        {
            var item = new EmployeeRequestModel
            {
                Id = Guid.NewGuid(),
                EmployeeType = EmployeeTypes.Worker,
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
                Email = $"Email{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Company Company(Action<Company>? action = null)
        {
            var item = new Company
            {
                Id = Guid.NewGuid(),
                Title = $"Title{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal CompanyRequestModel CompanyRequestModel(Action<CompanyRequestModel>? action = null)
        {
            var item = new CompanyRequestModel
            {
                Id = Guid.NewGuid(),
                Title = $"Title{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Project Project(Action<Project>? action = null)
        {
            var item = new Project
            {
                Id = Guid.NewGuid(),
                Title = $"Title{Guid.NewGuid():N}",
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow,
                Priority = 1,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal ProjectRequestModel ProjectRequestModel(Action<ProjectRequestModel>? action = null)
        {
            var item = new ProjectRequestModel
            {
                Id = Guid.NewGuid(),
                Title = $"Title{Guid.NewGuid():N}",
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow,
                Priority = 1,
            };

            action?.Invoke(item);
            return item;
        }

        static internal EmployeeProject EmployeeProject(Action<EmployeeProject>? action = null)
        {
            var item = new EmployeeProject
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal EmployeeProjectRequestModel EmployeeProjectRequestModel(Action<EmployeeProjectRequestModel>? action = null)
        {
            var item = new EmployeeProjectRequestModel
            {

            };

            action?.Invoke(item);
            return item;
        }
    }
}
