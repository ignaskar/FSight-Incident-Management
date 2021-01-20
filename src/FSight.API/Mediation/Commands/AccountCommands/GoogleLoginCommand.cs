using FSight.API.Controllers;
using FSight.API.Dtos.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Commands.AccountCommands
{
    public class GoogleLoginCommand : IRequest<ActionResult<UserDto>>
    {
        public GoogleUserDto GoogleUserDto { get; }
        public AccountController Controller { get; }

        public GoogleLoginCommand(GoogleUserDto googleUserDto, AccountController controller)
        {
            GoogleUserDto = googleUserDto;
            Controller = controller;
        }
    }
}