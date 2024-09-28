using JavaCollect.Domain.Entitys;
using JavaCollect.Infrastructure.Persistence;
using JavaCollect.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;

namespace Javsdt.Infrastructure.Persistence
{
    public class JavaCollectContext : DbContext
    {
        public JavaCollectContext(DbContextOptions<JavaCollectContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbContextUtil.GetConnectString());
            }
        }

        public JavaCollectContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShtItem>()
                .Property(st => st.Type)
                .HasConversion(
                    Type => Type.ToString(),
                    TypeStr => (ShtType)Enum.Parse(typeof(ShtType), TypeStr)
            );
        }

        public DbSet<ShtItem> ShtItems { get; set; }

    }
}
