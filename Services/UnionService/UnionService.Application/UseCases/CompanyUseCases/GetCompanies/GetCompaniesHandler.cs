using MediatR;
using UnionService.Application.DTOs;
using UnionService.Domain.Interfaces;
using System.Linq;
using AutoMapper;

namespace UnionService.Application.UseCases
{
    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, IEnumerable<CompanyDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;

        public GetCompaniesQueryHandler(IUnitOfWork unitOfWork, IAccessService accessService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDTO>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetAsync(td => td.Username == request.username);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with name {request.username} does not exist.");
            }

            var companies = await _unitOfWork.Companies.GetAllAsync(c =>
                c.Owners!.Any(o => o.Username == request.username) ||
                c.Managers!.Any(m => m.Username == request.username) ||
                c.Performers!.Any(p => p.User!.Username == request.username),
                1, int.MaxValue);

            var companyDTOs = companies.Select(company => _mapper.Map<CompanyDTO>(company)).ToList();

            companyDTOs = companyDTOs
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return companyDTOs;
        }
    }
}
