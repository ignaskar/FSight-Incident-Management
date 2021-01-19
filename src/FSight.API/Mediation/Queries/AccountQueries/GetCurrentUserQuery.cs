using FSight.API.Controllers;
using FSight.API.Dtos.Identity;
using MediatR;

namespace FSight.API.Mediation.Queries.AccountQueries
{
    public class GetCurrentUserQuery : IRequest<UserDto>
    {
        public AccountController Controller { get; }

        public GetCurrentUserQuery(AccountController controller)
        {
            Controller = controller;
        }
    }
}