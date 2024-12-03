using AuthService.Application.Exceptions;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.UseCases
{
    public class RevokeAllTokensHandler : IRequestHandler<RevokeAllTokensCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RevokeAllTokensHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RevokeAllTokensCommand request, CancellationToken cancellationToken)
        {
            var username = request.User.Identity?.Name!;
            if (username == null)
            {
                throw new NotFoundException("Wrong username");
            }

            var refreshTokens = await _unitOfWork.RefreshTokens.GetAllAsync(t => t.User != null && t.User.Username == username);

            if (!refreshTokens.Any())
                throw new ArgumentException("No tokens found for the user.");

            foreach (var refreshToken in refreshTokens)
            {
                _unitOfWork.RefreshTokens.Delete(refreshToken);
            }

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
