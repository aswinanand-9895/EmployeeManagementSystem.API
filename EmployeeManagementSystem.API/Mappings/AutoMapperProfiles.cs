using AutoMapper;
using EmployeeManagementSystem.API.Models.Domain;
using EmployeeManagementSystem.API.Models.DTO;

namespace EmployeeManagementSystem.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee,EmployeeDto>().ReverseMap();
        }
    }
}
