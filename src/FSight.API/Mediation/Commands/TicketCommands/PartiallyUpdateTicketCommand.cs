using FSight.API.Controllers;
using FSight.API.Dtos.Ticket;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Commands.TicketCommands
{
    public class PartiallyUpdateTicketCommand : IRequest<IActionResult>
    {
        public int TicketId { get; }
        public TicketsController Controller { get; }
        public JsonPatchDocument<TicketForUpdateDto> PatchDocument { get; }
        
        public PartiallyUpdateTicketCommand(int ticketId, TicketsController controller, JsonPatchDocument<TicketForUpdateDto> patchDocument)
        {
            TicketId = ticketId;
            Controller = controller;
            PatchDocument = patchDocument;
        }
    }
}