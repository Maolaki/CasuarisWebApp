using AutoMapper;
using UnionService.Application.DTOs;
using UnionService.Domain.Entities;
using UnionService.Domain.Enums;

namespace UnionService.Application.Profiles
{
    public class UserToCompanyMemberDTOProfile : Profile
    {
        public UserToCompanyMemberDTOProfile()
        {
            CreateMap<User, CompanyMemberDTO>()
                .ForMember(dest => dest.CompanyRole, opt => opt.MapFrom(src => CompanyRole.owner));

            CreateMap<PerformerInCompany, CompanyMemberDTO>()
                .ForMember(dest => dest.CompanyRole, opt => opt.MapFrom(src => CompanyRole.performer))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User!.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User!.Email));
        }
    }
}