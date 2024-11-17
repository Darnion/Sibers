using AutoMapper;
using Sibers.Api.Attribute;
using Sibers.Api.Infrastructures.Validator;
using Sibers.Api.Models;
using Sibers.Api.ModelsRequest.Employee;
using Sibers.Services.Contracts.Interfaces;
using Sibers.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;

namespace Sibers.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с сотрудниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeController"/>
        /// </summary>
        public EmployeeController(IEmployeeService employeeService,
            IMapper mapper,
            IApiValidatorService validatorService)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех работников
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<EmployeeResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await employeeService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<EmployeeResponse>>(result));
        }

        /// <summary>
        /// Получает работника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(EmployeeResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeService.GetByIdAsync(id, cancellationToken);

            return Ok(mapper.Map<EmployeeResponse>(item));
        }

        /// <summary>
        /// Создаёт нового работника
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(EmployeeResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var employeeRequestModel = mapper.Map<EmployeeRequestModel>(request);
            var result = await employeeService.AddAsync(employeeRequestModel, cancellationToken);
            return Ok(mapper.Map<EmployeeResponse>(result));
        }

        /// <summary>
        /// Редактирует существующего работника
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(EmployeeResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(EmployeeRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<EmployeeRequestModel>(request);
            var result = await employeeService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<EmployeeResponse>(result));
        }

        /// <summary>
        /// Удаляет существующего работника по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await employeeService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
