using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;
using System.Data;

namespace AuthService.Application.UseCases
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Email == request.email || u.Username == request.username);
            if (existingUser != null)
            {
                throw new DuplicateNameException("Пользователь с таким email или username уже существует.");
            }

            var newUser = new User
            {
                Username = request.username,
                HashedPassword = _passwordHasher.HashPassword(request.password!),
                Email = request.email,
            };

            _unitOfWork.Users.Create(newUser);
            await _unitOfWork.SaveAsync(); 

            return Unit.Value;
        }
    }
}
