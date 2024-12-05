using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.UseCases
{
    public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RevokeTokenHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var username = request.user!.Identity?.Name!;

            var refToken = await _unitOfWork.RefreshTokens.GetAsync(t => t.Token == request.refreshToken && t.User!.Username == username);

            if (refToken == null)
                throw new ArgumentException("Wrong refreshToken or/and username");

            _unitOfWork.RefreshTokens.Delete(refToken);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
