using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Ticket;
using FSight.API.Errors;
using FSight.API.Mediation.Commands.TicketCommands;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Handlers.TicketHandlers
{
    public class PartiallyUpdateTicketCommandHandler : IRequestHandler<PartiallyUpdateTicketCommand, IActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartiallyUpdateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<IActionResult> Handle(PartiallyUpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var spec = new TicketsWithCommentsAndDevelopersSpecification(request.TicketId);

            var ticket = await _unitOfWork.Repository<Ticket>().GetEntityWithSpecification(spec);

            if (ticket == null)
            {
                return request.Controller.NotFound(new ApiResponse(404));
            }
            
            var ticketToPatch = _mapper.Map<TicketForUpdateDto>(ticket);

            request.PatchDocument.ApplyTo(ticketToPatch, request.Controller.ModelState);

            if (!request.Controller.TryValidateModel(ticketToPatch))
            {
                return request.Controller.ValidationProblem(request.Controller.ModelState);
            }
            
            var updatedTicket = _mapper.Map(ticketToPatch, ticket);

            try
            {
                _unitOfWork.Repository<Ticket>().Update(updatedTicket);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ApiException(500, ex.Message));
            }

            return request.Controller.NoContent();
        }
    }
}