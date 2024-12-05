using AutoMapper;
using Microsoft.AspNetCore.Http;
using UnionService.Application.DTOs;
using UnionService.Domain.Entities;

namespace UnionService.Application.Profiles
{
    public class CompanyToCompanyDTOProfile : Profile
    {
        public CompanyToCompanyDTOProfile()
        {
            CreateMap<Company, CompanyDTO>()
                .ForMember(dest => dest.LogoFile, opt => opt.MapFrom(src => ConvertToFormFile(src.LogoData, src.LogoContentType)));
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
