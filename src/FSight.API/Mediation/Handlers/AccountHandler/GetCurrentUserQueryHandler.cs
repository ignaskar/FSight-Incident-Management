using System;
using System.Threading;
using System.Threading.Tasks;
using FSight.API.Dtos.Identity;
using FSight.API.Extensions;
using FSight.API.Mediation.Queries.AccountQueries;
using FSight.Core.Entities.Identity;
using FSight.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FSight.API.Mediation.Handlers.AccountHandler
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserAccessor _accessor;

        public GetCurrentUserQueryHandler(UserManager<AppUser> userManager, ITokenService tokenService, IUserAccessor accessor)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }
        
        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(_accessor.User);
            
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