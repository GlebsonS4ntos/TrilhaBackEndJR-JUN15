using AutoMapper;
using Desafio.API.Models.Dtos;
using Desafio.API.Models.Entities;

namespace Desafio.API.Middlewares
{
    public class AutoMapperConfig : Profile
    { 
        public AutoMapperConfig() 
        { 
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
