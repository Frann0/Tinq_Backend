using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using qwertygroup.Core.IServices;
using qwertygroup.DataAccess;
using qwertygroup.DataAccess.Entities;
using qwertygroup.DataAccess.Repositories;
using qwertygroup.Domain.IRepositories;
using qwertygroup.Domain.Services;

namespace qwertygroup.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "qwertygroup.WebApi", Version = "v1" });
            });
            services.AddScoped<IBodyRepository, BodyRepository>();
            services.AddScoped<IBodyService, BodyService>();
            services.AddScoped<ITitleRepository,TitleRepository>();
            services.AddScoped<ITitleService,TitleService>();
            services.AddDbContext<PostContext>(options => options.UseSqlite("Data Source=main.Db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PostContext postContext)
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
            postContext.titles.Add(new TitleEntity{
                Id=1,
                Text = "Existential crisis sucks"
            });
            postContext.titles.Add(new TitleEntity{
                Id=2,
                Text = "Potatoes ruin society"
            });
            postContext.SaveChanges();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
