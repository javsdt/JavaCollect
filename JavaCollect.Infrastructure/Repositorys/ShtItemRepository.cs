using JavaCollect.Domain.Entitys;
using JavaCollect.Domain.Repositorys;
using Javsdt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JavaCollect.Infrastructure.Repositorys
{
    internal class ShtItemRepository(JavaCollectContext context) : IShtItemRepository
    {
        public void Add(ShtItem item)
        {
            context.ShtItems.Add(item);
            context.SaveChanges();
        }

        public ShtItem? Get(string id)
        {
            return context.ShtItems
                .AsNoTracking()
                .FirstOrDefault(record => record.Id == id);
        }
    }
}
