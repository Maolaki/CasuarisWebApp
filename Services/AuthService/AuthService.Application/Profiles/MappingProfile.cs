using AutoMapper;
using AuthService.Application.DTOs;
using AuthService.Domain.Entities;

namespace AuthService.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>()
                        .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password));
        }
    }
}