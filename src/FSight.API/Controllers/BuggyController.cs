using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSight.Core.Entities.Identity;
using FSight.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BuggyController : ControllerBase
    {
        private readonly FSightContext _context;
        private readonly RoleManager<AppRole> _roleManager;

        public BuggyController(FSightContext context, RoleManager<AppRole> roleManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _roleManager = roleManager;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "Secret API Key";
        }
        
        [HttpGet("testauthwithroles")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<string> GetSecretTextAsAdmin()
        {
            return "Congratulations! You are an admin!";
        }

        [HttpGet("claims")]
        [Authorize]
        public IEnumerable<object> GetClaims()
        {
            return User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            });
        }
    }
}