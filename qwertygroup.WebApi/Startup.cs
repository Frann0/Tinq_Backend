using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using qwertygroup.Core.IServices;
using qwertygroup.DataAccess;
using qwertygroup.DataAccess.Entities;
using qwertygroup.DataAccess.Repositories;
using qwertygroup.Domain.IRepositories;
using qwertygroup.Domain.Services;
using qwertygroup.Security;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.IServices;
using qwertygroup.Security.Repositories;
using qwertygroup.Security.Services;
using qwertygroup.WebApi.Middleware;
using qwertygroup.WebApi.PolicyHandlers;

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
            var loggerFactory = LoggerFactory.Create(builder =>
                        {
                            builder.AddConsole();
                        }
                        );
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
                    options.SaveToken = true;
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(RegisteredUserHandler),
                    policy => policy.Requirements.Add(new RegisteredUserHandler()));
                options.AddPolicy(nameof(AdminUserHandler),
                    policy => policy.Requirements.Add(new AdminUserHandler()));
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy("Dev-cors", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                options.AddPolicy("Prod-cors", policy =>
                {
                    policy
                        .WithOrigins(
                            "https://legosforlife2021.firebaseapp.com",
                            "https://legosforlife2021.web.app")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                } );
            });

            services.AddScoped<IBodyRepository, BodyRepository>();
            services.AddScoped<IBodyService, BodyService>();


            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddDbContext<AuthDbContext>(options => options.UseSqlite(_configuration.GetConnectionString("AuthConnection")));

            services.AddScoped<ITitleRepository, TitleRepository>();
            services.AddScoped<ITitleService, TitleService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();

            services.AddDbContext<PostContext>(options =>
            {
                options.UseSqlite("Data Source=main.db");
                options.UseLoggerFactory(loggerFactory);
            });

            services.AddDbContext<PostContext>(options => options.UseSqlite("Data Source=main.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PostContext postContext,
            AuthDbContext authDbContext, IAuthService authService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "qwertygroup.WebApi v1"));
            }
            #region postDbtestdata
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
            postContext.titles.Add(new TitleEntity
            {
                Id = 1,
                Text = "Pepe clap is strong"
            });
            postContext.titles.Add(new TitleEntity
            {
                Id = 2,
                Text = "Potatoes ruin society"
            });
            postContext.Add(new PostEntity { Id = 1, TitleId = 1, BodyId = 1, UserId = 1 });
            postContext.Add(new PostEntity { Id = 2, TitleId = 2, BodyId=2, UserId = 1 });
            postContext.SaveChanges();
            #endregion

            //new AuthDbSeeder(authDbContext, securityService).SeedDevelopment();
            
            new AuthDbSeeder(authDbContext, authService).SeedDevelopment();
            app.UseCors("Dev-cors");
            app.UseHttpsRedirection();
            app.UseMiddleware<JwtMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseDeveloperExceptionPage();
        }
    }
}
