﻿using AuthService.Domain.Entities;
using System.Security.Claims;

namespace AuthService.Application.Interfaces
{
    public interface ITokenService
    {
        List<Claim> GenerateClaims(User user);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}