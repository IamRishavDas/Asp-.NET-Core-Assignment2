using Assignment2.Dtos;
using Assignment2.Models;
using AutoMapper;

namespace Assignment2.Helper
{
    public class ProfileMapper: Profile
    {
        public ProfileMapper()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}
