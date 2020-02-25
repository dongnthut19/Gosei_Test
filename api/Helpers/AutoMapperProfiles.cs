using api.Models;
using api.Models.Dtos;
using AutoMapper;

namespace api.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Qualification, QualificationDto>();
            CreateMap<EmployeeQualification, EmployeeQualificationDto>();
        }
    }
}