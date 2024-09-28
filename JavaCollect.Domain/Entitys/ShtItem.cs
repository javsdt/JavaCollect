using JavaCollect.Shared.Constants;

namespace JavaCollect.Domain.Entitys
{
    public class ShtItem : BaseEntity
    {
        public string Id { get; set; } = default!;

        public ShtType Type { get; set; }

        public string? Magnet  { get; set; }

        public DateTime Premiered { get; set; }

        public string Title { get; set; } = default!;

    }
}
