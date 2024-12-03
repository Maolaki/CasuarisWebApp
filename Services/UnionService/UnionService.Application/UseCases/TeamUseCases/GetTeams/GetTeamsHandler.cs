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
            var hasManagerAccess = await _accessService.CheckManagerAccessAsync(request.CompanyId, request.username);

            var teamsQuery = await _unitOfWork.Teams
                .GetAllAsync(t => t.CompanyId == request.CompanyId, 1, int.MaxValue);

            if (!hasManagerAccess)
            {
                teamsQuery = teamsQuery.Where(t => t.Members?.Any(m => m.Username == request.username) == true);
            }

            var teamDTOs = teamsQuery.Select(team => _mapper.Map<TeamDTO>(team)).ToList();

            teamDTOs = teamDTOs
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return teamDTOs;
        }
    }
}
