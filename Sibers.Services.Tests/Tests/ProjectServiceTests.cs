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
    public class ProjectServiceTests : SibersContextInMemory
    {
        private readonly IProjectService projectService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ProjectServiceTests"/>
        /// </summary>

        public ProjectServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            projectService = new ProjectService(
                new ProjectReadRepository(Reader),
                new ProjectWriteRepository(WriterContext),
                new CompanyReadRepository(Reader),
                new EmployeeProjectReadRepository(Reader),
                new EmployeeProjectWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение всех проектов возвращает empty
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            //Arrange

            // Act
            var result = await projectService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение всех проектов возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var custComp = TestDataGenerator.Company();
            var contComp = TestDataGenerator.Company();
            var director = TestDataGenerator.Employee();

            await Context.Companies.AddRangeAsync(custComp, contComp);
            await Context.Employees.AddAsync(director);

            var target = TestDataGenerator.Project();
            target.CustomerCompanyId = custComp.Id;
            target.ContractorCompanyId = contComp.Id;
            target.DirectorId = director.Id;

            var deletedTarget = TestDataGenerator.Project();
            deletedTarget.CustomerCompanyId = custComp.Id;
            deletedTarget.ContractorCompanyId = contComp.Id;
            deletedTarget.DirectorId = director.Id;
            deletedTarget.DeletedAt = DateTimeOffset.UtcNow;

            await Context.Projects.AddRangeAsync(target, deletedTarget);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await projectService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение проекта по идентификатору возвращает <see cref="SibersEntityNotFoundException{TEntity}"/>
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => projectService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<SibersEntityNotFoundException<Project>>();
        }

        /// <summary>
        /// Получение проекта по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var custComp = TestDataGenerator.Company();
            var contComp = TestDataGenerator.Company();
            var director = TestDataGenerator.Employee();

            await Context.Companies.AddRangeAsync(custComp, contComp);
            await Context.Employees.AddAsync(director);

            var target = TestDataGenerator.Project();
            target.CustomerCompanyId = custComp.Id;
            target.ContractorCompanyId = contComp.Id;
            target.DirectorId = director.Id;

            await Context.Projects.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await projectService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                });
        }

        /// <summary>
        /// Добавление проекта возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.ProjectRequestModel();
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            var act = await projectService.AddAsync(target, CancellationToken);

            //Assert

            var entity = Context.Projects.Single(x =>
                x.Id == act.Id);
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Изменение проекта изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Project();
            await Context.Projects.AddAsync(target);
            target.Title = "title";

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.ProjectRequestModel();
            targetModel.Id = target.Id;
            targetModel.Title = "titleEdit";

            //Act
            var act = await projectService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Projects.Single(x =>
                x.Id == act.Id &&
                x.Title == targetModel.Title);
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление связей проекта с работниками работает
        /// </summary>
        [Fact]
        public async Task LinkWorkersShouldWork()
        {
            //Arrange
            var custComp = TestDataGenerator.Company();
            var contComp = TestDataGenerator.Company();
            var director = TestDataGenerator.Employee();
            var empl1 = TestDataGenerator.Employee();
            var empl2 = TestDataGenerator.Employee();

            await Context.Companies.AddRangeAsync(custComp, contComp);
            await Context.Employees.AddRangeAsync(director, empl1, empl2);

            var target = TestDataGenerator.Project();
            target.CustomerCompanyId = custComp.Id;
            target.ContractorCompanyId = contComp.Id;
            target.DirectorId = director.Id;

            await Context.Projects.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            var emplProjReqModel = TestDataGenerator.EmployeeProjectRequestModel();
            emplProjReqModel.ProjectId = target.Id;
            emplProjReqModel.WorkersIds.Add(empl1.Id);
            emplProjReqModel.WorkersIds.Add(empl2.Id);

            //Act
            Func<Task> act = () => projectService.LinkWorkersAsync(emplProjReqModel, CancellationToken);

            //Assert
            await act.Should().NotThrowAsync();
        }

        /// <summary>
        /// Удаление связей проекта с работниками работает
        /// </summary>
        [Fact]
        public async Task UnlinkWorkersShouldWork()
        {
            //Arrange
            var custComp = TestDataGenerator.Company();
            var contComp = TestDataGenerator.Company();
            var director = TestDataGenerator.Employee();
            var empl1 = TestDataGenerator.Employee();
            var empl2 = TestDataGenerator.Employee();

            await Context.Companies.AddRangeAsync(custComp, contComp);
            await Context.Employees.AddRangeAsync(director, empl1, empl2);

            var target = TestDataGenerator.Project();
            target.CustomerCompanyId = custComp.Id;
            target.ContractorCompanyId = contComp.Id;
            target.DirectorId = director.Id;

            await Context.Projects.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            var emplProjReqModel = TestDataGenerator.EmployeeProjectRequestModel();
            emplProjReqModel.ProjectId = target.Id;
            emplProjReqModel.WorkersIds.Add(empl1.Id);
            emplProjReqModel.WorkersIds.Add(empl2.Id);
            await projectService.LinkWorkersAsync(emplProjReqModel, CancellationToken);

            //Act
            Func<Task> act = () => projectService.UnlinkWorkersAsync(emplProjReqModel, CancellationToken);

            //Assert
            await act.Should().NotThrowAsync();
        }

        /// <summary>
        /// Удаление проекта работает
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Project();
            await Context.Projects.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => projectService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Projects.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

