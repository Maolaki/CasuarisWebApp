﻿using AuthService.Application.Interfaces;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.UseCases
{
    public class RefreshTokensHandler : IRequestHandler<RefreshTokensCommand, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokensHandler(ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.accessToken!);
            var username = principal?.Identity?.Name;

            if (username == null || principal == null)
                throw new ArgumentException("Wrong jwt or/and username");

            var refToken = await _unitOfWork.RefreshTokens.GetAsync(t => t.Token == request.refreshToken && t.User!.Username == username);
            if (refToken == null || refToken.RefreshTokenExpiryTime <= DateTime.Now)
                throw new ArgumentException("Wrong refreshToken");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);

            refToken.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
            await _unitOfWork.SaveAsync();

            return newAccessToken;
        }
    }
}
