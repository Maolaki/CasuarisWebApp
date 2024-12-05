using AutoMapper;
using Microsoft.AspNetCore.Http;
using TaskService.Application.DTOs;
using TaskService.Domain.Entities;

namespace TaskService.Application.Profiles
{
    public class ResourceToResourceDTOProfile : Profile
    {
        public ResourceToResourceDTOProfile()
        {
            CreateMap<Resource, ResourceDTO>()
                .ForMember(dest => dest.ImageFile, opt => opt.MapFrom(src => ConvertToFormFile(src.DataBytes, src.ContentType)));
        }

        private static IFormFile? ConvertToFormFile(byte[]? logoData, string? logoContentType)
        {
            if (logoData == null || string.IsNullOrWhiteSpace(logoContentType))
            {
                return null;
            }

            var fileName = $"logo.{logoContentType.Split('/').Last()}";

            var memoryStream = new MemoryStream(logoData);

            return new FormFile(memoryStream, 0, logoData.Length, "Logo", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = logoContentType
            };
        }
    }
}