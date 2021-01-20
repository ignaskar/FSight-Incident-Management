using FSight.API.Controllers;
using FSight.API.Dtos.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Queries.AccountQueries
{
    public class GetCurrentUserQuery : IRequest<ActionResult<UserDto>>
    {
        public AccountController Controller { get; }

        public GetCurrentUserQuery(AccountController controller)
        {
            Controller = controller;
        }
    }
}