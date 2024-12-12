using AuthService.Domain.Interfaces;
using MediatR;
using System.Data;

namespace AuthService.Application.UseCases
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Username == request.username);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with username {request.username} does not exist.");
            }

            if (!string.IsNullOrEmpty(request.newUsername))
            {
                var usernameExists = await _unitOfWork.Users.GetAsync(u => u.Username == request.newUsername);
                if (usernameExists != null)
                {
                    throw new DuplicateNameException($"The username {request.newUsername} is already taken.");
                }
                existingUser.Username = request.newUsername;
            }

            if (!string.IsNullOrEmpty(request.newEmail))
            {
                var emailExists = await _unitOfWork.Users.GetAsync(u => u.Email == request.newEmail);
                if (emailExists != null)
                {
                    throw new DuplicateNameException($"The email {request.newEmail} is already taken.");
                }
                existingUser.Email = request.newEmail;
            }

            _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
