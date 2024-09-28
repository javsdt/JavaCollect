using JavaCollect.Domain.Entitys;

namespace JavaCollect.Application.Interfaces
{
    public interface IShtService
    {
        List<string> ExtractThreads(string listHtml);
        Task<string> GetSubtitleListHtmlAsync(int pageNo);
        Task<string> GetThreadHtml(string threadId);
        Task<string> GetYoumaListHtmlAsync(int startPageNo);
        void Scrape(string itemHtml, ShtItem item);
    }
}
