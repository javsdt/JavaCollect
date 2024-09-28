using JavaCollect.Domain.Entitys;

namespace JavaCollect.Domain.Repositorys
{
    public interface IShtItemRepository
    {
        void Add(ShtItem item);
        ShtItem? Get(string id);
    }
}
