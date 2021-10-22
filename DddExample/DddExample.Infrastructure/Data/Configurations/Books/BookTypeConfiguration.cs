using DddExample.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddExample.Infrastructure.Data.Configurations.Books
{
    internal class BookTypeConfiguration : IEntityTypeConfiguration<BookType>
    {
        public void Configure(EntityTypeBuilder<BookType> builder)
        {
            builder.ToTable("BookTypes");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();
            
            builder.Property(x => x.Name)
                .HasMaxLength(64)
                .IsRequired();
        }
    }
}