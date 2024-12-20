﻿using MediatR;
using UnionService.Domain.Interfaces;
using UnionService.Domain.Entities;

namespace UnionService.Application.UseCases
{
    public class AddTeamHandler : IRequestHandler<AddTeamCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public AddTeamHandler(IUnitOfWork unitOfWork, IAccessService accessService)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<Unit> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (!await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                throw new ArgumentException("User have no permission");

            var team = new Team
            {
                Name = request.name,
                Description = request.description,
                CompanyId = (int)request.companyId!
            };

            _unitOfWork.Teams.Create(team);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
