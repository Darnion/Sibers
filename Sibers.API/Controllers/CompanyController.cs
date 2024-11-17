using AutoMapper;
using Sibers.Api.Attribute;
using Sibers.Api.Infrastructures.Validator;
using Sibers.Api.Models;
using Sibers.Api.ModelsRequest.Company;
using Sibers.Services.Contracts.Interfaces;
using Sibers.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;

namespace Sibers.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с компаниями
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyController"/>
        /// </summary>
        public CompanyController(ICompanyService companyService,
            IMapper mapper,
        IApiValidatorService validatorService)
        {
            this.companyService = companyService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список всех компаний
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<CompanyResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await companyService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CompanyResponse>>(result));
        }

        /// <summary>
        /// Получает компанию по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(CompanyResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await companyService.GetByIdAsync(id, cancellationToken);

            return Ok(mapper.Map<CompanyResponse>(item));
        }

        /// <summary>
        /// Создаёт новую компанию
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(CompanyResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var companyRequestModel = mapper.Map<CompanyRequestModel>(request);
            var result = await companyService.AddAsync(companyRequestModel, cancellationToken);
            return Ok(mapper.Map<CompanyResponse>(result));
        }

        /// <summary>
        /// Редактирует существующую компанию
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(CompanyResponse))]
        [ApiConflict]
        public async Task<IActionResult> Edit(CompanyRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<CompanyRequestModel>(request);
            var result = await companyService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<CompanyResponse>(result));
        }

        /// <summary>
        /// Удаляет существующую компанию по id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await companyService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
