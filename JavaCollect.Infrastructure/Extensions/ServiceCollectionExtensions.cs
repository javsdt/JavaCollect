using JavaCollect.Application.Interfaces;
using JavaCollect.Domain.Repositorys;
using JavaCollect.Infrastructure.BaseServices;
using JavaCollect.Infrastructure.Repositorys;
using JavaCollect.Infrastructure.Services;
using JavaCollect.Shared.Constants;
using Javsdt.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JavaCollect.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JavaCollectContext>();
            services.AddScoped<IShtItemRepository, ShtItemRepository>();

            services.AddHttpClient(WebsiteType.Trust.ToString())
                .ConfigurePrimaryHttpMessageHandler(() => HttpClientConfig.CreateTrustHttpClientHandler());

            services.AddScoped<HttpClientWrapper>();
            services.AddScoped<BrowserPioneerService>();

            services.AddScoped<IShtService, ShtService>();
        }
    }

    internal class HttpClientConfig
    {
        internal static HttpMessageHandler CreateTrustHttpClientHandler()
        {
            return new HttpClientHandler
            {
                // 忽略 SSL 证书错误
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };
        }
    }
}
