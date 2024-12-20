﻿using Microsoft.AspNetCore.Http;
using TaskService.Domain.Enums;

namespace TaskService.Application.DTOs
{
    public class ResourceDTO
    {
        public int Id { get; set; }
        public string? Data { get; set; }
        public IFormFile? ImageFile { get; set; }
        public ResourceType Type { get; set; }
    }
}