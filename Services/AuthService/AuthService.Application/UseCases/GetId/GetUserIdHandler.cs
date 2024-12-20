﻿using AuthService.Application.Exceptions;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.UseCases
{
    public class GetUserIdHandler : IRequestHandler<GetUserIdQuery, long>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(GetUserIdQuery request, CancellationToken cancellationToken)
        {
            var username = request.claimsPrincipalIdentity.Identity?.Name;
            if (username == null)
            {
                throw new NotFoundException("Wrong username");
            }

            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Username == username);
            if (existingUser == null)
            {
                throw new NotFoundException("Wrong username");
            }

            return existingUser.Id;
        }
    }
}
