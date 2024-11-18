using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sibers.Api.Attribute;
using Sibers.Api.Infrastructures.Validator;
using Sibers.Api.Models;
using Sibers.Api.ModelsRequest.Project;
using Sibers.Services.Contracts.Interfaces;
using Sibers.Services.Contracts.ModelsRequest;

namespace Sibers.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с проектами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Project")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ProjectController"/>
        /// </summary>
        public ProjectController(IProjectService projectService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.projectService = projectService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех проектов
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<ProjectResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await projectService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ProjectResponse>>(result));
        }

        /// <summary>
        /// Получает проект по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(ProjectResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await projectService.GetByIdAsync(id, cancellationToken);

            return Ok(mapper.Map<ProjectResponse>(item));
        }

        /// <summary>
        /// Создаёт новый проект
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(ProjectResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var projectRequestModel = mapper.Map<ProjectRequestModel>(request);
            var result = await projectService.AddAsync(projectRequestModel, cancellationToken);
            return Ok(mapper.Map<ProjectResponse>(result));
        }

        /// <summary>
        /// Редактирует существующий проект
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(ProjectResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(ProjectRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<ProjectRequestModel>(request);
            var result = await projectService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<ProjectResponse>(result));
        }

        /// <summary>
        /// Добавляет связи проекта с работниками
        /// </summary>
        [HttpPatch("link")]
        [ApiOk(typeof(ProjectResponse))]
        public async Task<IActionResult> LinkWorkers(EmployeeProjectRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<EmployeeProjectRequestModel>(request);
            await projectService.LinkWorkersAsync(model, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Удаляет связи проекта с работниками
        /// </summary>
        [HttpPatch("unlink")]
        [ApiOk(typeof(ProjectResponse))]
        public async Task<IActionResult> UnlinkWorkers(EmployeeProjectRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<EmployeeProjectRequestModel>(request);
            await projectService.UnlinkWorkersAsync(model, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Удаляет существующий проект по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await projectService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
