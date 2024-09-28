using JavaCollect.Application.Extensions;
using JavaCollect.Infrastructure.Extensions;
using JavaCollect.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using JavaCollect.Application.Services;

namespace JavaCollect.Starter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            using (var scope = serviceProvider.CreateScope())
            {
                ShtCollectService collectService = scope.ServiceProvider.GetRequiredService<ShtCollectService>();
                collectService.RetrievalPageAsync().Wait();
            }

            Console.WriteLine("Hello, World!");
        }
        private static ServiceProvider ConfigureServices()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var services = new ServiceCollection();

            // 配置 IConfiguration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration/appsettings.json"), optional: false, reloadOnChange: true)
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Configuration/appsettings.{environment}.json"), optional: false, reloadOnChange: true)
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration/extrasettings.json"), optional: false, reloadOnChange: true)
                .Build();
            services.AddSingleton<IConfiguration>(configuration);

            services.UseSerilogLogging(configuration);
            services.AddDomain();
            services.AddApplication(configuration);
            services.AddInfrastructure(configuration);

            return services.BuildServiceProvider();
        }
    }
}
