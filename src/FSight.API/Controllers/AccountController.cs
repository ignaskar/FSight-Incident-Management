using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FSight.API.Dtos;
using FSight.API.Errors;
using FSight.Core.Entities.Identity;
using FSight.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?
                .Value;

            var user = await _userManager.FindByEmailAsync(email);
            
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new UnprocessableEntityObjectResult(new ApiValidationErrorResponse{ Errors = new []{"Email address is already in use."}});
            }
            
            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return UnprocessableEntity();
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

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401, "Login failed. Either an account with this email does not exist or password was incorrect."));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);

            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401, "Login failed. Either an account with this email does not exist or password was incorrect."));
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