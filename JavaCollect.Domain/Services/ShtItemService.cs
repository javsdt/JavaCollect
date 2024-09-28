using JavaCollect.Domain.Entitys;
using JavaCollect.Domain.Repositorys;

namespace JavaCollect.Domain.Services
{
    public class ShtItemService(IShtItemRepository repository)
    {
        public void Add(ShtItem item)
        {
            repository.Add(item);
        }

        public ShtItem? Get(string Id)
        {
            return repository.Get(Id);
        }
    }
}
