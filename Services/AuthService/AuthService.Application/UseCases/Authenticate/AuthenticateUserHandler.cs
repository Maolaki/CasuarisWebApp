﻿using AuthService.Domain.Interfaces;
using AuthService.Application.DTOs;
using AuthService.Domain.Entities;
using MediatR;
using AuthService.Application.Interfaces;

namespace AuthService.Application.UseCases
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserQuery, AuthenticatedDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticateUserHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedDTO> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Username == request.login || u.Email == request.login);

            if (user == null || !_passwordHasher.VerifyPassword(request.password!, user.HashedPassword!))
            {
                throw new ArgumentException("Wrong login or/and password");
            }

            var claims = _tokenService.GenerateClaims(user);

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _unitOfWork.RefreshTokens.Create(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(1)
            });

            await _unitOfWork.SaveAsync();

            return new AuthenticatedDTO
            {
                Username = user.Username,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
