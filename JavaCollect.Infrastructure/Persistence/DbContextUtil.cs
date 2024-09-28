using JavaCollect.Shared.Constants;
using Microsoft.Extensions.Configuration;

namespace JavaCollect.Infrastructure.Persistence
{
    public class DbContextUtil
    {
        public static string GetConnectString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(ApplicationInfo.AppSettingsJsonPath, optional: false, reloadOnChange: true)
                .Build();
            return configuration.GetConnectionString("AppDb")!;
        }
    }
}
