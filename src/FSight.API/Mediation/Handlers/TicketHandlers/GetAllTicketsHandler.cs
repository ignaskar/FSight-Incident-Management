using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Ticket;
using FSight.API.Helpers;
using FSight.API.Mediation.Queries.TicketQueries;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using MediatR;

namespace FSight.API.Mediation.Handlers.TicketHandlers
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, Pagination<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public GetAllTicketsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<Pagination<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var spec = new TicketsWithCommentsAndDevelopersSpecification(request.TicketParameters);
            
            var countSpec = new TicketWithFiltersForCountSpecification(request.TicketParameters);
            
            var tickets = await _unitOfWork.Repository<Ticket>().ListAsync(spec);

            var totalItems = await _unitOfWork.Repository<Ticket>().CountAsync(countSpec);
            
            var data = _mapper.Map<IReadOnlyList<Ticket>, IReadOnlyList<TicketDto>>(tickets);

            return new Pagination<TicketDto>(request.TicketParameters.PageIndex, request.TicketParameters.PageSize, totalItems, data);
        }
    }
}