using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Sibers.Api.Models;
using Sibers.Api.Models.Enums;
using Sibers.Api.ModelsRequest.Employee;
using Sibers.Services.Contracts.Models;
using Sibers.Services.Contracts.Models.Enums;
using Sibers.Services.Contracts.ModelsRequest;
using Sibers.Api.ModelsRequest.Project;
using Sibers.Api.ModelsRequest.Company;
using Sibers.Context.Contracts.Models;

namespace Sibers.Api.Infrastructures
{
    /// <summary>
    /// Профиль маппера АПИшки
    /// </summary>
    public class ApiAutoMapperProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiAutoMapperProfile"/>
        /// </summary>
        public ApiAutoMapperProfile()
        {
            CreateMap<EmployeeTypesModel, EmployeeTypesResponse>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<ProjectModel, CreateProjectRequest>(MemberList.Destination).ReverseMap();
            CreateMap<ProjectModel, ProjectRequest>(MemberList.Destination).ReverseMap();
            CreateMap<CreateProjectRequest, ProjectRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, next => next.Ignore());
            CreateMap<ProjectRequest, ProjectRequestModel>(MemberList.Destination);
            CreateMap<ProjectModel, ProjectResponse>(MemberList.Destination)
                .ForMember(pr => pr.ContractorCompanyTitle,
                           opt => opt.MapFrom(pm => pm.ContractorCompany != null
                                                   ? pm.ContractorCompany.Title 
                                                   : string.Empty))
                .ForMember(pr => pr.CustomerCompanyTitle,
                           opt => opt.MapFrom(pm => pm.CustomerCompany != null
                                                   ? pm.CustomerCompany.Title
                                                   : string.Empty))
                .ForMember(pr => pr.Director,
                           opt => opt.MapFrom(pm => pm.Director != null
                                                   ? $"{pm.Director.LastName} {pm.Director.FirstName} {pm.Director.Patronymic ?? string.Empty}".Trim()
                                                   : string.Empty));

            CreateMap<EmployeeModel, CreateEmployeeRequest>(MemberList.Destination).ReverseMap();
            CreateMap<EmployeeModel, EmployeeRequest>(MemberList.Destination).ReverseMap();
            CreateMap<CreateEmployeeRequest, EmployeeRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, next => next.Ignore());
            CreateMap<EmployeeRequest, EmployeeRequestModel>(MemberList.Destination);
            CreateMap<EmployeeModel, EmployeeResponse>(MemberList.Destination);

            CreateMap<CompanyModel, CreateCompanyRequest>(MemberList.Destination).ReverseMap();
            CreateMap<CompanyModel, CompanyRequest>(MemberList.Destination).ReverseMap();
            CreateMap<CreateCompanyRequest, CompanyRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, next => next.Ignore());
            CreateMap<CompanyRequest, CompanyRequestModel>(MemberList.Destination);
            CreateMap<CompanyModel, CompanyResponse>(MemberList.Destination);
        }
    }

}
