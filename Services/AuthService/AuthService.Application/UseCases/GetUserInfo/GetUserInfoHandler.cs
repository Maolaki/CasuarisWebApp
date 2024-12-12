using AuthService.Application.DTOs;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.UseCases
{
    public class GetUserInfoHandler : IRequestHandler<GetUserInfoQuery, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Username == request.username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username {request.username} does not exist.");
            }

            var userDto = _mapper.Map<UserDTO>(user);

            return userDto;
        }
    }
}
