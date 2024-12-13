using MediatR;
using UnionService.Application.DTOs;
using UnionService.Domain.Interfaces;
using AutoMapper;

namespace UnionService.Application.UseCases
{
    public class GetCompanyHandler : IRequestHandler<GetCompanyQuery, CompanyDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;

        public GetCompanyHandler(IUnitOfWork unitOfWork, IAccessService accessService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accessService = accessService;
            _mapper = mapper;
        }

        public async Task<CompanyDTO> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetAsync(td => td.Username == request.username);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with name {request.username} does not exist.");
            }

            var company = await _unitOfWork.Companies.GetAsync(c => c.Id == request.companyId);

            var companyDTO = _mapper.Map<CompanyDTO>(company);

            return companyDTO;
        }
    }
}
