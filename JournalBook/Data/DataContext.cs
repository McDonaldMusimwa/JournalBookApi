using Microsoft.EntityFrameworkCore;

using JournalBook.Models;
namespace JournalBook.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        
        }

        public DbSet<Owner>Owners { get; set; }

        public DbSet<Story>Stories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Story>()
                .HasOne(s => s.Owner)
                .WithMany(q => q.Stories)
                .HasForeignKey(s => s.OwnerId);
        }
    }
}
