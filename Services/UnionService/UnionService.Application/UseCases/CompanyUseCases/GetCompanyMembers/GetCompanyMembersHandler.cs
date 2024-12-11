using AutoMapper;
using MediatR;
using UnionService.Application.DTOs;
using UnionService.Domain.Enums;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class GetCompanyMembersHandler : IRequestHandler<GetCompanyMembersQuery, IEnumerable<CompanyMemberDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;

        public GetCompanyMembersHandler(IUnitOfWork unitOfWork, IAccessService accessService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyMemberDTO>> Handle(GetCompanyMembersQuery request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(c => c.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            if (await _accessService.HaveOwnerAccessAsync(existingCompany.Id, request.username!))
            {
                var ownersAndManagers = existingCompany.Owners!
                    .Select(owner => {
                        var dto = _mapper.Map<CompanyMemberDTO>(owner);
                        dto.CompanyRole = CompanyRole.owner;
                        return dto;
                    })
                    .Concat(existingCompany.Managers!.Select(manager => {
                        var dto = _mapper.Map<CompanyMemberDTO>(manager);
                        dto.CompanyRole = CompanyRole.manager;
                        return dto;
                    }))
                    .ToList();

                var performers = existingCompany.Performers!
                    .Select(performer => _mapper.Map<CompanyMemberDTO>(performer))
                    .ToList();

                return ownersAndManagers.Concat(performers);
            }
            else if (await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!))
            {
                return existingCompany.Performers!
                    .Select(performer => _mapper.Map<CompanyMemberDTO>(performer))
                    .ToList();
            }

            throw new ArgumentException("User have no permission");
        }
    }
}
