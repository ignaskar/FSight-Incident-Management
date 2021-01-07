using FSight.API.Controllers;
using FSight.API.Dtos.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Commands.AccountCommands
{
    public class LoginCommand : IRequest<ActionResult<UserDto>>
    {
        public LoginDto LoginDto { get; }
        public AccountController Controller { get; }

        public LoginCommand(LoginDto loginDto, AccountController controller)
        {
            LoginDto = loginDto;
            Controller = controller;
        }
    }
}