using JavaCollect.Application.Interfaces;
using JavaCollect.Domain.Entitys;
using JavaCollect.Domain.Services;
using JavaCollect.Shared.Constants;
using Microsoft.Extensions.Logging;

namespace JavaCollect.Application.Services
{
    public class ShtCollectService(ILogger<ShtCollectService> logger, IShtService shtService, ShtItemService shtItemService)
    {
        private static readonly int decreaseNum = 1000;

        public async Task RetrievalPageAsync()
        {
            int startPageNo = 684;

            int currentPageNo = startPageNo;

            while (currentPageNo > 0)
            {
                currentPageNo -= 1;

                string listHtml = await shtService.GetSubtitleListHtmlAsync(currentPageNo);
                List<string> threadIds = shtService.ExtractThreads(listHtml);

                foreach (string threadId in threadIds)
                {
                    ShtItem? item;
                    //item = shtItemService.Get(threadId);
                    //if (item != null)
                    //{
                    //    logger.LogInformation("当前序号已收录: {id} {title}", threadId, item.Title);
                    //    continue;
                    //}

                    string threadHtml = await shtService.GetThreadHtml(threadId);

                    item = new ShtItem() { Id = threadId };
                    try
                    {
                        shtService.Scrape(threadHtml, item);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "解析网页内容发生错误");
                        continue;
                    }

                    if (!item.Type.NeedCollect())
                    {
                        continue;
                    }

                    shtItemService.Add(item);
                }
            }
        }

        public async Task RetrievalId()
        {
            int startThreadId = 1115048;
            int stopThreadId = startThreadId - decreaseNum;

            int currentId = startThreadId;

            while (currentId >= stopThreadId)
            {
                currentId -= 1;
                string threadHtml = await shtService.GetThreadHtml(currentId.ToString());

                ShtItem item = new ShtItem() { Id = currentId.ToString() };

                try
                {
                    shtService.Scrape(threadHtml, item);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "解析网页内容发生错误");
                    continue;
                }

                if (!item.Type.NeedCollect())
                {
                    continue;
                }

                shtItemService.Add(item);
            }
        }
    }
}
