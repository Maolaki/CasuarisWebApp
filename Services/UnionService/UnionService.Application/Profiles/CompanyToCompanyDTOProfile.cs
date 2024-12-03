using AutoMapper;
using UnionService.Application.DTOs;
using UnionService.Domain.Entities;

namespace UnionService.Application.Profiles
{
    public class CompanyToCompanyDTOProfile : Profile
    {
        public CompanyToCompanyDTOProfile() 
        {
            CreateMap<Company, CompanyDTO>().ReverseMap();
        }
    }
}
