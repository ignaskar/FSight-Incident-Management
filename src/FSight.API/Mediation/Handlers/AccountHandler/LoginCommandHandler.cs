using System;
using System.Threading;
using System.Threading.Tasks;
using FSight.API.Dtos.Identity;
using FSight.API.Errors;
using FSight.API.Mediation.Commands.AccountCommands;
using FSight.Core.Entities.Identity;
using FSight.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Handlers.AccountHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ActionResult<UserDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        
        public async Task<ActionResult<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);

            if (user == null)
            {
                return request.Controller.Unauthorized(new ApiResponse(401, "Login failed. Either an account with this email does not exist or password was incorrect."));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, true);

            if (!result.Succeeded)
            {
                return request.Controller.Unauthorized(new ApiResponse(401, "Login failed. Either an account with this email does not exist or password was incorrect."));
            }

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmployeeNumber = user.EmployeeNumber,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            };
        }
    }
}