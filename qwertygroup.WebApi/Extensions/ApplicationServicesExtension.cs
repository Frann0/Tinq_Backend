using Microsoft.Extensions.DependencyInjection;
using qwertygroup.Core.IServices;
using qwertygroup.DataAccess.Repositories;
using qwertygroup.Domain.IRepositories;
using qwertygroup.Domain.Services;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.IServices;
using qwertygroup.Security.Repositories;
using qwertygroup.Security.Services;

namespace qwertygroup.WebApi.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPostTagRepository, PostTagRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IBodyRepository, BodyRepository>();
            services.AddScoped<IBodyService, BodyService>();
            services.AddScoped<ITitleRepository, TitleRepository>();
            services.AddScoped<ITitleService, TitleService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            
            return services;
        }
    }
}