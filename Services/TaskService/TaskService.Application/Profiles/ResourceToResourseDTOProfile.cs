using AutoMapper;
using TaskService.Application.DTOs;
using TaskService.Domain.Entities;

namespace TaskService.Application.Profiles
{
    public class ResourceToResourceDTOProfile : Profile
    {
        public ResourceToResourceDTOProfile()
        {
            CreateMap<ResourceDTO, Resource>().ReverseMap();
        }
    }
}