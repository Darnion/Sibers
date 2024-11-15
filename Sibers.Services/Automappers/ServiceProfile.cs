using AutoMapper;
using Sibers.Context.Contracts.Enums;
using Sibers.Context.Contracts.Models;
using Sibers.Services.Contracts.Models.Enums;
using Sibers.Services.Contracts.Models;
using AutoMapper.Extensions.EnumMapping;

namespace Sibers.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<EmployeeTypes, EmployeeTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<Employee, EmployeeModel>(MemberList.Destination)
                .ForMember(x => x.Projects, next => next.Ignore());

            CreateMap<Company, CompanyModel>(MemberList.Destination);

            CreateMap<Project, ProjectModel>(MemberList.Destination)
                .ForMember(x => x.ContractorCompany, next => next.Ignore())
                .ForMember(x => x.CustomerCompany, next => next.Ignore())
                .ForMember(x => x.Workers, next => next.Ignore())
                .ForMember(x => x.Director, next => next.Ignore());
        }
    }
}
