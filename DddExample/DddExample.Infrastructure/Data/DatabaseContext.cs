using DddExample.Domain.Aggregates.BookAggregate;
using DddExample.Infrastructure.Data.Configurations.Books;
using Microsoft.EntityFrameworkCore;

namespace DddExample.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChapterConfiguration());

            modelBuilder.Entity<BookType>().HasData(BookType.List());
        }
    }
}