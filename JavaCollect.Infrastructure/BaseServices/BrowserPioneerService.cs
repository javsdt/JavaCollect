using JavaCollect.Infrastructure.BaseServices.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JavaCollect.Infrastructure.BaseServices
{
    internal class BrowserPioneerService(ILogger<BrowserPioneerService> _logger, HttpClientWrapper httpClientHelper, 
        IConfiguration configuration)
    {
        private readonly string remoteDownloadServer = configuration["TrustServers:BrowserPioneer:DownloadImage"]!;
        private readonly string webpageContentServer = configuration["TrustServers:BrowserPioneer:WebpageContent"]!;

        public async Task DownloadImageByOtherServiceAsync(string pageUrl, string imageXPath, string savePath,
            string? obstacleXPath = null, int downCount = 0)
        {
            RemoteImageDownloadRequest command = new RemoteImageDownloadRequest
            {
                PageUrl = pageUrl,
                ImageXPath = imageXPath,
                ObstacleXPath = obstacleXPath,
                DownCount = downCount,
                ImageName = Path.GetFileName(savePath),
            };

            HttpResponseMessage response = await httpClientHelper.PostJsonAsync(remoteDownloadServer, command);
            response.EnsureSuccessStatusCode();
            byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(savePath, fileBytes);
        }

        public async Task<string> GetWebpageContentAsync(string pageUrl)
        {
            RemoteWebpageContentRequest request = new RemoteWebpageContentRequest
            {
                PageUrl = pageUrl
            };

            HttpResponseMessage response = await httpClientHelper.PostJsonAsync(webpageContentServer, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
