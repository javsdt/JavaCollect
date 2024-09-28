using HtmlAgilityPack;
using JavaCollect.Application.Interfaces;
using JavaCollect.Domain.Entitys;
using JavaCollect.Infrastructure.BaseServices;
using JavaCollect.Shared.Constants;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace JavaCollect.Infrastructure.Services
{
    internal class ShtService(ILogger<ShtService> logger, BrowserPioneerService browserPioneerService) : IShtService
    {
        public async Task<string> GetYoumaListHtmlAsync(int pageNo)
        {
            string listUrl = FormatYoumaListUrl(pageNo);
            string html = await browserPioneerService.GetWebpageContentAsync(listUrl);
            return html;
        }
        public async Task<string> GetSubtitleListHtmlAsync(int pageNo)
        {
            string listUrl = FormatSubtitleListUrl(pageNo);
            string html = await browserPioneerService.GetWebpageContentAsync(listUrl);
            return html;
        }

        public async Task<string> GetThreadHtml(string threadId)
        {
            string threadUrl = FormatThreadUrl(threadId);
            string html = await browserPioneerService.GetWebpageContentAsync(threadUrl);
            return html;
        }

        public List<string> ExtractThreads(string listHtml)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(listHtml);

            List<string> ids = new List<string>();
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//tbody[@id]");
            foreach (var node in nodes)
            {
                string id = node.GetAttributeValue("id", "");
                if (id.StartsWith("normalthread_"))
                {
                    ids.Add(id.Substring("normalthread_".Length));
                }
            }

            if (ids.Count <26 )
            {
                logger.LogWarning("当前页面不足30个thread");
            }

            return ids;
        }

        public void Scrape(string itemHtml, ShtItem item)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(itemHtml);

            HtmlNode notFound = document.DocumentNode.SelectSingleNode("//div[@class='alert_error']");
            if (notFound != null)
            {
                logger.LogWarning("当前页面无内容");
                return;
            }

            //类型 //*[@id="pt"]/div/a[4] /html/body/div[6]/div[4]/div/a[4] //*[@id="pt"]/div/a[4]
            string type = document.DocumentNode.SelectSingleNode("//div[@id='pt']/div/a[4]").InnerText;
            item.Type = (ShtType)Enum.Parse(typeof(ShtType), type);
            if (!item.Type.NeedCollect())
            {
                return;
            }
            if (item.Type != ShtType.亚洲有码原创 && item.Type != ShtType.高清中文字幕)
            {
                return;
            }

            //磁力 //*[@id="code_VIz"]/ol/li/text() //*[@id="code_v7E"]/ol/li/text()
            string magnet = document.DocumentNode.SelectSingleNode("//div[@class='blockcode']//ol").InnerText;
            item.Magnet = magnet;

            //发表于 //*[@id="authorposton8845810"]
            string srcPremiered = document.DocumentNode.SelectSingleNode("//div[@class='authi']/em").InnerText;
            string premiered = srcPremiered[4..];
            item.Premiered = DateTime.Parse(premiered);

            //标题
            string title = document.DocumentNode.SelectSingleNode("//title").InnerText;
            item.Title = title;

        }

        private static string FormatYoumaListUrl(int pageNo) => $"https://sehuatang.net/forum.php?mod=forumdisplay&fid=37&page={pageNo}";

        private static string FormatSubtitleListUrl(int pageNo) => $"https://sehuatang.net/forum-103-{pageNo}.html";

        private static string FormatThreadUrl(string threadId) => $"https://sehuatang.net/thread-{threadId}-1-748.html";

    }
}
