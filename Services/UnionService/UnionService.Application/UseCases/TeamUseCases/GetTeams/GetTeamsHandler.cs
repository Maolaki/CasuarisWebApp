using MediatR;
using AutoMapper;
using UnionService.Domain.Interfaces;
using UnionService.Application.DTOs;

namespace UnionService.Application.UseCases
{
    public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, IEnumerable<TeamDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;

        public GetTeamsQueryHandler(IUnitOfWork unitOfWork, IAccessService accessService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDTO>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            var existingCompany = await _unitOfWork.Companies.GetAsync(td => td.Id == request.companyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with Id {request.companyId} does not exist.");
            }

            var hasManagerAccess = await _accessService.HaveManagerAccessAsync(existingCompany.Id, request.username!);

            var teamsQuery = await _unitOfWork.Teams
                .GetAllAsync(t => t.CompanyId == request.companyId, 1, int.MaxValue);

            if (!hasManagerAccess)
            {
                teamsQuery = teamsQuery.Where(t => t.Members?.Any(m => m.Username == request.username) == true);
            }

            var teamDTOs = teamsQuery.Select(team => _mapper.Map<TeamDTO>(team)).ToList();

            teamDTOs = teamDTOs
                .Skip((int)((request.pageNumber! - 1) * request.pageSize!))
                .Take((int)request.pageSize!)
                .ToList();

            return teamDTOs;
        }
    }
}
