using AutoMapper;
using EmployeeAPI.Models;
using EmployeeAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.MappingProfile
{
    public class DtoMappingProfile:Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<GetAllEmployeesDTO, Employee>().ReverseMap();
            CreateMap<UpdateEmployeesDTO, Employee>().ReverseMap();
            CreateMap<SaveEmployeesDTO, Employee>().ReverseMap();
            CreateMap<GetAllDepartmentsDTO, Department>().ReverseMap();
        }
    }
}
