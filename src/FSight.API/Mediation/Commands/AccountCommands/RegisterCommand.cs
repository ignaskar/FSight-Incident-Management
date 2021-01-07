using FSight.API.Controllers;
using FSight.API.Dtos.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Commands.AccountCommands
{
    public class RegisterCommand : IRequest<ActionResult<UserDto>>
    {
        public RegisterDto RegisterDto { get; }
        public AccountController Controller { get; }

        public RegisterCommand(RegisterDto registerDto, AccountController controller)
        {
            RegisterDto = registerDto;
            Controller = controller;
        }
    }
}