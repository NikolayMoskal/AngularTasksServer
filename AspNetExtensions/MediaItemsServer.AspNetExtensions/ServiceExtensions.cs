using MediaItemsServer.Services;
using MediaItemsServer.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MediaItemsServer.AspNetExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IMediaItemsService, MediaItemsService>();

            return services;
        }
    }
}
