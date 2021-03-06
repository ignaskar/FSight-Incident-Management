using System.Text;
using FSight.Core.Entities.Identity;
using FSight.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FSight.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            var builder = services.AddIdentityCore<AppUser>();
            
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRoles<AppRole>();
            builder.AddEntityFrameworkStores<FSightContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        // CHANGE WHEN CLIENT IS DEVELOPED
                        ValidateAudience = false
                    };
                })
                .AddGoogle(o =>
                {
                    o.ClientId = config["Authentication:Google:ClientId"];
                    o.ClientSecret = config["Authentication:Google:ClientSecret"];
                });

            return services;
        }
    }
}