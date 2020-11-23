using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos;
using FSight.API.Dtos.Ticket;
using FSight.API.Errors;
using FSight.API.Helpers;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<TicketDto>>> GetTickets(
            [FromQuery] TicketSpecParams parameters)
        {
            var spec = new TicketsWithCommentsAndDevelopersSpecification(parameters);
            
            var countSpec = new TicketWithFiltersForCountSpecification(parameters);
            
            var tickets = await _unitOfWork.Repository<Ticket>().ListAsync(spec);

            var totalItems = await _unitOfWork.Repository<Ticket>().CountAsync(countSpec);
            
            var data = _mapper.Map<IReadOnlyList<Ticket>, IReadOnlyList<TicketDto>>(tickets);

            return Ok(new Pagination<TicketDto>(parameters.PageIndex, parameters.PageSize, totalItems, data));
        }

        [HttpGet("{ticketId}", Name = "GetTicket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TicketDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketDto>> GetTicket(int ticketId)
        {
            var spec = new TicketsWithCommentsAndDevelopersSpecification(ticketId);

            var ticket = await _unitOfWork.Repository<Ticket>().GetEntityWithSpecification(spec);

            if (ticket == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<Ticket, TicketDto>(ticket));
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(TicketDto),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TicketDto>> CreateTicket(TicketForCreationDto ticket)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ApiResponse(400, $"Unknown error while finding user. Please try again."));
            }
            
            ticket.CreatedBy = Guid.Parse(userId);
            ticket.UpdatedBy = Guid.Parse(userId);

            var ticketEntity = _mapper.Map<Ticket>(ticket);
             _unitOfWork.Repository<Ticket>().Add(ticketEntity);
            
             await _unitOfWork.Complete();
            
             var ticketToReturn = _mapper.Map<TicketDto>(ticketEntity);
             return CreatedAtRoute("GetTicket", new {ticketId = ticketToReturn.Id}, ticketToReturn);
        }

        [HttpPatch("{ticketId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TicketForUpdateDto), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> PartiallyUpdateTicket(int ticketId,
            JsonPatchDocument<TicketForUpdateDto> patchDocument)
        {
            var spec = new TicketsWithCommentsAndDevelopersSpecification(ticketId);

            var ticket = await _unitOfWork.Repository<Ticket>().GetEntityWithSpecification(spec);

            if (ticket == null)
            {
                _unitOfWork.Dispose();
                return NotFound(new ApiResponse(404));
            }

            var ticketToPatch = _mapper.Map<TicketForUpdateDto>(ticket);
            
            
            patchDocument.ApplyTo(ticketToPatch, ModelState);

            if (!TryValidateModel(ticketToPatch))
            {
                return ValidationProblem(ModelState);
            }

            var updatedTicket = _mapper.Map(ticketToPatch, ticket);
            
            _unitOfWork.Repository<Ticket>().Update(updatedTicket);
            await _unitOfWork.Complete();

            return NoContent();
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTicketsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PATCH,OPTIONS");
            return Ok(new ApiResponse(200));
        }
    }
}