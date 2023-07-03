using MediaItemsServer.Data;
using MediaItemsServer.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MediaItemsServer.AspNetExtensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<DbContext>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IRoleRepository, RoleRepository>();
            services.AddSingleton<IMediaItemsRepository, MediaItemsRepository>();

            return services;
        }
    }
}
