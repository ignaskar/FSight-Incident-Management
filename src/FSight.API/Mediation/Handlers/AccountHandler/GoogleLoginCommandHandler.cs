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
using Microsoft.Extensions.Configuration;

namespace FSight.API.Mediation.Handlers.AccountHandler
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, ActionResult<UserDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public GoogleLoginCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IConfiguration config)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        
        public async Task<ActionResult<UserDto>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var payload = await Google.Apis.Auth.GoogleJsonWebSignature.ValidateAsync(request.GoogleUserDto.IdToken, new Google.Apis.Auth.GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] {_config["Authentication:Google:ClientId"]}
            });

            var user = await _userManager.FindByLoginAsync("google", payload.Subject);

            if (user != null)
            {
                return request.Controller.Ok(new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmployeeNumber = user.EmployeeNumber,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user)
                });
            }

            user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                user = new AppUser
                {
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    Email = payload.Email,
                    UserName = payload.Email
                };

                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            var info = new UserLoginInfo("google", payload.Subject, payload.Subject.ToUpperInvariant());
            var result = await _userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
            {
                return request.Controller.Ok(new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmployeeNumber = user.EmployeeNumber,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user)
                });
            }

            return request.Controller.UnprocessableEntity(new ApiResponse(422, "Unable to process GMail account. Please try to login again."));
        }
    }
}