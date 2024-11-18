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
    public class CompanyServiceTests : SibersContextInMemory
    {
        private readonly ICompanyService companyService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyServiceTests"/>
        /// </summary>

        public CompanyServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            companyService = new CompanyService(
                new CompanyReadRepository(Reader),
                new CompanyWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение всех компаний возвращает empty
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            //Arrange

            // Act
            var result = await companyService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение всех компаний возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Company();
            var deletedTarget = TestDataGenerator.Company();

            deletedTarget.DeletedAt = DateTimeOffset.UtcNow;

            await Context.Companies.AddRangeAsync(target, deletedTarget);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await companyService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение компании по идентификатору возвращает <see cref="SibersEntityNotFoundException{TEntity}"/>
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => companyService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<SibersEntityNotFoundException<Company>>();
        }

        /// <summary>
        /// Получение компании по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Company();
            await Context.Companies.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await companyService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                });
        }

        /// <summary>
        /// Получение компании по названию возвращает <see cref="SibersNotFoundException"/>
        /// </summary>
        [Fact]
        public async Task GetByTitleShouldReturnException()
        {
            //Arrange
            var target = TestDataGenerator.Company();
            target.Title = "title";
            await Context.Companies.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => companyService.GetByTitleAsync("titl", CancellationToken);

            // Assert
            await act.Should().ThrowAsync<SibersNotFoundException>();
        }

        /// <summary>
        /// Получение компании по названию возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByTitleShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Company();
            target.Title = "title";
            await Context.Companies.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await companyService.GetByTitleAsync("title", CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                });
        }

        /// <summary>
        /// Добавление компании возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.CompanyRequestModel();
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            var act = await companyService.AddAsync(target, CancellationToken);

            //Assert

            var entity = Context.Companies.Single(x =>
                x.Id == act.Id);
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Изменение компании изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Company();
            await Context.Companies.AddAsync(target);
            target.Title = "title";

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.CompanyRequestModel();
            targetModel.Id = target.Id;
            targetModel.Title = "titleEdit";

            //Act
            var act = await companyService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Companies.Single(x =>
                x.Id == act.Id &&
                x.Title == targetModel.Title);
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление проекта работает
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Company();
            await Context.Companies.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => companyService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Companies.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

