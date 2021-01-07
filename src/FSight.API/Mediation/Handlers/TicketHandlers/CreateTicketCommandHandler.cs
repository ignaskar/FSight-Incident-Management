using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Ticket;
using FSight.API.Exceptions;
using FSight.API.Mediation.Commands.TicketCommands;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using MediatR;


namespace FSight.API.Mediation.Handlers.TicketHandlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _accessor;

        public CreateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor accessor)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }
        
        public async Task<TicketDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var userId = _accessor.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new HttpAccessorNullUserException("An error occured while obtaining user info. Please try again.");
            }
            
            request.CreatedBy = Guid.Parse(userId);
            request.UpdatedBy = Guid.Parse(userId);

            var ticketEntity = _mapper.Map<Ticket>(request);
            
            try
            {
                _unitOfWork.Repository<Ticket>().Add(ticketEntity);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return _mapper.Map<TicketDto>(ticketEntity);
        }
    }
}