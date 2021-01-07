using System;
using System.Threading.Tasks;
using FSight.API.Dtos.Identity;
using FSight.API.Mediation.Commands.AccountCommands;
using MediatR;
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
