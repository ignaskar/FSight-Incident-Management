using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Ticket;
using FSight.Core.Entities;
using FSight.API.Mediation.Queries.TicketQueries;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using MediatR;

namespace FSight.API.Mediation.Handlers.TicketHandlers
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public GetTicketByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new TicketsWithCommentsAndDevelopersSpecification(request.TicketId);

            var ticket = await _unitOfWork.Repository<Ticket>().GetEntityWithSpecification(spec);

            return ticket == null ? null : _mapper.Map<Ticket, TicketDto>(ticket);
        }
    }
}