using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using qwertygroup.Core.IServices;
using qwertygroup.DataAccess;
using qwertygroup.DataAccess.Entities;
using qwertygroup.DataAccess.Repositories;
using qwertygroup.Domain.Services;
using qwertygroup.Security;
using qwertygroup.Security.Entities;
using qwertygroup.Security.IAuthUserService;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.Models;
using qwertygroup.Security.Services;

namespace qwertygroup.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "qwertygroup.WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            
            services.AddAuthentication(authenticationOptions =>
                {
                    authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = 
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"])),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["JwtConfig:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["JwtConfig:Audience"],
                        ValidateLifetime = true
                    };
                });
            
            services.AddScoped<IBodyRepository, BodyRepository>();
            services.AddScoped<IBodyService, BodyService>();

            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<ISecurityService, SecurityService>();

            services.AddDbContext<AuthDbContext>(options => options.UseSqlite(_configuration.GetConnectionString("AuthConnection")));
            services.AddDbContext<PostContext>(options => options.UseSqlite("Data Source=main.Db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PostContext postContext,
            AuthDbContext authDbContext, ISecurityService securityService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "qwertygroup.WebApi v1"));
            }
            postContext.Database.EnsureDeleted();
            postContext.Database.EnsureCreated();
            postContext.bodies.Add(new BodyEntity
            {
                Id = 1,
                Text = "The difference between my darkness and your darkness is that I can look at my own badness in the face and accept its existence while you are busy covering your mirror with a white linen sheet. The difference between my sins and your sins is that when I sin I know I'm sinning while you have actually fallen prey to your own fabricated illusions. I am a siren, a mermaid; I know that I am beautiful while basking on the ocean's waves and I know that I can eat flesh and bones at the bottom of the sea. You are a white witch, a wizard; your spells are manipulations and your cauldron from hell yet you wrap yourself in white and wear a silver wig."
            });
            postContext.bodies.Add(new BodyEntity
            {
                Id = 2,
                Text = "One of the greatest regrets in life is being what others would want you to be, rather than being yourself."
            });
            postContext.bodies.Add(new BodyEntity
            {
                Id = 3,
                Text = "What's the whole point of being pretty on the outside when you’re so ugly on the inside?"
            });
            postContext.SaveChanges();
            
            new AuthDbSeeder(authDbContext, securityService).SeedDevelopment();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
