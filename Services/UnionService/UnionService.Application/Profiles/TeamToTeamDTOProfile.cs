using AutoMapper;
using UnionService.Application.DTOs;
using UnionService.Domain.Entities;

namespace UnionService.Application.Profiles
{
    public class TeamToTeamDTOProfile : Profile
    {
        public TeamToTeamDTOProfile()
        {
            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members!.Select(m => m.Username).ToList()));
        }
    }
}
