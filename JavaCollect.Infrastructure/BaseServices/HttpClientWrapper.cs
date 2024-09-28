using JavaCollect.Shared.Constants;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace JavaCollect.Infrastructure.BaseServices
{
    public class HttpClientWrapper(ILogger<HttpClientWrapper> logger, IHttpClientFactory _httpClientFactory)
    {
        public async Task<HttpResponseMessage> PostJsonAsync<T>(string url, T content)
        {
            string jsonContent = JsonSerializer.Serialize(content, new JsonSerializerOptions() { WriteIndented = true });
            StringContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            logger.LogInformation("准备向信任服务{url} 发送内容:\n{content}", url, jsonContent);

            HttpClient client = _httpClientFactory.CreateClient(WebsiteType.Trust.ToString());
            HttpResponseMessage response = await client.PostAsync(url, httpContent);

            return response;
        }
    }
}
