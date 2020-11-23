using System.ComponentModel.DataAnnotations;
using FSight.API.Dtos;
using FSight.API.Dtos.Ticket;

namespace FSight.API.ValidationAttributes
{
    public class TicketTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var ticket = (TicketForManipulationDto) validationContext.ObjectInstance;

            if (ticket.Title == ticket.Description)
            {
                return new ValidationResult(ErrorMessage,
                    new []{nameof(TicketForManipulationDto)});
            }
            
            return ValidationResult.Success;
        }
    }
}