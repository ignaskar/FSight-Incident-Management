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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ActionResult<UserDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public RegisterCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        
        public async Task<ActionResult<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _userManager.FindByEmailAsync(request.RegisterDto.Email);
            
            if (userEntity != null)
            {
                return new UnprocessableEntityObjectResult(new ApiValidationErrorResponse{ Errors = new []{"Email address is already in use."}});
            }
            
            var user = new AppUser
            {
                FirstName = request.RegisterDto.FirstName,
                LastName = request.RegisterDto.LastName,
                Email = request.RegisterDto.Email,
                UserName = request.RegisterDto.Email
            };

            var result = await _userManager.CreateAsync(user, request.RegisterDto.Password);

            if (!result.Succeeded)
            {
                return request.Controller.BadRequest(new ApiResponse(400));
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            };
        }
    }
}