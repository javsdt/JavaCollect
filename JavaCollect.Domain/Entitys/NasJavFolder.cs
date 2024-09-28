using System.ComponentModel.DataAnnotations;

namespace JavaCollect.Domain.Entitys
{
    public class NasJavFolder : BaseEntity
    {
        public NasJavFolder(string path)
        {
            Path = path;
        }

        [Key]
        public string Path { get; set; }

        public string? Car { get; set; }

        public bool HasVideo { get; set; }

    }
}
