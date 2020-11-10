using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos;
using FSight.API.Helpers;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
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
        public async Task<ActionResult<Pagination<TicketToReturnDto>>> GetTickets(
            [FromQuery] TicketSpecParams parameters)
        {
            var spec = new TicketsWithCommentsDevelopersAndCustomersSpecification(parameters);
            
            var countSpec = new TicketWithFiltersForCountSpecification(parameters);

            var totalItems = await _unitOfWork.Repository<Ticket>().CountAsync(countSpec);
            
            var tickets = await _unitOfWork.Repository<Ticket>().ListAsync(spec);
            
            var data = _mapper.Map<IReadOnlyList<Ticket>, IReadOnlyList<TicketToReturnDto>>(tickets);

            return Ok(new Pagination<TicketToReturnDto>(parameters.PageIndex, parameters.PageSize, totalItems, data));
        }
    }
}