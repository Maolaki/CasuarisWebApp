using AutoMapper;
using AuthService.Application.DTOs;
using AuthService.Domain.Entities;

namespace AuthService.Application.Profiles
{
    public class UserToUserDTOProfile : Profile
    {
        public UserToUserDTOProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}