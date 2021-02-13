using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using  BassetS.WApi.Logger.DAO;
namespace BassetS.WApi.Logger
{
    public static class IoCHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SQLiteAdapter>();
            return services;
        }
    }
}