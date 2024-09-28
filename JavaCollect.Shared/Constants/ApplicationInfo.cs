using System.Text.RegularExpressions;

namespace JavaCollect.Shared.Constants
{
    public static class ApplicationInfo
    {
        private static readonly string AppName = "JavaCollect";

        public static string CurrentDirectory { get; } = Environment.CurrentDirectory;

        public static string AppSettingsJsonPath { get; } = GetAppSettingsJsonPath();

        private static string GetAppSettingsJsonPath()
        {
            string projectRoot = Regex.Match(CurrentDirectory, @$"^.*?{AppName}").Value;
            return Path.Combine(projectRoot, $"{AppName}.Shared", "Configuration/appsettings.Development.json");
        }

    }
}
