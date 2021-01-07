using System;
using System.Threading.Tasks;
using FSight.API.Dtos.Ticket;
using FSight.API.Errors;
using FSight.API.Helpers;
using FSight.API.Mediation.Commands.TicketCommands;
using FSight.API.Mediation.Queries.TicketQueries;
using FSight.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<TicketDto>>> GetAllTickets(
            [FromQuery] TicketSpecParams parameters)
        {
            var query = new GetAllTicketsQuery(parameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{ticketId:int}", Name = "GetTicket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TicketDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketDto>> GetSingleTicket(int ticketId)
        {
            var query = new GetTicketByIdQuery(ticketId);
            var result = await _mediator.Send(query);
            return result == null
                ? NotFound(new ApiResponse(404, "Requested ticket was not found."))
                : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(TicketDto),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TicketDto>> CreateTicket(CreateTicketCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetTicket", new {ticketId = result.Id}, result);
        }

        [HttpPatch("{ticketId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TicketForUpdateDto), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> PartiallyUpdateTicket(int ticketId,
            JsonPatchDocument<TicketForUpdateDto> patchDocument)
        {
            var command = new PartiallyUpdateTicketCommand(ticketId, this, patchDocument);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketsOptions()
        {
            var query = new GetTicketsOptionsQuery(this);
            var result = await _mediator.Send(query);
            return result;
        }
    }
}