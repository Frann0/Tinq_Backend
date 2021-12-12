using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using qwertygroup.WebApi.PolicyHandlers;

namespace qwertygroup.WebApi.Extensions
{
    public static class AuthServiceExtensions
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(authenticationOptions =>
                {
                    authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(config["JwtConfig:Secret"])),
                        ValidateIssuer = true,
                        ValidIssuer = config["JwtConfig:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = config["JwtConfig:Audience"],
                        ValidateLifetime = true
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(RegisteredUserHandler),
                    policy => policy.Requirements.Add(new RegisteredUserHandler()));
                options.AddPolicy(nameof(AdminUserHandler),
                    policy => policy.Requirements.Add(new AdminUserHandler()));
            });
            
            return services;
        }
    }
}