using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos;
using FSight.API.Helpers;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Ticket> _ticketRepo;

        public TicketsController(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Ticket> ticketRepo)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _ticketRepo = ticketRepo;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<TicketDto>>> GetTickets(
            [FromQuery] TicketSpecParams parameters)
        {
            var spec = new TicketsWithCommentsDevelopersAndCustomersSpecification(parameters);
            
            var countSpec = new TicketWithFiltersForCountSpecification(parameters);

            var totalItems = await _unitOfWork.Repository<Ticket>().CountAsync(countSpec);
            
            var tickets = await _unitOfWork.Repository<Ticket>().ListAsync(spec);
            
            var data = _mapper.Map<IReadOnlyList<Ticket>, IReadOnlyList<TicketDto>>(tickets);

            return Ok(new Pagination<TicketDto>(parameters.PageIndex, parameters.PageSize, totalItems, data));
        }

        [HttpGet("{ticketId}", Name = "GetTicket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TicketDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketDto>> GetTicket(int ticketId)
        {
            var spec = new TicketsWithCommentsDevelopersAndCustomersSpecification(ticketId);

            var ticket = await _unitOfWork.Repository<Ticket>().GetEntityWithSpecification(spec);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Ticket, TicketDto>(ticket));
        }

        [HttpPost]
        public async Task<ActionResult<TicketDto>> CreateTicket(TicketForCreationDto ticket)
        {
            var ticketEntity = _mapper.Map<Ticket>(ticket);
            _unitOfWork.Repository<Ticket>().Add(ticketEntity);

            await _unitOfWork.Complete();

            var ticketToReturn = _mapper.Map<TicketDto>(ticketEntity);
            return CreatedAtRoute("GetTicket", new {ticketId = ticketToReturn.Id}, ticketToReturn);
        }
    }
}