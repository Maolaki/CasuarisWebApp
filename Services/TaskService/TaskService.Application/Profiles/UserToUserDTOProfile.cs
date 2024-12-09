using AutoMapper;
using TaskService.Application.DTOs;
using TaskService.Domain.Entities;

namespace TaskService.Application.Profiles
{
    public class UserToUserDTOProfile : Profile
    {
        public UserToUserDTOProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
