using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FSight.API.Dtos;
using FSight.API.Dtos.Identity;
using FSight.API.Errors;
using FSight.API.Mediation.Commands.AccountCommands;
using FSight.Core.Entities.Identity;
using FSight.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var command = new RegisterCommand(registerDto, this);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var command = new LoginCommand(loginDto, this);
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
