using AutoMapper;
using UnionService.Application.DTOs;
using UnionService.Domain.Entities;

namespace UnionService.Application.Profiles
{
    public class InvitationToInvitationDTOProfile : Profile
    {
        public InvitationToInvitationDTOProfile()
        {
            CreateMap<Invitation, InvitationDTO>().ReverseMap();
        }
    }
}
