using JavaCollect.Domain.Repositorys;
using JavaCollect.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JavaCollect.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ShtItemService>();
        }
    }
}
