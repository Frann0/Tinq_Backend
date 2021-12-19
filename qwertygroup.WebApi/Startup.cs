using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using qwertygroup.DataAccess;
using qwertygroup.DataAccess.Entities;
using qwertygroup.Security;
using qwertygroup.Security.IServices;
using qwertygroup.WebApi.Extensions;
using qwertygroup.WebApi.Middleware;

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
                        });

            services.AddControllers();
            services.AddApplicationServices();
            services.AddAuthServices(_configuration);
            services.AddSwaggerDocumentation();
            
            services.AddDbContext<AuthDbContext>(options => 
                options.UseSqlite(_configuration.GetConnectionString("AuthConnection")));
            services.AddDbContext<PostContext>(options =>
            {
                options.UseSqlite("Data Source=main.db");
                options.UseLoggerFactory(loggerFactory);
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
                            "https://tinq-34862.web.app",
                            "https://tinq-34862.firebaseapp.com",
                            "https://tinq.online")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PostContext postContext,
            AuthDbContext authDbContext, IAuthService authService)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Dev-cors");
                new AuthDbSeeder(authDbContext, authService).SeedDevelopment();
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var _ctx = services.GetService<AuthDbContext>();
                    var _Postctx = services.GetService<PostContext>();
                    _ctx.Database.EnsureCreated();
                    _Postctx.Database.EnsureCreated();
                    new AuthDbSeeder(authDbContext, authService).SeedDevelopment();
                    app.UseCors("Prod-cors");
                }
            }
            #region postDbtestdata
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

            postContext.tags.Add(new TagEntity
            {
                Id = 1,
                Text = "Good"
            });
            postContext.tags.Add(new TagEntity
            {
                Id = 2,
                Text = "Wowza"
            });
            postContext.postTags.Add(new PostTagEntity { PostId = 1, TagId = 1 });
            postContext.postTags.Add(new PostTagEntity { PostId = 1, TagId = 2 });
            postContext.postTags.Add(new PostTagEntity { PostId = 2, TagId = 2 });
            postContext.posts.Add(new PostEntity { Id = 1, TitleId = 1, BodyId = 1, UserId = 1 });
            postContext.posts.Add(new PostEntity { Id = 2, TitleId = 2, BodyId = 2, UserId = 1 });
            postContext.SaveChanges();
            #endregion
            

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
