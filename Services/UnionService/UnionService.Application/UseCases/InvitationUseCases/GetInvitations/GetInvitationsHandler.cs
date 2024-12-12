using AutoMapper;
using MediatR;
using UnionService.Application.DTOs;
using UnionService.Domain.Interfaces;

namespace UnionService.Application.UseCases
{
    public class GetInvitationsHandler : IRequestHandler<GetInvitationsQuery, IEnumerable<InvitationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;

        public GetInvitationsHandler(IUnitOfWork unitOfWork, IAccessService accessService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvitationDTO>> Handle(GetInvitationsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetAsync(td => td.Username == request.username);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with name {request.username} does not exist.");
            }

            var invitations = await _unitOfWork.Invitations.GetAllAsync(i => i.UserId == existingUser.Id, 1, int.MaxValue);

            var invitationDTOs = _mapper.Map<IEnumerable<InvitationDTO>>(invitations);

            invitationDTOs = invitationDTOs
                .Skip((int)((request.pageNumber! - 1) * request.pageSize!))
                .Take((int)request.pageSize!)
                .ToList();

            return invitationDTOs;
        }
    }
}
