using AutoMapper;
using Sibers.Repositories.Implementations;
using Sibers.Services.Automappers;
using Sibers.Services.Contracts.Interfaces;
using Sibers.Services.Implementations;
using FluentAssertions;
using Xunit;
using Sibers.Context.Tests;
using Sibers.Services.Tests;
using Sibers.Context.Contracts.Models;
using Sibers.Services.Contracts.Exceptions;

namespace Sibers.Services.Tests.Tests
{
    public class EmployeeServiceTests : SibersContextInMemory
    {
        private readonly IEmployeeService employeeService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeServiceTests"/>
        /// </summary>

        public EmployeeServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            employeeService = new EmployeeService(
                new EmployeeReadRepository(Reader),
                new EmployeeWriteRepository(WriterContext),
                new EmployeeProjectReadRepository(Reader),
                new EmployeeProjectWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение всех работников возвращает empty
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            //Arrange

            // Act
            var result = await employeeService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение всех работников возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            var deletedTarget = TestDataGenerator.Employee();

            deletedTarget.DeletedAt = DateTimeOffset.UtcNow;

            await Context.Employees.AddRangeAsync(target, deletedTarget);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение всех работников по части полного имени возвращает empty
        /// </summary>
        [Fact]
        public async Task GetAllByNameShouldReturnEmpty()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            target.LastName = "target";

            var deletedTarget = TestDataGenerator.Employee();
            deletedTarget.LastName = "target";
            deletedTarget.FirstName = "deleted";
            deletedTarget.DeletedAt = DateTimeOffset.UtcNow;

            await Context.Employees.AddRangeAsync(target, deletedTarget);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeService.GetAllByNameAsync("sdfgdhfgdh", CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение всех работников по части полного имени возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllByNameShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            target.LastName = "target";

            var deletedTarget = TestDataGenerator.Employee();
            deletedTarget.LastName = "target";
            deletedTarget.FirstName = "deleted";
            deletedTarget.DeletedAt = DateTimeOffset.UtcNow;

            await Context.Employees.AddRangeAsync(target, deletedTarget);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeService.GetAllByNameAsync("tar", CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает <see cref="SibersEntityNotFoundException{TEntity}"/>
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => employeeService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<SibersEntityNotFoundException<Employee>>();
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            await Context.Employees.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await employeeService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.EmployeeType
                });
        }

        /// <summary>
        /// Добавление работника возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.EmployeeRequestModel();
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            var act = await employeeService.AddAsync(target, CancellationToken);

            //Assert

            var entity = Context.Employees.Single(x =>
                x.Id == act.Id);
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Изменение работника, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            await Context.Employees.AddAsync(target);
            target.Email = "email";

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.EmployeeRequestModel();
            targetModel.Id = target.Id;
            targetModel.Email = "emailEdit";

            //Act
            var act = await employeeService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Employees.Single(x =>
                x.Id == act.Id &&
                x.Email == targetModel.Email);
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление работника работает
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Employee();
            await Context.Employees.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => employeeService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Employees.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

