using MediatR;
using UnionService.Domain.Interfaces;
using AutoMapper;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public class GetCompanyRoleHandler : IRequestHandler<GetCompanyRoleQuery, CompanyRole>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;

        public GetCompanyRoleHandler(IUnitOfWork unitOfWork, IAccessService accessService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<CompanyRole> Handle(GetCompanyRoleQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetAsync(td => td.Username == request.username);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with name {request.username} does not exist.");
            }

            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (await _accessService.HaveOwnerAccessAsync(existingCompany.Id, request.username!))
                return CompanyRole.owner;

            if (await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
                return CompanyRole.manager;

            return CompanyRole.performer;
        }
    }
}
